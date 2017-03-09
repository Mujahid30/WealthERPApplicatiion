using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

using BoProductMaster;
using VoProductMaster;
using WealthERP.Base;
using BoWerpAdmin;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VoUser;
using BoCommon;


namespace WealthERP.SuperAdmin
{
    public partial class AddSchemeMapping : System.Web.UI.UserControl
    {
        int schemePlanCode;
        DataSet dsSchemePlanDetails;
        CustomerBo customerBo;
        PriceBo priceBo = new PriceBo();
        ProductMFBo productMFBo = new ProductMFBo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        string strExternalCodeToBeEdited;
        String DisplayType;
        DataSet dsDataTransDetails;
        DataSet dsTransactionType = new DataSet();
        UserVo userVo = new UserVo();
      
        AdvisorVo advisorVo;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            int adviserId = advisorVo.advisorId;

            if (Session["OnlineSchemeSetupSchemecode"] != null)
            {
                ViewState["OnlineSchemeSetupSchemecode"] = Session["OnlineSchemeSetupSchemecode"];
                btnGo_Click(sender, e);
                Session["OnlineSchemeSetupSchemecode"] = null;
            }
            if (!IsPostBack)
            {
                BindAMC();
                BindSchemeCategory();
                btnExportFilteredData.Visible = false;
            }
            
        }

