using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using System.Data;
using WealthERP.Base;
using BoProductMaster;
using BoWerpAdmin;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using System.Text.RegularExpressions;
using Telerik.Web.UI;
namespace WealthERP.OnlineOrderManagement
{
    public partial class MFSchemeRelateInformation : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            BindAMC();
            BindCategory();
            BindScheme();
            userVo = (UserVo)Session[SessionContents.UserVo];
        }
        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC.SelectedIndex != 0)
            {
                BindScheme();
            }
        }
        private void BindAMC()
        {

            PriceBo priceBo = new PriceBo();
            DataTable dtGetAMCList = new DataTable();
            dtGetAMCList = priceBo.GetMutualFundList();
            ddlAMC.DataSource = dtGetAMCList;
            ddlAMC.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
            ddlAMC.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
            ddlAMC.DataBind();
            ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        private void BindCategory()
        {
            DataSet dsProductAssetCategory;
            ProductMFBo productMFBo = new ProductMFBo();
            dsProductAssetCategory = productMFBo.GetProductAssetCategory();
            DataTable dtCategory = dsProductAssetCategory.Tables[0];
            ddlCategory.DataSource = dtCategory;
            ddlCategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        protected void BindScheme()
        {
            DataTable dt;
            ProductMFBo productMFBo = new ProductMFBo();
            if (ddlAMC.SelectedValue == "0")
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC.SelectedValue));
                ddlScheme.DataSource = dt;
                ddlScheme.DataValueField = "PASP_SchemePlanCode";
                ddlScheme.DataTextField = "PASP_SchemePlanName";
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            else
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC.SelectedValue));
                ddlScheme.DataSource = dt;
                ddlScheme.DataValueField = "PASP_SchemePlanCode";
                ddlScheme.DataTextField = "PASP_SchemePlanName";
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

            }
        }
        protected void Go_OnClick(object sender, EventArgs e)
        {
            BindSchemeRelatedDetails();
        }
        protected void BindSchemeRelatedDetails()
        {
            OnlineOrderBackOfficeBo bo = new OnlineOrderBackOfficeBo();
            DataTable dtBindSchemeRelatedDetails = bo.GetSchemeDetails(int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), ddlCategory.SelectedValue);
              if (Cache["UnitHolding" + userVo.UserId] == null)
                    {
                        Cache.Insert("BindSchemeRelatedDetails" + userVo.UserId, dtBindSchemeRelatedDetails);
                    }
                    else
                    {
                        Cache.Remove("BindSchemeRelatedDetails" + userVo.UserId);
                        Cache.Insert("BindSchemeRelatedDetails" + userVo.UserId, dtBindSchemeRelatedDetails);
                    }
            rgSchemeDetails.DataSource = dtBindSchemeRelatedDetails;
            rgSchemeDetails.Rebind();
            pnlSchemeDetails.Visible = true;
        }

        protected void rgSchemeDetails_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindSchemeRelatedDetails = new DataTable();
            dtBindSchemeRelatedDetails = (DataTable)Cache["BindSchemeRelatedDetails" + userVo.UserId.ToString()];
            if (dtBindSchemeRelatedDetails != null)
            {
                rgSchemeDetails.DataSource = dtBindSchemeRelatedDetails;
                rgSchemeDetails.Visible = true;
            }
        }
        protected void rgSchemeDetails_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            // GridDataItem gvr = (GridDataItem)e.Item;
            if (e.CommandName == "Buy")
            {
                if (Session["PageDefaultSetting"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderPurchaseTransType')", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderPurchaseTransType')", true);
                }

            }
            if (e.CommandName == "SIP")
            {
                if (Session["PageDefaultSetting"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderSIPTransType')", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderSIPTransType')", true);
                }
            }
          
        }
        public void rgSchemeDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Label lblISRedeemFlag = (Label)e.Item.FindControl("lblISRedeemFlag");

                Label lblSIPSchemeFlag = (Label)e.Item.FindControl("lblSIPSchemeFlag");
                Label lblIsPurcheseFlag = (Label)e.Item.FindControl("lblIsPurcheseFlag");
                Label lblSchemeRating = (Label)e.Item.FindControl("lblSchemeRating");

                Label lblRating3Year = (Label)e.Item.FindControl("lblRating3Year");
                Label lblRating5Year = (Label)e.Item.FindControl("lblRating5Year");
                Label lblRating10Year = (Label)e.Item.FindControl("lblRating10Year");
                Label lblRatingAsOnPopUp = (Label)e.Item.FindControl("lblRatingAsOnPopUp");

                System.Web.UI.WebControls.Image imgSchemeRating = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgSchemeRating");

                System.Web.UI.WebControls.Image imgRating3Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating3yr");
                System.Web.UI.WebControls.Image imgRating5Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating5yr");
                System.Web.UI.WebControls.Image imgRating10Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating10yr");
                System.Web.UI.WebControls.Image imgRatingOvelAll = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRatingOvelAll");

                if (lblSIPSchemeFlag.Text.Trim().ToLower() == "false")
                {
                    ImageButton imgSIP = (ImageButton)e.Item.FindControl("imgSip");
                    imgSIP.Visible = false;

                }
                if (lblIsPurcheseFlag.Text.Trim().ToLower() == "false")
                {
                    ImageButton imgBuy = (ImageButton)e.Item.FindControl("imgBuy");
                    imgBuy.Visible = false;

                }
               
                lblIsPurcheseFlag.Visible = false;
                lblSIPSchemeFlag.Visible = false;


                imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblSchemeRating.Text.Trim() + ".png";

                imgRating3Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating3Year.Text.Trim() + ".png";
                imgRating5Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating5Year.Text.Trim() + ".png";
                imgRating10Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating10Year.Text.Trim() + ".png";

                imgRatingOvelAll.ImageUrl = @"../Images/MorningStarRating/RatingOverall/" + lblSchemeRating.Text.Trim() + ".png";


            }

        }
    }
}