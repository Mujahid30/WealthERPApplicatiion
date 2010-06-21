using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Data.SqlTypes;
using VoWerpAdmin;
using BoWerpAdmin;
using BoCommon;

namespace WealthERP.Admin
{
    public partial class Mapping : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            MappingBo obj = new MappingBo();

            if (!String.IsNullOrEmpty(Request.QueryString["AssetGroup"]) && Request.QueryString["AssetGroup"].ToString().Equals("Equity"))
                DivScripMapping.Style.Add("display", "visible");

            else if (!String.IsNullOrEmpty(Request.QueryString["AssetGroup"]) && Request.QueryString["AssetGroup"].ToString().Equals("MF"))
                DivSchemeMapping.Style.Add("display", "visible");

            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                this.ClearControl();
                if (!String.IsNullOrEmpty(Request.QueryString["AssetGroup"]) && Request.QueryString["AssetGroup"].Equals("Equity"))
                {
                    ds = obj.GetInstrumentCategory("DE");
                    ddlInstCategory.DataSource = ds.Tables[0];
                    ddlInstCategory.DataTextField = ds.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                    ddlInstCategory.DataValueField = ds.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                    ddlInstCategory.DataBind();
                    ddlInstCategory.Items.Insert(0, "Select a InstrumentCategory");
                    ds.Clear();

                    ds = obj.GetProductMarketCapClassification();
                    ddlMarketCap.DataSource = ds.Tables[0];
                    ddlMarketCap.DataTextField = ds.Tables[0].Columns["PMCC_CapClassification"].ToString();
                    ddlMarketCap.DataValueField = ds.Tables[0].Columns["PMCC_MarketCapClassificationCode"].ToString();
                    ddlMarketCap.DataBind();
                    ddlMarketCap.Items.Insert(0, "Select a Market Cap");
                    ds.Clear();
           
                }

                else if (!String.IsNullOrEmpty(Request.QueryString["AssetGroup"]) && Request.QueryString["AssetGroup"].Equals("MF"))
                {
                    ds = obj.GetInstrumentCategory("MF");
                    ddlSchmMapInstCat.DataSource = ds;
                    ddlSchmMapInstCat.DataTextField = ds.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                    ddlSchmMapInstCat.DataValueField = ds.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                    ddlSchmMapInstCat.DataBind();
                    ddlSchmMapInstCat.Items.Insert(0, "Select a InstrumentCategory");
                    ds.Clear();


                    ds = obj.GetProductMarketCapClassification();
                    ddlSchmMapMrktCap.DataSource = ds;
                    ddlSchmMapMrktCap.DataTextField = ds.Tables[0].Columns["PMCC_CapClassification"].ToString();
                    ddlSchmMapMrktCap.DataValueField = ds.Tables[0].Columns["PMCC_MarketCapClassificationCode"].ToString();
                    ddlSchmMapMrktCap.DataBind();
                    ddlSchmMapMrktCap.Items.Insert(0, "Select a Market Cap");
                    ds.Clear();
            

                }
                if (!String.IsNullOrEmpty(Request.QueryString["From"]) && (Request.QueryString["From"].ToString()).Equals("Main")
                    && !String.IsNullOrEmpty(Request.QueryString["AssetGroup"]) && Request.QueryString["AssetGroup"].Equals("Equity")
                     && (Request.QueryString["Mode"].Equals("Edit") || Request.QueryString["Mode"].Equals("View"))
              )
                {
                    this.LoadData("Equity");
                }