        //protected void txtSchemePlanCode_ValueChanged1(object sender, EventArgs e)
        //{
        //    dsSchemePlanDetails = new DataSet();
        //    customerBo = new CustomerBo();
        //    if (!string.IsNullOrEmpty(txtSchemePlanCode.Value.ToString().Trim()))
        //    {
        //        schemePlanCode = int.Parse(txtSchemePlanCode.Value);
        //        BindSchemePlanDetails(schemePlanCode);
        //    }
        //}
        protected void ddlMappingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMappingType.SelectedItem.Value.ToString()=="0")
            {
                trShemeMapping.Visible = false;
                trExternalsource.Visible = false;
                pnlCamsKarvy.Visible = false;
                pnlCams.Visible = false;
                pnltempleton.Visible = false;
                tdmapped.Visible = true;
                tduMaped.Visible = true;
            }
            else
            {
                trShemeMapping.Visible = false;
                trExternalsource.Visible = true;
                pnlgvScheme.Visible = false;
                tdmapped.Visible = false;
                tduMaped.Visible = false;
            }
        }
        private void BindAMC()
        {
            DataSet dsProductAmc;
            DataTable dtProductAMC;
            dsProductAmc = productMFBo.GetProductAmc();
            if (dsProductAmc.Tables[0].Rows.Count > 0)
                {
                    dtProductAMC = dsProductAmc.Tables[0];
                    ddlAMC.DataSource = dtProductAMC;
                    ddlAMC.DataTextField = dtProductAMC.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataValueField = dtProductAMC.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataBind();
                }
            ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        private void BindSchemeCategory()
        {
            DataSet dsSchemeCategory=new DataSet();
            DataTable dtSchemeCategory;
            dsSchemeCategory = priceBo.GetNavOverAllCategoryList();
            //----------------------------------------------Scheme Category Binding------------------------
            if (dsSchemeCategory.Tables.Count > 0)
            {
                dtSchemeCategory = dsSchemeCategory.Tables[0];
                ddlCategory.DataSource = dtSchemeCategory;
                ddlCategory.DataValueField = dtSchemeCategory.Columns["Category_Code"].ToString();
                ddlCategory.DataTextField = dtSchemeCategory.Columns["Category_Name"].ToString();
                ddlCategory.DataBind();

            }
            ddlCategory.Items.Insert(0, new ListItem("All", "0"));
        }


        public void BindSchemePlanDetails(int schemePlanCode)
        {
            //dsSchemePlanDetails = new DataSet();
            //customerBo = new CustomerBo();
            //schemePlanCode = int.Parse(txtSchemePlanCode.Value);
            //dsSchemePlanDetails = customerBo.GetSchemeDetails(schemePlanCode);
            //gvSchemeDetails.DataSource = dsSchemePlanDetails;
            //gvSchemeDetails.DataBind();
            //if (dsSchemePlanDetails != null)
            //    btnExportFilteredData.Visible = true;
            //if (Cache["gvSchemeDetailsForMappinginSuperAdmin"] == null)
            //{
            //    Cache.Insert("gvSchemeDetailsForMappinginSuperAdmin", dsSchemePlanDetails);
            //}
            //else
            //{
            //    Cache.Remove("gvSchemeDetailsForMappinginSuperAdmin");
            //    Cache.Insert("gvSchemeDetailsForMappinginSuperAdmin", dsSchemePlanDetails);
            //}
        }
        protected void SetParameter()
        {
            if (ddlAMC.SelectedIndex != 0)
            {
                hdnAMC.Value = ddlAMC.SelectedValue;
                ViewState["AMCDropDown"] = hdnAMC.Value;
            }
            else
            {
                hdnAMC.Value = "0";
            }
            if (ddlCategory.SelectedIndex != 0)
            {
                hdnCategory.Value = ddlCategory.SelectedValue;
                ViewState["CategoryDropDown"] = hdnCategory.Value;
            }
            else
            {
                hdnCategory.Value = "0";
            }
            if (ddlExternalSource.SelectedIndex != 0)
            {

                hdnExternalSource.Value = ddlExternalSource.SelectedValue;
                ViewState["ExternalSource"] = hdnExternalSource.Value;
            }
            else
            {
                hdnExternalSource.Value = " ";
            }
            if(ddlSchemeMappingType.SelectedIndex!=0)
            {
                hdnType.Value = ddlExternalSource.SelectedValue;
                ViewState["Type"] = hdnType.Value;
            }
            else
            {
                hdnType.Value = "0";
            }
        
        }
        public void BindSchemePlanDetails()
        {
            //SetParameter();
            dsSchemePlanDetails = new DataSet();
            customerBo = new CustomerBo();
            dsSchemePlanDetails = customerBo.GetSchemeMapDetails(hdnExternalSource.Value, 0, hdnCategory.Value, hdnType.Value,int.Parse(ddlMAapped.SelectedValue.ToString()));
            if (dsSchemePlanDetails.Tables[0].Rows.Count > 0)
            {
                btnExportFilteredData.Visible = true;
                pnlgvScheme.Visible = true;
                gvSchemeDetails.Visible = true;
            }
            if (Cache["gvSchemeDetailsForMappinginSuperAdmin"] == null)
            {
                Cache.Insert("gvSchemeDetailsForMappinginSuperAdmin", dsSchemePlanDetails);
            }
            else
            {
                Cache.Remove("gvSchemeDetailsForMappinginSuperAdmin");
                Cache.Insert("gvSchemeDetailsForMappinginSuperAdmin", dsSchemePlanDetails);
            }
            gvSchemeDetails.DataSource = dsSchemePlanDetails;
            gvSchemeDetails.DataBind();
        
        }
        public void BindDataTranslatorDetails()
        {
          
            customerBo = new CustomerBo();
            dsDataTransDetails = customerBo.GetDataTransMapDetails(DisplayType);
            if (dsDataTransDetails.Tables[0].Rows.Count!=0)
            {
                btnExportFilteredData.Visible = false;
                //pnlgvScheme.Visible = true;
                //gvSchemeDetails.Visible = true;
            }
           
            if (Cache["gvDataForMappinginSuperAdmin"] == null)
            {
                Cache.Insert("gvDataForMappinginSuperAdmin", dsDataTransDetails);
            }
            else
            {
                Cache.Remove("gvDataForMappinginSuperAdmin");
                Cache.Insert("gvDataForMappinginSuperAdmin", dsDataTransDetails);
            }
            //if (DisplayType == "CAMS" || DisplayType == "Sundaram")
            //{
            //    gvCams.DataSource = dsDataTransDetails;
            //    gvCams.DataBind();
            //}
            //if (DisplayType == "KARVY")
            //{

            //    gvCamsKarvy.DataSource = dsDataTransDetails;
            //    gvCamsKarvy.DataBind();
            //}
            //if (DisplayType == "Templeton")
            //{
            //    // BindDataTranslatorDetails();
            //    gvTempleton.DataSource = dsDataTransDetails;
            //    gvTempleton.DataBind();
            //}
           
        }


        protected void btnAddScheme_Click(object sender, EventArgs e)
        {
           // btnUpdate.Visible = false;
            //btnInsert.visible = true;
            //btnAddScheme.visible = false;
            Button buttonAddMaping = (Button)sender;
            GridEditFormItem gdi = (GridEditFormItem)buttonAddMaping.NamingContainer;
           
            //if(gdi<>null)
           
            {
                Button btn = gdi.FindControl("btnUpdate") as Button;
                btn.Text = "Add Mapping";
                //btn.Visible = false;
                //btn.Attributes.Add("Text", "Add Mapping");
                //btn.Attributes.Add("BackColor", "Black");
                //btn.CommandName = "";
                //btn.Text = "Add Mapping";
            }
            
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {

            if (ViewState["OnlineSchemeSetupSchemecode"] != null)
            {
                ddlMappingType.SelectedValue = "0";
                
            }

            if (ddlMappingType.SelectedValue.ToString() == "0" && ddlMAapped.SelectedValue !=null)
            {
                BindSchemePlanDetails();
                pnlgvScheme.Visible = true;
                pnlCamsKarvy.Visible = false;
            }
            else
            {
                DisplayType = ddlExternalSource.SelectedValue;
                if (DisplayType == "CAMS" || DisplayType == "Sundaram")
                {
                    BindDataTranslatorDetails();
                    gvCams.DataSource = dsDataTransDetails;
                    gvCams.DataBind();
                    pnlCams.Visible = true;
                    pnlCamsKarvy.Visible = false;
                   // gvCamsKarvy.Columns[2].Visible =true;
                    gvCams.Visible = true;
                }
                if (DisplayType == "KARVY")
                {
                    BindDataTranslatorDetails();
                    gvCamsKarvy.DataSource = dsDataTransDetails;
                    gvCamsKarvy.DataBind();
                    pnlCamsKarvy.Visible = true;
                    pnlCams.Visible = false;
                    pnltempleton.Visible = false;
                }
                if (DisplayType == "Templeton")
                {
                    BindDataTranslatorDetails();
                    gvTempleton.DataSource = dsDataTransDetails;
                    gvTempleton.DataBind();
                    pnltempleton.Visible = true;
                  //  gvCamsKarvy.Columns[2].Visible = false;
                  //  pnltempleton.Visible = true;
                    gvTempleton.Visible = true;
                    pnlCamsKarvy.Visible = false;
                    pnlCams.Visible = false;
                }
            }
        }
        protected void gvSchemeDetails_ItemCommand(object source, GridCommandEventArgs e)
        {
            int strSchemePlanCode = 0;
            int Isonline = 0;
            string strExternalCode = string.Empty;
            string strExternalType = string.Empty;
            DateTime createdDate = new DateTime();
            DateTime editedDate = new DateTime();
            DateTime deletedDate = new DateTime();
          
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                strExternalCodeToBeEdited=Session["extCodeTobeEdited"].ToString();
                CustomerBo customerBo = new CustomerBo();
                bool isUpdated = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtExtCode = (TextBox)e.Item.FindControl("txtExternalCodeForEditForm");
                DropDownList txtExtType = (DropDownList)e.Item.FindControl("ddlExternalType");
                TextBox txtSchemePlancode = (TextBox)e.Item.FindControl("txtSchemePlanCodeForEditForm");
                DropDownList ddlIsonline = (DropDownList)e.Item.FindControl("ddlONline");
                strSchemePlanCode = int.Parse(txtSchemePlancode.Text);
                strExternalCode = txtExtCode.Text;
                strExternalType = txtExtType.Text;
                Isonline =Convert.ToInt32(ddlIsonline.Text.ToString());
                editedDate = DateTime.Now;
                int count = customerBo.ToCheckSchemeisonline(strSchemePlanCode, Isonline, strExternalCode);
                if (count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('this scheme is allready onlline!!');", true);
                    return;
                }
                //isUpdated = false;
                isUpdated=customerBo.EditProductAMCSchemeMapping(strSchemePlanCode, strExternalCodeToBeEdited, strExternalCode, Isonline, strExternalType, createdDate, editedDate, deletedDate, userVo.UserId);

            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                bool isDeleted = false;
                customerBo = new CustomerBo();
                GridDataItem dataItem = (GridDataItem)e.Item;
                TableCell strSchemePlanCodeForDelete = dataItem["PASP_SchemePlanCode"];
                TableCell strSchemePlanNameForDelete = dataItem["PASP_SchemePlanName"];
                TableCell StrExternalCodeForDelete = dataItem["PASC_AMC_ExternalCode"];
                TableCell strExternalTypeForDelete = dataItem["PASC_AMC_ExternalType"];
                strSchemePlanCode = int.Parse(strSchemePlanCodeForDelete.Text);
                strExternalCode = StrExternalCodeForDelete.Text;
                strExternalType = strExternalTypeForDelete.Text;
                deletedDate = DateTime.Now;
                isDeleted = customerBo.DeleteMappedSchemeDetails(strSchemePlanCode, strExternalCode, strExternalType, createdDate, editedDate, deletedDate);
            }
            if (e.CommandName ==RadGrid.PerformInsertCommandName)
            {
                CustomerBo customerBo = new CustomerBo();
                bool isInserted = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtExtCode = (TextBox)e.Item.FindControl("txtExternalCodeForEditForm");
                DropDownList txtExtType = (DropDownList)e.Item.FindControl("ddlExternalType");
                TextBox txtSchemePlancode = (TextBox)e.Item.FindControl("txtSchemePlanCodeForEditForm");
                TextBox txtSchemePlanName = (TextBox)e.Item.FindControl("txtSchemePlanNameForEditForm");
                strSchemePlanCode = int.Parse(txtSchemePlancode.Text);
                strExternalCode = txtExtCode.Text;
                strExternalType = txtExtType.Text;
                createdDate = DateTime.Now;
                isInserted = customerBo.InsertProductAMCSchemeMappingDetalis(strSchemePlanCode, strExternalCode, strExternalType, createdDate, editedDate, deletedDate);
            }
            BindSchemePlanDetails();
         
        }
        protected void gvCamsKarvy_ItemCommand(object source, GridCommandEventArgs e)
        {
            //int strSchemePlanCode = 0;
            string TransactionHead = string.Empty;
            string TransactionDescription = string.Empty;
            string TransactionType = string.Empty;
            string TransactionTypeFlag = string.Empty;
            string TransactionClassificationCode = string.Empty;
            DisplayType = ddlExternalSource.SelectedValue;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                //strExternalCodeToBeEdited = Session["extCodeTobeEdited"].ToString();
                CustomerBo customerBo = new CustomerBo();
                bool isUpdated = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtHead = (TextBox)e.Item.FindControl("txtHead");
                DropDownList txtCode = (DropDownList)e.Item.FindControl("ddlclassificationCode");
                TextBox txtDescription = (TextBox)e.Item.FindControl("txtDescription");
                TextBox txtType = (TextBox)e.Item.FindControl("txttransactiontype");
                TextBox txtflag = (TextBox)e.Item.FindControl("txtsourceflag");
                string prevTransactionType = gvCamsKarvy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Transaction_Type"].ToString();
                string prevTransactionDescription = gvCamsKarvy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Description"].ToString();
                string prevTransactionHead = gvCamsKarvy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WKDTM_TransactionHead"].ToString();
                string prevTransactionTypeFlag = gvCamsKarvy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WKDTM_TransactionTypeFlag"].ToString();
                TransactionHead = txtHead.Text;
                TransactionDescription = txtDescription.Text;
                TransactionType = txtType.Text;
                TransactionTypeFlag = txtflag.Text;
                TransactionClassificationCode = txtCode.SelectedValue.ToString();
                isUpdated = customerBo.EditDataTranslateMappingDetalis(prevTransactionHead, prevTransactionDescription, prevTransactionType, prevTransactionTypeFlag,TransactionHead, TransactionDescription, TransactionType, TransactionTypeFlag, TransactionClassificationCode);

            }
            //if (e.CommandName == RadGrid.DeleteCommandName)
            //{
            //    bool isDeleted = false;
            //    customerBo = new CustomerBo();
            //    GridDataItem dataItem = (GridDataItem)e.Item;
            //    TableCell strSchemePlanCodeForDelete = dataItem["PASP_SchemePlanCode"];
            //    TableCell strSchemePlanNameForDelete = dataItem["PASP_SchemePlanName"];
            //    TableCell StrExternalCodeForDelete = dataItem["PASC_AMC_ExternalCode"];
            //    TableCell strExternalTypeForDelete = dataItem["PASC_AMC_ExternalType"];
            //    strSchemePlanCode = int.Parse(strSchemePlanCodeForDelete.Text);
            //    strExternalCode = StrExternalCodeForDelete.Text;
            //    strExternalType = strExternalTypeForDelete.Text;
            //    deletedDate = DateTime.Now;
            //    isDeleted = customerBo.DeleteMappedSchemeDetails(strSchemePlanCode, strExternalCode, strExternalType, createdDate, editedDate, deletedDate);
            //}
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                CustomerBo customerBo = new CustomerBo();
                bool isInserted = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtHead = (TextBox)e.Item.FindControl("txtHead");
                DropDownList txtCode = (DropDownList)e.Item.FindControl("ddlclassificationCode");
                TextBox txtDescription = (TextBox)e.Item.FindControl("txtDescription");
                TextBox txtType = (TextBox)e.Item.FindControl("txttransactiontype");
                TextBox txtflag = (TextBox)e.Item.FindControl("txtsourceflag");
                TransactionHead = txtHead.Text;
                TransactionDescription = txtDescription.Text;
                TransactionType = txtType.Text;
                TransactionTypeFlag = txtflag.Text;
                TransactionClassificationCode = txtCode.SelectedValue.ToString();
                isInserted = customerBo.InsertDataTranslateMappingDetalis(TransactionHead, TransactionDescription, TransactionType, TransactionTypeFlag, TransactionClassificationCode);
            }
            if (DisplayType == "KARVY")
            {
                BindDataTranslatorDetails();
                gvCamsKarvy.DataSource = dsDataTransDetails;
                gvCamsKarvy.DataBind();
            }
          if (DisplayType == "CAMS" || DisplayType == "Sundaram")
            {
                BindDataTranslatorDetails();
                 gvCams.DataSource = dsDataTransDetails;
                gvCams.DataBind();
            }
          if (DisplayType == "Templeton")
            {
                BindDataTranslatorDetails();
                gvTempleton.DataSource = dsDataTransDetails;
                gvTempleton.DataBind();
            }
            //BindDataTranslatorDetails();
     
           
        }
        protected void gvCams_ItemCommand(object source, GridCommandEventArgs e)
        {
                      
            string TransactionDescription = string.Empty;
            string TransactionType = string.Empty;
            string TransactionClassificationCode = string.Empty;
            DisplayType = ddlExternalSource.SelectedValue;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                //strExternalCodeToBeEdited = Session["extCodeTobeEdited"].ToString();
                CustomerBo customerBo = new CustomerBo();
                bool isUpdated = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;           
                DropDownList txtCode = (DropDownList)e.Item.FindControl("ddlclassificationCode");
                TextBox txtDescription = (TextBox)e.Item.FindControl("txtDescription");
                TextBox txtType = (TextBox)e.Item.FindControl("txttransactiontype");
                string prevTransactionType = gvCams.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Transaction_Type"].ToString();
                string prevTransactionDescription = gvCams.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Description"].ToString();
                TransactionDescription = txtDescription.Text;
                TransactionType = txtType.Text;               
                TransactionClassificationCode = txtCode.SelectedValue.ToString();
                isUpdated = customerBo.EditCamsDataTranslateMappingDetalis(prevTransactionType, prevTransactionDescription,TransactionType, TransactionDescription, TransactionClassificationCode);

            }
            //if (e.CommandName == RadGrid.DeleteCommandName)
            //{
            //    bool isDeleted = false;
            //    customerBo = new CustomerBo();
            //    GridDataItem dataItem = (GridDataItem)e.Item;
            //    TableCell strSchemePlanCodeForDelete = dataItem["PASP_SchemePlanCode"];
            //    TableCell strSchemePlanNameForDelete = dataItem["PASP_SchemePlanName"];
            //    TableCell StrExternalCodeForDelete = dataItem["PASC_AMC_ExternalCode"];
            //    TableCell strExternalTypeForDelete = dataItem["PASC_AMC_ExternalType"];
            //    strSchemePlanCode = int.Parse(strSchemePlanCodeForDelete.Text);
            //    strExternalCode = StrExternalCodeForDelete.Text;
            //    strExternalType = strExternalTypeForDelete.Text;
            //    deletedDate = DateTime.Now;
            //    isDeleted = customerBo.DeleteMappedSchemeDetails(strSchemePlanCode, strExternalCode, strExternalType, createdDate, editedDate, deletedDate);
            //}
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                CustomerBo customerBo = new CustomerBo();
                bool isInserted = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;               
                DropDownList txtCode = (DropDownList)e.Item.FindControl("ddlclassificationCode");
                TextBox txtDescription = (TextBox)e.Item.FindControl("txtDescription");
                TextBox txtType = (TextBox)e.Item.FindControl("txttransactiontype");
                TransactionDescription = txtDescription.Text;
                TransactionType = txtType.Text;               
                TransactionClassificationCode = txtCode.SelectedValue.ToString();
                isInserted = customerBo.InsertCamsDataTranslateMappingDetalis(TransactionType, TransactionDescription, TransactionClassificationCode);
            }
            if (DisplayType == "KARVY")
            {
                BindDataTranslatorDetails();
                gvCamsKarvy.DataSource = dsDataTransDetails;
                gvCamsKarvy.DataBind();
            }
            if (DisplayType == "CAMS" || DisplayType == "Sundaram")
            {
                BindDataTranslatorDetails();
                gvCams.DataSource = dsDataTransDetails;
                gvCams.DataBind();
            }
            if (DisplayType == "Templeton")
            {
                BindDataTranslatorDetails();
                gvTempleton.DataSource = dsDataTransDetails;
                gvTempleton.DataBind();
            }
            //BindDataTranslatorDetails();


        }

        protected void gvTempleton_ItemCommand(object source, GridCommandEventArgs e)
        {
           
            string TransactionType = string.Empty;
            string TransactionClassificationCode = string.Empty;
            DisplayType = ddlExternalSource.SelectedValue;
            
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                //strExternalCodeToBeEdited = Session["extCodeTobeEdited"].ToString();
                CustomerBo customerBo = new CustomerBo();
                bool isUpdated = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;           
                DropDownList txtCode = (DropDownList)e.Item.FindControl("ddlclassificationCode");
                TextBox txtType = (TextBox)e.Item.FindControl("txttransactiontype");
                string prevTransactionType = gvTempleton.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Transaction_Type"].ToString();         
                TransactionType = txtType.Text;               
                TransactionClassificationCode = txtCode.SelectedValue.ToString();
                isUpdated = customerBo.EditTempletonDataTranslateMappingDetalis(prevTransactionType,TransactionType, TransactionClassificationCode);

            }
            //if (e.CommandName == RadGrid.DeleteCommandName)
            //{
            //    bool isDeleted = false;
            //    customerBo = new CustomerBo();
            //    GridDataItem dataItem = (GridDataItem)e.Item;
            //    TableCell strSchemePlanCodeForDelete = dataItem["PASP_SchemePlanCode"];
            //    TableCell strSchemePlanNameForDelete = dataItem["PASP_SchemePlanName"];
            //    TableCell StrExternalCodeForDelete = dataItem["PASC_AMC_ExternalCode"];
            //    TableCell strExternalTypeForDelete = dataItem["PASC_AMC_ExternalType"];
            //    strSchemePlanCode = int.Parse(strSchemePlanCodeForDelete.Text);
            //    strExternalCode = StrExternalCodeForDelete.Text;
            //    strExternalType = strExternalTypeForDelete.Text;
            //    deletedDate = DateTime.Now;
            //    isDeleted = customerBo.DeleteMappedSchemeDetails(strSchemePlanCode, strExternalCode, strExternalType, createdDate, editedDate, deletedDate);
            //}
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                CustomerBo customerBo = new CustomerBo();
                bool isInserted = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;               
                DropDownList txtCode = (DropDownList)e.Item.FindControl("ddlclassificationCode");
                TextBox txtType = (TextBox)e.Item.FindControl("txttransactiontype");                          
                TransactionType = txtType.Text;               
                TransactionClassificationCode = txtCode.SelectedValue.ToString();
                isInserted = customerBo.InsertTempletonDataTranslateMappingDetalis(TransactionType, TransactionClassificationCode);
            }
            if (DisplayType == "KARVY")
            {
                BindDataTranslatorDetails();
                gvCamsKarvy.DataSource = dsDataTransDetails;
                gvCamsKarvy.DataBind();
            }
            if (DisplayType == "CAMS" || DisplayType == "Sundaram")
            {
                BindDataTranslatorDetails();
                gvCams.DataSource = dsDataTransDetails;
                gvCams.DataBind();
            }
            if (DisplayType == "Templeton")
            {
                BindDataTranslatorDetails();
                gvTempleton.DataSource = dsDataTransDetails;
                gvTempleton.DataBind();
            }
            //BindDataTranslatorDetails();

       
        }
        protected void gvSchemeDetails_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtGvSchemeDetails = new DataSet();
            dtGvSchemeDetails = (DataSet)Cache["gvSchemeDetailsForMappinginSuperAdmin"];
            if (dtGvSchemeDetails != null)
            {
                gvSchemeDetails.Visible = true;
                gvSchemeDetails.DataSource = dtGvSchemeDetails;
                
            }
        }

        protected void gvCamsKarvy_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtGvSchemeDetails = new DataSet();
            dtGvSchemeDetails = (DataSet)Cache["gvDataForMappinginSuperAdmin"];
            if (dtGvSchemeDetails != null)
            {
                gvCamsKarvy.Visible = true;
                gvCamsKarvy.DataSource = dtGvSchemeDetails;

            }
        }
        protected void gvCams_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtGvSchemeDetails = new DataSet();
            dtGvSchemeDetails = (DataSet)Cache["gvDataForMappinginSuperAdmin"];
            if (dtGvSchemeDetails != null)
            {
                gvCams.Visible = true;
                gvCams.DataSource = dtGvSchemeDetails;

            }
        }

        protected void gvTempleton_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtGvSchemeDetails = new DataSet();
            dtGvSchemeDetails = (DataSet)Cache["gvDataForMappinginSuperAdmin"];
            if (dtGvSchemeDetails != null)
            {
                gvTempleton.Visible = true;
                gvTempleton.DataSource = dtGvSchemeDetails;

            }
        }
        protected void gvSchemeDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
            customerBo = new CustomerBo();
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {   
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                TextBox txtBox = (TextBox)item.FindControl("txtSchemePlanCodeForEditForm");
                TextBox txtBoxScName = (TextBox)item.FindControl("txtSchemePlanNameForEditForm");
                //txtBox.Text = txtSchemePlanCode.Value;
                //txtBoxScName.Text = txtSchemeName.Text;
                txtBox.Enabled = false;
                txtBoxScName.Enabled = false;
                Button btn = (Button)item.FindControl("btnUpdate");
                btn.Text = "add";

            }
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string schemeName = dataItem["PASC_AMC_ExternalType"].Text;
                LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;               
                LinkButton buttonDelete = dataItem["deleteColumn"].Controls[0] as LinkButton;
                Label lbl = new Label();
                lbl = (Label)e.Item.FindControl("lblFiletypeId");
                if (schemeName == "AMFI" || schemeName == "ValueResearch" || schemeName == "AF")
                {
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonDelete.Attributes["onclick"] = "return alert('You cannot delete this scheme')";
                    buttonEdit.Attributes["onclick"] = "return alert('You cannot Edit this scheme')";
                }
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                string strExtType = gvSchemeDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PASC_AMC_ExternalType"].ToString();
                strExternalCodeToBeEdited = gvSchemeDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PASC_AMC_ExternalCode"].ToString();
                string IsOnline=gvSchemeDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PASC_IsOnline"].ToString();
                if (Session["extCodeTobeEdited"] != null)
                    Session["extCodeTobeEdited"] = null;
                Session["extCodeTobeEdited"] = strExternalCodeToBeEdited;
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList dropDownList = (DropDownList)editedItem.FindControl("ddlExternalType");
                dropDownList.SelectedValue = strExtType;
                DropDownList ddlONline = (DropDownList)editedItem.FindControl("ddlONline");
                if(IsOnline=="NO")
                 ddlONline.SelectedValue = 0.ToString();
                else
                    ddlONline.SelectedValue = 1.ToString();
            }
        }

        protected void gvCamsKarvy_ItemDataBound(object sender, GridItemEventArgs e)
        {
            customerBo = new CustomerBo();
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                DropDownList ddlclassificationCode = (DropDownList)item.FindControl("ddlclassificationCode");
                dsTransactionType = customerTransactionBo.GetMFTransactionType();
                ddlclassificationCode.DataValueField = "WMTT_TransactionClassificationCode";
                ddlclassificationCode.DataTextField = "WMTT_TransactionClassificationName";
                ddlclassificationCode.DataSource = dsTransactionType;
                ddlclassificationCode.DataBind();
                ddlclassificationCode.Items.Insert(0, new ListItem("Select", "Select"));

            }
            if (e.Item is GridDataItem)
            {
                //GridDataItem dataItem = e.Item as GridDataItem;           
                //LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                //LinkButton buttonDelete = dataItem["deleteColumn"].Controls[0] as LinkButton;
                //Label lbl = new Label();
                //lbl = (Label)e.Item.FindControl("lblFiletypeId");
                //if (schemeName == "AMFI" || schemeName == "ValueResearch" || schemeName == "AF")
                //{
                //    buttonEdit.Enabled = false;
                //    buttonDelete.Enabled = false;
                //    buttonDelete.Attributes["onclick"] = "return alert('You cannot delete this scheme')";
                //    buttonEdit.Attributes["onclick"] = "return alert('You cannot Edit this scheme')";
                //}
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                string strClassificationCode = gvCamsKarvy.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ClassificationCode"].ToString(); ;
                             
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList ddlclassificationCode = (DropDownList)editedItem.FindControl("ddlclassificationCode");
                dsTransactionType = customerTransactionBo.GetMFTransactionType();
                ddlclassificationCode.DataValueField = "WMTT_TransactionClassificationCode";
                ddlclassificationCode.DataTextField = "WMTT_TransactionClassificationName";
                ddlclassificationCode.DataSource = dsTransactionType;
                ddlclassificationCode.DataBind();
                ddlclassificationCode.SelectedValue=strClassificationCode;
               
            }
        }
        protected void gvCams_ItemDataBound(object sender, GridItemEventArgs e)
        {
            customerBo = new CustomerBo();
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                DropDownList ddlclassificationCode = (DropDownList)item.FindControl("ddlclassificationCode");
                dsTransactionType = customerTransactionBo.GetMFTransactionType();
                ddlclassificationCode.DataValueField = "WMTT_TransactionClassificationCode";
                ddlclassificationCode.DataTextField = "WMTT_TransactionClassificationName";
                ddlclassificationCode.DataSource = dsTransactionType;
                ddlclassificationCode.DataBind();
                ddlclassificationCode.Items.Insert(0, new ListItem("Select", "Select"));


            }
            if (e.Item is GridDataItem)
            {
                //GridDataItem dataItem = e.Item as GridDataItem;           
                //LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                //LinkButton buttonDelete = dataItem["deleteColumn"].Controls[0] as LinkButton;
                //Label lbl = new Label();
                //lbl = (Label)e.Item.FindControl("lblFiletypeId");
                //if (schemeName == "AMFI" || schemeName == "ValueResearch" || schemeName == "AF")
                //{
                //    buttonEdit.Enabled = false;
                //    buttonDelete.Enabled = false;
                //    buttonDelete.Attributes["onclick"] = "return alert('You cannot delete this scheme')";
                //    buttonEdit.Attributes["onclick"] = "return alert('You cannot Edit this scheme')";
                //}
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                string strClassificationCode = gvCams.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ClassificationCode"].ToString(); ;

                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList ddlclassificationCode = (DropDownList)editedItem.FindControl("ddlclassificationCode");
                dsTransactionType = customerTransactionBo.GetMFTransactionType();
                ddlclassificationCode.DataValueField = "WMTT_TransactionClassificationCode";
                ddlclassificationCode.DataTextField = "WMTT_TransactionClassificationName";
                ddlclassificationCode.DataSource = dsTransactionType;
                ddlclassificationCode.DataBind();
                ddlclassificationCode.SelectedValue = strClassificationCode;

            }
        }
        protected void gvTempleton_ItemDataBound(object sender, GridItemEventArgs e)
        {
            customerBo = new CustomerBo();
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                DropDownList ddlclassificationCode = (DropDownList)item.FindControl("ddlclassificationCode");
                dsTransactionType = customerTransactionBo.GetMFTransactionType();
                ddlclassificationCode.DataValueField = "WMTT_TransactionClassificationCode";
                ddlclassificationCode.DataTextField = "WMTT_TransactionClassificationName";
                ddlclassificationCode.DataSource = dsTransactionType;
                ddlclassificationCode.DataBind();
                ddlclassificationCode.Items.Insert(0, new ListItem("Select", "Select"));


            }
            if (e.Item is GridDataItem)
            {
                //GridDataItem dataItem = e.Item as GridDataItem;           
                //LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                //LinkButton buttonDelete = dataItem["deleteColumn"].Controls[0] as LinkButton;
                //Label lbl = new Label();
                //lbl = (Label)e.Item.FindControl("lblFiletypeId");
                //if (schemeName == "AMFI" || schemeName == "ValueResearch" || schemeName == "AF")
                //{
                //    buttonEdit.Enabled = false;
                //    buttonDelete.Enabled = false;
                //    buttonDelete.Attributes["onclick"] = "return alert('You cannot delete this scheme')";
                //    buttonEdit.Attributes["onclick"] = "return alert('You cannot Edit this scheme')";
                //}
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                string strClassificationCode = gvTempleton.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ClassificationCode"].ToString(); ;

                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList ddlclassificationCode = (DropDownList)editedItem.FindControl("ddlclassificationCode");
                dsTransactionType = customerTransactionBo.GetMFTransactionType();
                ddlclassificationCode.DataValueField = "WMTT_TransactionClassificationCode";
                ddlclassificationCode.DataTextField = "WMTT_TransactionClassificationName";
                ddlclassificationCode.DataSource = dsTransactionType;
                ddlclassificationCode.DataBind();
                ddlclassificationCode.SelectedValue = strClassificationCode;

            } 
        }
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    gvSchemeDetails.HeaderContextMenu.ItemClick += new RadMenuEventHandler(HeaderContextMenu_ItemClick);
        //    GridHeaderContextMenu ghcm = new GridHeaderContextMenu(gvSchemeDetails);
        //    int i = ghcm.Items.Count;
        //    RadMenuItem rmi = new RadMenuItem();
        //    RadMenuEventArgs rmie = new RadMenuEventArgs(rmi);
        //    HeaderContextMenu_ItemClick(sender, rmie);
        //}

        //protected void HeaderContextMenu_ItemClick(object sender, RadMenuEventArgs e)
        //{
        //    //GridHeaderContextMenu ghcm = new GridHeaderContextMenu(gvSchemeDetails);
        //    //ghcm.ClientID[1].ToString();
        //}

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            DataSet dtGvSchemeDetails = new DataSet();
            dtGvSchemeDetails = (DataSet)Cache["gvSchemeDetailsForMappinginSuperAdmin"];
            gvSchemeDetails.DataSource = dtGvSchemeDetails;
            gvSchemeDetails.ExportSettings.OpenInNewWindow = true;
            gvSchemeDetails.ExportSettings.IgnorePaging = true;
            gvSchemeDetails.ExportSettings.HideStructureColumns = true;
            gvSchemeDetails.ExportSettings.ExportOnlyData = true;
            gvSchemeDetails.ExportSettings.FileName = "Scheme Mapping Details";
            gvSchemeDetails.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvSchemeDetails.MasterTableView.ExportToExcel();
        }

        protected void gvSchemeDetails_PreRender(object sender, EventArgs e)
        {
            if (ViewState["OnlineSchemeSetupSchemecode"] != null)
                schemePlanCode = Convert.ToInt32(ViewState["OnlineSchemeSetupSchemecode"]);
            
            DataSet dsCustomerList = new DataSet();
            dsCustomerList = (DataSet)Cache["gvSchemeDetailsForMappinginSuperAdmin"];
            DataTable dtCustomerList = new DataTable();
            dtCustomerList = dsCustomerList.Tables[0];

            if (!IsPostBack)
            {
                gvSchemeDetails.MasterTableView.FilterExpression = "([PASP_SchemePlanCode] = \'" + schemePlanCode + "\') ";
                GridColumn column = gvSchemeDetails.MasterTableView.GetColumnSafe("PASP_SchemePlanCode");
                column.CurrentFilterFunction = GridKnownFunction.Contains;
                column.CurrentFilterValue = Convert.ToInt32(schemePlanCode).ToString();
                DataRow[] rows = dtCustomerList.Select(gvSchemeDetails.MasterTableView.FilterExpression.ToString());
                gvSchemeDetails.MasterTableView.Rebind();

                ddlSchemeMappingType.Enabled = false;
            }

            //if (gvSchemeDetails.MasterTableView.GetItems(GridItemType.CommandItem).Length > 0)
            //{
                //GridCommandItem commandItem = gvSchemeDetails.MasterTableView.GetItems(GridItemType.CommandItem)[0] as GridCommandItem;
                //Button btn = (Button)commandItem.FindControl("btnUpdate");
                //btn.Text = "add";
                //commandItem.FindControl("InitInsertButton").Visible = false;
            //}
            //Button buttonAddMaping = (Button)sender;
            //GridEditFormItem filterItem = (GridEditFormItem)gvSchemeDetails.MasterTableView.GetItems(GridItemType.EditFormItem)[2];
            //Button btn = (Button)filterItem.FindControl("btnUpdate");


            //btn.Text = "Add Mapping";

        }
      }
    }