                if (!String.IsNullOrEmpty(Request.QueryString["From"]) && (Request.QueryString["From"].ToString()).Equals("Main")
                         && !String.IsNullOrEmpty(Request.QueryString["AssetGroup"]) && Request.QueryString["AssetGroup"].ToString().Equals("MF")
                    && (Request.QueryString["Mode"].Equals("Edit") || Request.QueryString["Mode"].Equals("View"))
                   )
                {
                    this.LoadData("MF");

                }
            }
          
            if (!String.IsNullOrEmpty(Request.QueryString["Mode"]) && Request.QueryString["Mode"].Equals("View"))
            {
                this.ReadOnly(true);
                btnSubmit.Visible = false;

            }

            if (!String.IsNullOrEmpty(Request.QueryString["From"])
                && (Request.QueryString["From"].ToString()).Equals("R")
                && !String.IsNullOrEmpty(Request.QueryString["AssetGroup"])
                && Request.QueryString["AssetGroup"].Equals("Equity")
                 && !String.IsNullOrEmpty(Request.QueryString["RecordName"])
                )
            {
                txtScripName.Text = Request.QueryString["RecordName"];

            }

            if (!String.IsNullOrEmpty(Request.QueryString["From"]) && (Request.QueryString["From"]).Equals("R")
                      && !String.IsNullOrEmpty(Request.QueryString["AssetGroup"]) && Request.QueryString["AssetGroup"].Equals("MF")
                 && !String.IsNullOrEmpty(Request.QueryString["RecordName"])
               )
                txtSchmPlnName.Text = Request.QueryString["RecordName"];

        }

        protected void OnSelectedIndexChanged_InstCategory(object sender, EventArgs e)
        {
            MappingBo obj = new MappingBo();
            DataSet ds = new DataSet();
            string instrumentgroup = ddlInstCategory.SelectedValue;
            if (ddlSubCategory.Items.Count > 0)
            {
                ddlSubCategory.Items.Clear();
            }
            
            if (!String.IsNullOrEmpty(Request.QueryString["AssetGroup"]) && Request.QueryString["AssetGroup"].ToString().Equals("Equity"))
            {
                ds = obj.GetAssetInstrumentSubCategory("DE", instrumentgroup);
                ddlSubCategory.DataSource = ds.Tables[0]; ;
                ddlSubCategory.DataTextField = "PAISC_AssetInstrumentSubCategoryName";
                ddlSubCategory.DataValueField = "PAISC_AssetInstrumentSubCategoryCode";
                ddlSubCategory.DataBind();
            }
        }

        protected void ddlSchmMapInstCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            MappingBo obj = new MappingBo();
            DataSet ds = new DataSet();
            string instrumentgroup = ddlSchmMapInstCat.SelectedValue;
            if (!String.IsNullOrEmpty(Request.QueryString["AssetGroup"].ToString()) && Request.QueryString["AssetGroup"].Equals("MF"))
            {
                ds = obj.GetAssetInstrumentSubCategory("MF", instrumentgroup);
                ddlSchmMapSubCat.DataSource = ds.Tables[0]; 
                ddlSchmMapSubCat.DataTextField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlSchmMapSubCat.DataValueField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlSchmMapSubCat.DataBind();
            }
        }

        protected void ddlSchmMapSubCat_SelectedIndexChanged(Object sender, EventArgs e)
        {
            MappingBo obj = new MappingBo();
            DataSet ds = new DataSet();
            string subcategory = ddlSchmMapSubCat.SelectedValue;
            string instrumentgroup = ddlSchmMapInstCat.SelectedValue;
            if (!String.IsNullOrEmpty(Request.QueryString["AssetGroup"].ToString()) && Request.QueryString["AssetGroup"].Equals("MF"))
            {
                ds = obj.GetAssetInstrumentSubSubCategory("MF", instrumentgroup, subcategory);
                ddlSubSubCat.DataSource = ds.Tables[0]; ;
                ddlSubSubCat.DataTextField = ds.Tables[0].Columns["PAISSC_AssetInstrumentSubSubCategoryName"].ToString();
                ddlSubSubCat.DataValueField = ds.Tables[0].Columns["PAISSC_AssetInstrumentSubSubCategoryCode"].ToString();
                ddlSubSubCat.DataBind();
            }

        }

        protected void OnClick_Submit(object sender, EventArgs e)
        {
            MappingVo objVo = new MappingVo();
            MappingBo objBo = new MappingBo();
            string WERPCode = String.Empty;
            string Mode = Request.QueryString["Mode"];
            if (!String.IsNullOrEmpty(Request.QueryString["AssetGroup"]) && Request.QueryString["AssetGroup"].Equals("Equity"))
            {
                if (lblWERPCODE.Text != "")
                    objVo.WERPCOde = lblWERPCODE.Text;
                else objVo.WERPCOde = null;
                objVo.Mode = lblWERPCODE.Text == String.Empty ? "New" : "Edit";
                objVo.ScripName = txtScripName.Text;
                objVo.Ticker = txtTicker.Text;
                objVo.IncorporationDate = Convert.ToDateTime(txtIncdate.Text);
                objVo.PublishDate = Convert.ToDateTime(txtPublishDate.Text);
                objVo.MarketLot = Convert.ToInt32(txtMarketLot.Text);
                objVo.FaceValue = Convert.ToDecimal(txtFaceValue.Text);
                objVo.BookClosure = Convert.ToDateTime(txtBookClouser.Text);
                objVo.InstrumentCategory = ddlInstCategory.SelectedValue;
                objVo.SubCategory = ddlSubCategory.SelectedValue;
                // objVo.Sector =Convert.ToInt32(ddlSector.DataValueField);
                //  objVo.MarketCap = Convert.ToInt32(ddlMarketCap.DataValueField);
                objVo.BSE = txtBSE.Text == "" ? null : txtBSE.Text;
                objVo.NSE = txtNSE.Text == "" ? null : txtNSE.Text;
                objVo.CERC = txtCERC.Text == "" ? null : txtCERC.Text;
                WERPCode = objBo.SubmitEquityMapping("Equity", objVo);
                lblWERPCODE.Text = WERPCode;

            }
            else if (!String.IsNullOrEmpty(Request.QueryString["AssetGroup"]) && Request.QueryString["AssetGroup"].Equals("MF"))
            {
                if (lblSchMapWerpCode.Text != null)
                    objVo.MFWERPCode = lblSchMapWerpCode.Text;
                else
                    objVo.MFWERPCode = null;
                objVo.Mode = Mode;
                objVo.SchemePlanName = txtSchmPlnName.Text;
                objVo.MFInstrumentCategory = ddlSchmMapInstCat.SelectedValue;
                objVo.MFSubCategory = ddlSchmMapSubCat.SelectedValue;
                objVo.MFSubSubCategory = ddlSubSubCat.SelectedValue;
                //  objVo.MFMarketCap = Convert.ToInt32(ddlSchmMapMrktCap.DataValueField);
                //   objVo.MFSector = Convert.ToInt32(ddlSchmMapSector.DataValueField);
                objVo.AMFI = txtAMFI.Text;
                objVo.CAMS = txtCAMS.Text;
                objVo.Karvy = txtKarvy.Text;
                WERPCode = objBo.SubmitMFMapping("MF", objVo);
                lblSchMapWerpCode.Text = WERPCode;

            }

        }

        protected void ClearControl()
        {

            //Write A code to clear the controls

        }

        protected void LoadData(string AssetGroup)
        {
            int WERPCODE;
            MappingVo objVo = new MappingVo();
            MappingBo objBo = new MappingBo();
            DataSet ds = new DataSet();
            if (!String.IsNullOrEmpty(Request.QueryString["WERPCODE"]) & AssetGroup.Equals("Equity"))
            {
                WERPCODE = Convert.ToInt32(Request.QueryString["WERPCODE"]);
                objVo = objBo.GetProductEquityScripDetails(WERPCODE);
                lblWERPCODE.Text = WERPCODE.ToString();
                txtScripName.Text = objVo.ScripName;
                txtTicker.Text = objVo.Ticker;
                txtIncdate.Text = objVo.IncorporationDate.ToString();
                txtPublishDate.Text = objVo.PublishDate.ToString();
                txtMarketLot.Text = objVo.MarketLot.ToString();
                txtFaceValue.Text = objVo.FaceValue.ToString();
                txtBookClouser.Text = objVo.BookClosure.ToString();
                ddlInstCategory.SelectedValue = objVo.InstrumentCategory;
                txtBSE.Text = objVo.BSE;
                txtNSE.Text = objVo.NSE;
                txtCERC.Text = objVo.CERC;
                //  ddlSector.SelectedValue = objVo.Sector.ToString();
                // ddlMarketCap.SelectedValue = objVo.MarketCap.ToString();

                ds = objBo.GetAssetInstrumentSubCategory("DE", objVo.InstrumentCategory);
                ddlSubCategory.DataSource = ds.Tables[0];
                ddlSubCategory.DataTextField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlSubCategory.DataValueField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlSubCategory.DataBind();
                ddlSubCategory.SelectedValue = objVo.SubCategory;

            }

            else if (!String.IsNullOrEmpty(Request.QueryString["WERPCODE"]) & AssetGroup.Equals("MF"))
            {
                WERPCODE = Convert.ToInt32(Request.QueryString["WERPCODE"]);
                objVo = objBo.GetProductAMCSchemePlanDetails(WERPCODE);
                lblSchMapWerpCode.Text = WERPCODE.ToString();
                txtSchmPlnName.Text = objVo.SchemePlanName;
                ddlSchmMapInstCat.SelectedValue = objVo.MFInstrumentCategory;

                //  ddlSchmMapSector.SelectedValue = objVo.MFSector.ToString();
                txtAMFI.Text = objVo.AMFI;
                txtCAMS.Text = objVo.CAMS;
                txtKarvy.Text = objVo.Karvy;

                ds = objBo.GetAssetInstrumentSubCategory(AssetGroup, objVo.MFInstrumentCategory);
                ddlSchmMapSubCat.DataSource = ds.Tables[0];
                ddlSchmMapSubCat.DataTextField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlSchmMapSubCat.DataValueField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlSchmMapSubCat.DataBind();
                ddlSchmMapSubCat.SelectedValue = objVo.MFSubCategory;
                ds.Clear();

                ds = objBo.GetAssetInstrumentSubSubCategory(AssetGroup, objVo.MFInstrumentCategory, objVo.MFSubCategory);
                ddlSubSubCat.DataSource = ds.Tables[0];
                ddlSubSubCat.DataTextField = ds.Tables[0].Columns["PAISSC_AssetInstrumentSubSubCategoryName"].ToString();
                ddlSubSubCat.DataValueField = ds.Tables[0].Columns["PAISSC_AssetInstrumentSubSubCategoryCode"].ToString();
                ddlSubSubCat.DataBind();
                ddlSubSubCat.SelectedValue = objVo.MFSubSubCategory;
            }

        }

        protected void ReadOnly(bool value)
        {

            txtScripName.Enabled =
            txtTicker.Enabled =
            txtIncdate.Enabled =
            txtPublishDate.Enabled =
            txtMarketLot.Enabled =
            txtFaceValue.Enabled =
            txtBookClouser.Enabled =
            ddlInstCategory.Enabled =
            ddlSubCategory.Enabled =
            //ddlSector.Enabled =
            ddlMarketCap.Enabled =
            txtBSE.Enabled =
            txtNSE.Enabled =
            txtCERC.Enabled =
            txtSchmPlnName.Enabled =
            ddlSchmMapInstCat.Enabled =
            ddlSchmMapSubCat.Enabled =
            ddlSubSubCat.Enabled =
           // ddlSchmMapSector.Enabled =
            ddlSchmMapMrktCap.Enabled =
            txtAMFI.Enabled = txtCAMS.Enabled = txtKarvy.Enabled = !value;

        }
    }
}