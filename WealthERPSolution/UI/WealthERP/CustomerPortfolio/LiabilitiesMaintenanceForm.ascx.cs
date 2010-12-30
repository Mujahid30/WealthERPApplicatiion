using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using System.Configuration;
using System.Data;
using BoCustomerProfiling;
using VoUser;
using VoCustomerProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCalculator;
using WealthERP.Base;
using System.Collections;


namespace WealthERP.CustomerPortfolio
{
    public partial class LiabilitiesMaintenanceForm : System.Web.UI.UserControl
    {
        string path = string.Empty;
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        LiabilitiesBo liabilitiesBo = new LiabilitiesBo();
        LiabilitiesVo liabilityVo = new LiabilitiesVo();
        UserVo userVo = new UserVo();
        string menu;
        DataTable dtCoBorrower = new DataTable();
        DataTable dtLiabilityAssociates = new DataTable();
        DataTable dtEditLogInfo = new DataTable();
        DataTable dtTempCoBorrower = new DataTable();
        PropertyVo propertyVo = new PropertyVo();
        DataTable dtAssetOwnership = new DataTable();
        PropertyBo propertyBo = new PropertyBo();
        Calculator calculator = new Calculator();
        RMVo rmVo = new RMVo();
        float CB_Share = 0;
        CustomerFamilyVo familyVo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                userVo = (UserVo)Session[SessionContents.UserVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];
                //CompareValidator5.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
                if (Session["propertyVo"] != null && Session["test"] != null)
                {
                    propertyVo = (PropertyVo)Session["propertyVo"];
                    // dtCoBorrower = liabilitiesBo.GetPropertyAccountAssociates(propertyVo.PropertyId);
                }
                //else
                //{
                dtCoBorrower = liabilitiesBo.GetCustomerAssociates(customerVo.CustomerId);
                //string x = ((TextBox)(upnlLiabilities.FindControl("txtNoCoBorrowers"))).Text;
                //if (!String.IsNullOrEmpty(Request.Params["ctrl_LiabilitiesMaintenanceForm$txtNoCoBorrowers"]))
                //{
                //    if (int.Parse(Request.Params["ctrl_LiabilitiesMaintenanceForm$txtNoCoBorrowers"]) == 0)
                //        gvCoBorrower.Visible = false;
                //}
                //else
                //    gvCoBorrower.Visible = true;
                // BindAssetsDropDown();
                if (!IsPostBack)
                {
                    if (Session["menu"] != null)
                    {
                        liabilityVo = (LiabilitiesVo)Session["liabilityVo"];
                        if (liabilityVo.LoanTypeCode.ToString() == "1")
                        {

                            dtAssetOwnership = liabilitiesBo.GetAssetOwnerShip(liabilityVo.LiabilitiesId);
                            if (dtAssetOwnership != null)
                            {
                                for (int i = 0; i < dtAssetOwnership.Rows.Count; i++)
                                {
                                    if (dtAssetOwnership.Rows[i]["XLAT_LoanAssociateCode"].ToString() == "CB")
                                        CB_Share = float.Parse(dtAssetOwnership.Rows[i]["Share"].ToString()) + CB_Share;
                                }
                            }
                        }
                        Session.Remove("personalVo");
                        Session.Remove("propertyVo");
                        //  trAssets.Visible = false;
                        //gvCoBorrower.Visible = false;
                        if (Session["menu"].ToString() == "View")
                        {
                            trUpdate.Visible = false;
                            trSubmit.Visible = false;
                            trEdit.Visible = true;
                            trBack.Visible = true;
                            //btnCoborrowers.Visible = false;
                            ViewLiability();
                        }
                        else if (Session["menu"].ToString() == "Edit")
                        {
                            trUpdate.Visible = true;
                            trSubmit.Visible = false;
                            trEdit.Visible = false;
                            trBack.Visible = true;
                            //btnCoborrowers.Visible = true;
                            EditLiability();
                        }
                    }
                    else
                    {

                        trUpdate.Visible = false;
                        trSubmit.Visible = true;
                        //trExistingAssets.Visible = false;
                        // trLAP.Visible = false;
                        trEdit.Visible = false;
                        // trLAPError.Visible = false;
                        // trLAPError.Visible = false;
                        // trLAPTitle.Visible = false;
                        // trAddAlterations.Visible = false;
                        // trLoanExceptions.Visible = false;
                        // trLoanExceptionsTitle.Visible = false;
                        // Session.Remove("propertyVo");
                        BindDropDowns();
                        //BindAllClientsDropDownForGuarantor();
                    }
                }
                RestorePreviousState();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesMaintenanceForm.ascx.cs:Page_Load()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        //private void BindAllClientsDropDownForGuarantor()
        //{
        //    DataSet ds = new DataSet();
        //    LiabilitiesBo liabilitiesBo = new LiabilitiesBo();

        //    ds = liabilitiesBo.GetRMClientList(rmVo.RMId);

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        ddlGuarantor.DataSource = ds.Tables[0];
        //        ddlGuarantor.DataTextField = "CustomerName";
        //        ddlGuarantor.DataValueField = "C_CustomerId";
        //        ddlGuarantor.DataBind();
        //        ddlGuarantor.Items.Insert(0, new ListItem("Select a Guarantor", "Select a Guarantor"));
        //    }
        //    else
        //    {
        //        ddlGuarantor.Items.Clear();
        //    }
        //}

        protected void BindDropDowns()
        {
            try
            {
                // BINDING LOAN TYPE

                DataTable dt = XMLBo.GetLoanType(path);
                ddlLoanType.DataSource = dt;
                ddlLoanType.DataTextField = dt.Columns["XLT_LoanType"].ToString();
                ddlLoanType.DataValueField = dt.Columns["XLT_LoanTypeCode"].ToString();
                ddlLoanType.DataBind();
                ddlLoanType.Items.Insert(0, new ListItem("Select Loan Type", "Select Loan Type"));
                if (Session["propertyVo"] != null)
                {
                    ddlLoanType.SelectedValue = "1";
                }
                else if (Session["personalVo"] != null)
                {
                    ddlLoanType.SelectedValue = "2";
                }
                // BINDING LENDERS

                DataTable dtLender = XMLBo.GetLoanPartner(path);
                ddlLender.DataSource = dtLender;
                ddlLender.DataTextField = dtLender.Columns["XLP_LoanPartner"].ToString();
                ddlLender.DataValueField = dtLender.Columns["XLP_LoanPartnerCode"].ToString();
                ddlLender.DataBind();
                ddlLender.Items.Insert(0, new ListItem("Select Lender", "Select Lender"));

                // BINDING REPAYMENT TYPE

                DataTable dtRepayment = XMLBo.GetInstallmentType(path);
                ddlRepaymentType.DataSource = dtRepayment;
                ddlRepaymentType.DataTextField = dtRepayment.Columns["XIT_InstallmentType"].ToString();
                ddlRepaymentType.DataValueField = dtRepayment.Columns["XIT_InstallmentTypeCode"].ToString();
                ddlRepaymentType.DataBind();
                ddlRepaymentType.Items.Insert(0, new ListItem("Select the Installment Type", "Select the Installment Type"));

                // BINDING Payment Option

                DataTable dtPaymentOption = XMLBo.GetPaymentOption(path);
                ddlPaymentOption.DataSource = dtPaymentOption;
                ddlPaymentOption.DataTextField = dtPaymentOption.Columns["XPO_PaymentOption"].ToString();
                ddlPaymentOption.DataValueField = dtPaymentOption.Columns["XPO_PaymentOptionCode"].ToString();
                ddlPaymentOption.DataBind();
                ddlPaymentOption.Items.Insert(0, new ListItem("Select the Payment Option", "Select the Payment Option"));

                // BINDING Compound FREQUENCY

                DataTable dtCompoundFrequency = XMLBo.GetFrequency(path);
                for (int i = 0; i < dtCompoundFrequency.Rows.Count; i++)
                {
                    if (dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "MN" && dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "YR" && dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "QT" && dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "HY")
                    {
                        dtCompoundFrequency.Rows[i].Delete();
                        i--;
                    }
                }
                ddlCompoundFrequency.DataSource = dtCompoundFrequency;
                ddlCompoundFrequency.DataTextField = dtCompoundFrequency.Columns["Frequency"].ToString();
                ddlCompoundFrequency.DataValueField = dtCompoundFrequency.Columns["FrequencyCode"].ToString();
                ddlCompoundFrequency.DataBind();
                ddlCompoundFrequency.Items.Insert(0, new ListItem("Select the Frequency", "Select the Frequency"));

                // BINDING EMI FREQUENCY

                DataTable dtFrequency = XMLBo.GetFrequency(path);
                for (int i = 0; i < dtFrequency.Rows.Count; i++)
                {
                    if (dtFrequency.Rows[i]["FrequencyCode"].ToString() != "MN" && dtFrequency.Rows[i]["FrequencyCode"].ToString() != "YR" && dtFrequency.Rows[i]["FrequencyCode"].ToString() != "QT" && dtFrequency.Rows[i]["FrequencyCode"].ToString() != "HY")
                    {
                        dtFrequency.Rows[i].Delete();
                        i--;
                    }
                }
                ddlEMIFrequency.DataSource = dtFrequency;
                ddlEMIFrequency.DataTextField = dtFrequency.Columns["Frequency"].ToString();
                ddlEMIFrequency.DataValueField = dtFrequency.Columns["FrequencyCode"].ToString();
                ddlEMIFrequency.DataBind();
                ddlEMIFrequency.Items.Insert(0, new ListItem("Select the Frequency", "Select the Frequency"));

                // BINDING GUARANTOR


                //DataTable dtGuarantor = customerFamilyBo.GetCustomerAssociates(customerVo.CustomerId);
                //if (dtGuarantor != null)
                //{
                //    ddlGuarantor.DataSource = dtGuarantor;
                //    ddlGuarantor.DataValueField = dtGuarantor.Columns["CA_AssociationId"].ToString();
                //    ddlGuarantor.DataTextField = dtGuarantor.Columns["CustomerName"].ToString();
                //    ddlGuarantor.DataBind();
                //    ddlGuarantor.Items.Insert(0, new ListItem("Select the Guarantor", "Select the Guarantor"));
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
                FunctionInfo.Add("Method", "LiabilitiesMaintenanceForm.ascx.cs:BindDropDowns()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = customerVo.CustomerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoanTypeChange();
        }

        //protected void ddlExistingAssets_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int propertyId;
        //    if (ddlExistingAssets.SelectedValue != "Pick One")
        //    {
        //        propertyId = int.Parse(ddlExistingAssets.SelectedValue.ToString());
        //        // Set the VO into the Session
        //        propertyVo = propertyBo.GetPropertyAsset(propertyId);
        //        Session["propertyVo"] = propertyVo;
        //    }
        //}

        private void LoanTypeChange()
        {
            try
            {
                if (ddlLoanType.SelectedItem.Value.ToString() == "1" || ddlLoanType.SelectedItem.Value.ToString() == "2")
                {
                    //trAssets.Visible = true;
                    //btnAddAsset0.Visible = true;
                    //trExistingAssets.Visible = true;
                    //BindAssetsDropDown();
                    //dictAssets = propertyBo.GetPropertyDropDown(ddlClientName.SelectedValue, custBorrowerIds);

                    //ddlExistingAssets.DataSource = dictAssets;
                    //ddlExistingAssets.DataTextField = "Value";
                    //ddlExistingAssets.DataValueField = "Key";
                    //ddlExistingAssets.DataBind();
                    //ddlExistingAssets.Items.Insert(0, new ListItem("Pick One", "Pick One"));
                }

                else
                {
                    //btnAddAsset0.Visible = false;
                    // trExistingAssets.Visible = false;
                }
                if (ddlLoanType.SelectedItem.Value.ToString() == "3" || ddlLoanType.SelectedItem.Value.ToString() == "4")
                {
                    // trLAP.Visible = true;
                    //trLAPTitle.Visible = true;
                    //trLAPError.Visible = false;
                    //BindLAPCheckList();
                }
                else
                {
                    // trLAP.Visible = false;
                    // trLAPTitle.Visible = false;
                    // trLAPError.Visible = false;
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
                FunctionInfo.Add("Method", "LiabilitiesMaintenanceForm.ascx.cs:ddlLoanType_SelectedIndexChanged()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        //private void BindAssetDropDown()
        //{
        //    dictAssets = propertyBo.GetPropertyDropDown(ddlClientName.SelectedValue, custBorrowerIds);

        //    ddlExistingAssets.DataSource = dictAssets;
        //    ddlExistingAssets.DataTextField = "Value";
        //    ddlExistingAssets.DataValueField = "Key";
        //    ddlExistingAssets.DataBind();
        //    ddlExistingAssets.Items.Insert(0, new ListItem("Pick One", "Pick One"));
        //}

        //protected void BindLAPCheckList()
        //{
        //    DataTable dtProperty = null;
        //    PropertyBo propertyBo = new PropertyBo();
        //    DataTable dtExistingLiaAsset = null;
        //    int loanType;
        //    try
        //    {
        //        if (Session["menu"] == null)
        //        {
        //            loanType = int.Parse(ddlLoanType.SelectedItem.Value.ToString());
        //        }
        //        else
        //        {
        //            liabilityVo = (LiabilitiesVo)Session["liabilityVo"];
        //            loanType = liabilityVo.LoanTypeCode;
        //        }
        //        if (loanType.ToString() == "3")
        //        {
        //            //chkLstAsset.Items.Clear();
        //            dtProperty = propertyBo.GetLoanAgainstProperty(customerVo.CustomerId);
        //            if (dtProperty != null)
        //            {
        //                for (int i = 0; i < dtProperty.Rows.Count; i++)
        //                {

        //                    //chkLstAsset.Items.Add(new ListItem(dtProperty.Rows[i]["CPNP_Name"].ToString() + "&nbsp;&nbsp;&nbsp;", dtProperty.Rows[i]["CPNP_PropertyNPId"].ToString()));
        //                }
        //                if (Session["menu"] != null)
        //                {
        //                   // trLAPError.Visible = false;
        //                    liabilityVo = (LiabilitiesVo)Session["liabilityVo"];
        //                    dtExistingLiaAsset = liabilitiesBo.GetLiabilityAssetAssociation(liabilityVo.LiabilitiesId);
        //                    if (dtExistingLiaAsset != null)
        //                    {
        //                        int j = 0;
        //                        for (int i = 0; i < dtProperty.Rows.Count; i++)
        //                        {
        //                            if (j < dtExistingLiaAsset.Rows.Count)
        //                            {
        //                                if (int.Parse(chkLstAsset.Items[i].Value.ToString()) == int.Parse(dtExistingLiaAsset.Rows[j]["CLAA_AssetId"].ToString()))
        //                                {
        //                                    chkLstAsset.Items[i].Selected = true;
        //                                    j = j + 1;
        //                                }
        //                            }
        //                        }

        //                    }
        //                }
        //            }
        //            else
        //            {
        //                trLAP.Visible = false;
        //                trLAPTitle.Visible = true;
        //                trLAPError.Visible = true;
        //            }
        //        }
        //        else
        //        {
        //            trLAP.Visible = false;
        //            trLAPTitle.Visible = true;
        //            trLAPError.Visible = true;
        //        }


        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "LiabilitiesMaintenanceForm.ascx.cs:BindLAPCheckList()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        // BINDING CO BORROWER DROP DOWN IN GRID VIEW

        //protected DataTable BindGridDroDown()
        //{
        //    DropDownList ddl1 = new DropDownList();

        //    foreach (GridViewRow gvr in gvCoBorrower.Rows)
        //    {
        //        if ((DropDownList)gvr.FindControl("ddlCoBorrowers") != null)
        //        {
        //            ddl1 = (DropDownList)gvr.FindControl("ddlCoBorrowers");
        //            ddl1.DataSource = dtCoBorrower;
        //            ddl1.DataValueField = dtCoBorrower.Columns["AssociationId"].ToString();
        //            ddl1.DataTextField = dtCoBorrower.Columns["AssociateName"].ToString();
        //            ddl1.DataBind();
        //            ddl1.Items.Insert(0, new ListItem("Select", "Select"));
        //        }
        //    }
        //    return dtCoBorrower;
        //}

        //protected void btnAddAlterations_Click(object sender, EventArgs e)
        //{
        //    trLoanExceptions.Visible = true;
        //    BindExceptionGrid();

        //}

        //private void BindExceptionGrid()
        //{

        //    DataTable tempDt = new DataTable();
        //    DataRow dr;
        //    LiabilitiesVo liabilitiesVo = new LiabilitiesVo();

        //    try
        //    {
        //        trLoanExceptions.Visible = true;
        //        liabilitiesVo = (LiabilitiesVo)Session["liabilityVo"];
        //        dtEditLogInfo = liabilitiesBo.GetEditLogInfo(liabilitiesVo.LiabilitiesId);
        //        if (dtEditLogInfo == null)
        //        {
        //            tempDt.Rows.Add(tempDt.NewRow());
        //            gvAddAlterations.DataSource = tempDt;
        //            gvAddAlterations.DataBind();
        //            int columnsCount = gvAddAlterations.Columns.Count;
        //            gvAddAlterations.Rows[0].Cells.Clear();// clear all the cells in the row
        //            gvAddAlterations.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
        //            gvAddAlterations.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell
        //            gvAddAlterations.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //            gvAddAlterations.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
        //            gvAddAlterations.Rows[0].Cells[0].Font.Bold = true;
        //            gvAddAlterations.Rows[0].Cells[0].Text = "NO RESULT FOUND!";
        //        }
        //        else
        //        {

        //            tempDt.Columns.Add("SI_No");
        //            tempDt.Columns.Add("ExceptionType");
        //            tempDt.Columns.Add("Date");
        //            tempDt.Columns.Add("Amount");
        //            tempDt.Columns.Add("ReferenceNumber");
        //            tempDt.Columns.Add("Remark");
        //            for (int i = 0; i < dtEditLogInfo.Rows.Count; i++)
        //            {
        //                dr = tempDt.NewRow();
        //                dr[0] = dtEditLogInfo.Rows[i]["CLEL_LiabilitiesEditLogId"].ToString();
        //                dr[1] = dtEditLogInfo.Rows[i]["XLET_EditTypeName"].ToString();
        //                dr[2] = dtEditLogInfo.Rows[i]["CLEL_EditOccurenceDate"].ToString();
        //                dr[3] = dtEditLogInfo.Rows[i]["CLEL_EditAmount"].ToString();
        //                dr[4] = dtEditLogInfo.Rows[i]["CLEL_ReferenceNum"].ToString();
        //                dr[5] = dtEditLogInfo.Rows[i]["CLEL_Remark"].ToString();
        //                tempDt.Rows.Add(dr);
        //            }

        //            gvAddAlterations.DataSource = tempDt;
        //            gvAddAlterations.DataBind();
        //        }
        //        gvAddAlterations.Visible = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "LiabilitiesMaintenanceForm.ascx.cs:txtNoCoBorrowers_TextChanged()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //protected void gvAddAlterations_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (dtEditLogInfo != null)
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            GridViewRow gvr = e.Row;

        //            Label lblAmount = e.Row.FindControl("lblAmount") as Label;
        //            Label lblRefNo = e.Row.FindControl("lblRefNo") as Label; ;
        //            Label lblSerialNo = e.Row.FindControl("lblSerialNo") as Label;
        //            Label lblExceptionType = e.Row.FindControl("lblExceptionType") as Label;
        //            Label lblDate = e.Row.FindControl("lblDate") as Label;
        //            Label lblRemark = e.Row.FindControl("lblRemark") as Label;


        //            if (lblAmount != null)
        //            {
        //                lblAmount.Text = dtEditLogInfo.Rows[gvr.RowIndex]["CLEL_EditAmount"].ToString();

        //            } if (lblRefNo != null)
        //            {
        //                lblRefNo.Text = dtEditLogInfo.Rows[gvr.RowIndex]["CLEL_ReferenceNum"].ToString();

        //            } if (lblSerialNo != null)
        //            {
        //                lblSerialNo.Text = dtEditLogInfo.Rows[gvr.RowIndex]["CLEL_LiabilitiesEditLogId"].ToString();

        //            }
        //            if (lblExceptionType != null)
        //            {
        //                lblExceptionType.Text = dtEditLogInfo.Rows[gvr.RowIndex]["XLET_EditTypeName"].ToString();

        //            }
        //            if (lblDate != null)
        //            {
        //                lblDate.Text = dtEditLogInfo.Rows[gvr.RowIndex]["CLEL_EditOccurenceDate"].ToString();

        //            }
        //            if (lblRemark != null)
        //            {
        //                lblRemark.Text = dtEditLogInfo.Rows[gvr.RowIndex]["CLEL_Remark"].ToString();

        //            }
        //        }
        //    }
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        DropDownList ddlAddExceptionType;

        //        ddlAddExceptionType = e.Row.FindControl("ddlExceptionType") as DropDownList;
        //        if (ddlAddExceptionType != null)
        //        {
        //            ddlAddExceptionType.DataSource = XMLBo.GetEditType(path);
        //            ddlAddExceptionType.DataTextField = "XLET_EditTypeName";
        //            ddlAddExceptionType.DataValueField = "XLET_EditTypeCode";
        //            ddlAddExceptionType.DataBind();
        //            ddlAddExceptionType.Items.Insert(0, "Select the Exception Type");
        //        }
        //    }
        //}

        //protected int GridViewValidation()
        //{
        //    float LiabilityShare = 0;
        //    float MarginPer = 0;
        //    if (gvCoBorrower.Visible == true)
        //    {
        //        foreach (GridViewRow gvr in gvCoBorrower.Rows)
        //        {
        //            if ((DropDownList)gvr.FindControl("ddlCoBorrowers") != null)
        //            {
        //                DropDownList ddlCB = (DropDownList)gvr.FindControl("ddlCoBorrowers");
        //                if (ddlCB.SelectedItem.Text == "Select" && gvr.RowIndex != 0)
        //                {
        //                    return 3;
        //                }
        //            }
        //            if ((TextBox)gvr.FindControl("txtLoanObligation") != null)
        //            {
        //                TextBox txtAsset = (TextBox)gvr.FindControl("txtLoanObligation");
        //                LiabilityShare = LiabilityShare + float.Parse(txtAsset.Text);
        //            }
        //            if ((TextBox)gvr.FindControl("txtMargin") != null)
        //            {
        //                TextBox txtMargin = (TextBox)gvr.FindControl("txtMargin");
        //                MarginPer = MarginPer + float.Parse(txtMargin.Text);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        LiabilityShare = 100;
        //        MarginPer = 100;
        //    }

        //    if (LiabilityShare == 100 && MarginPer == 100)
        //    {
        //        return 1;
        //    }
        //    else
        //        return 2;

        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LiabilitiesVo liabilitiesVo;
            LiabilityAssociateVo liabilityAssociateVo = new LiabilityAssociateVo();
            //AssetAssociationVo assetAssociationVo;
            int LiabilityId = 0;
            //int gvValidationResult;
            CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
            CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
            PropertyVo propertyVo = new PropertyVo();
            CustomerAccountBo customerAccountBo = new CustomerAccountBo();
            PropertyBo propertyBo = new PropertyBo();
            try
            {
                //gvValidationResult = GridViewValidation();
                //if (gvValidationResult == 1)
                //{
                liabilitiesVo = new LiabilitiesVo();
                //liabilitiesVo.AmountPrepaid = double.Parse(txtAmountPrepaid.Text);
                liabilitiesVo.CommissionAmount = 0;
                liabilitiesVo.CommissionPer = 0;
                liabilitiesVo.CreatedBy = userVo.UserId;
                if(txtEMIAmount.Text!="")
                liabilitiesVo.EMIAmount = double.Parse(txtEMIAmount.Text);
                //liabilitiesVo.EMIDate = int.Parse(ddl.SelectedItem.Value.ToString());
                if (ddlEMIFrequency.SelectedItem.Value.ToString() != "Select the Frequency")
                    liabilitiesVo.FrequencyCodeEMI = ddlEMIFrequency.SelectedItem.Value.ToString();
                if (ddlCompoundFrequency.SelectedItem.Value.ToString() != "Select the Frequency")
                    liabilitiesVo.CompoundFrequency = ddlCompoundFrequency.SelectedItem.Value.ToString();
                if (ddlPaymentOption.SelectedItem.Value.ToString() != "Select the Payment Option")
                    liabilitiesVo.PaymentOptionCode = int.Parse(ddlPaymentOption.SelectedItem.Value.ToString());
                if (txtLoanOutstandingAmount.Text != "")
                    liabilitiesVo.OutstandingAmount = double.Parse(txtLoanOutstandingAmount.Text.ToString());
                if (txtLoanStartDate.Text != "")
                    liabilitiesVo.LoanStartDate = DateTime.Parse(txtLoanStartDate.Text);
                if (txtInstallmentEndDt.Text != "")
                    liabilitiesVo.InstallmentEndDate = DateTime.Parse(txtInstallmentEndDt.Text);
                if (txtInstallmentStartDt.Text != "")
                    liabilitiesVo.InstallmentStartDate = DateTime.Parse(txtInstallmentStartDt.Text);
                liabilitiesVo.OtherLenderName = txtOtherLender.Text;
                double lrAmount = 0;
                bool lrResult = Double.TryParse(txtLumpsumRepaymentAmount.Text, out lrAmount);
                liabilitiesVo.LumpsumRepaymentAmount = lrAmount;

                //if (rbtnFloatYes.Checked)
                //{
                //    liabilitiesVo.IsFloatingRateInterest = 1;
                //}
                //else
                //{
                //    liabilitiesVo.IsFloatingRateInterest = 0;
                //}
                liabilitiesVo.IsInProcess = 0;
                liabilitiesVo.LoanAmount = double.Parse(txtLoanAmount.Text);
                liabilitiesVo.LoanPartnerCode = int.Parse(ddlLender.SelectedItem.Value.ToString());
                liabilitiesVo.LoanTypeCode = int.Parse(ddlLoanType.SelectedItem.Value.ToString());
                liabilitiesVo.ModifiedBy = userVo.UserId;
                if(txtNoOfInstallments.Text!="")
                    liabilitiesVo.NoOfInstallments = int.Parse(txtNoOfInstallments.Text);
                liabilitiesVo.RateOfInterest = float.Parse(txtInterestRate.Text);
                if (ddlRepaymentType.SelectedItem.Value.ToString() != "Select the Installment Type")
                    liabilitiesVo.InstallmentTypeCode = int.Parse(ddlRepaymentType.SelectedItem.Value.ToString());
                liabilitiesVo.Guarantor = txtGuarantor.Text;
                int noOfYears = 0;
                int noOfMonths = 0;
                int.TryParse(txtTenture.Text, out noOfYears);
                int.TryParse(txtTenureMonths.Text, out noOfMonths);
                
                    liabilitiesVo.Tenure = (noOfYears * 12) + noOfMonths;
                

                LiabilityId = liabilitiesBo.CreateLiabilities(liabilitiesVo);
                //if (liabilitiesVo.LoanTypeCode.ToString() == "1")
                //{
                //    propertyVo = (PropertyVo)Session["propertyVo"];
                //}
                //if (LiabilityId != 0 && (liabilitiesVo.LoanTypeCode.ToString() == "3" || liabilitiesVo.LoanTypeCode.ToString() == "4" || liabilitiesVo.LoanTypeCode.ToString() == "1" || liabilitiesVo.LoanTypeCode.ToString() == "2"))
                //{
                //    if (liabilitiesVo.LoanTypeCode.ToString() == "3" || liabilitiesVo.LoanTypeCode.ToString() == "4")
                //    {
                //        for (int i = 0; i < chkLstAsset.Items.Count; i++)
                //        {
                //            if (chkLstAsset.Items[i].Selected)
                //            {
                //                assetAssociationVo = new AssetAssociationVo();
                //                if (liabilitiesVo.LoanTypeCode.ToString() == "3")
                //                {
                //                    assetAssociationVo.AssetGroupCode = "PR";
                //                    assetAssociationVo.AssetTable = "CustomerPropertyNetPosition";
                //                }
                //                else if (liabilitiesVo.LoanTypeCode.ToString() == "4")
                //                {
                //                    assetAssociationVo.AssetGroupCode = "EQ";
                //                    assetAssociationVo.AssetTable = "CustomerEquityNetPosition";
                //                }
                //                assetAssociationVo.AssetId = int.Parse(chkLstAsset.Items[i].Value.ToString());
                //                assetAssociationVo.CreatedBy = userVo.UserId;
                //                assetAssociationVo.ModifiedBy = userVo.UserId;
                //                assetAssociationVo.LiabilitiesId = LiabilityId;
                //                liabilitiesBo.CreatLiabilityAssetAssociation(assetAssociationVo);
                //                //{

                //                //    propertyVo=propertyBo.GetPropertyAsset(assetAssociationVo.AssetId);
                //                //    customerAccountVo = customerAccountBo.GetCustomerPropertyAccount(propertyVo.AccountId);
                //                //    customerAccountAssociationVo.AccountId = customerAccountVo.AccountId;
                //                //    customerAccountAssociationVo.AssociationId=
                //                //    customerAccountBo.CreatePropertyAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                //                //}


                //            }
                //        }
                //    }
                //    else if (liabilitiesVo.LoanTypeCode.ToString() == "1" || liabilitiesVo.LoanTypeCode.ToString() == "2")
                //    {
                //        assetAssociationVo = new AssetAssociationVo();
                //        if (liabilitiesVo.LoanTypeCode.ToString() == "1")
                //        {
                //            assetAssociationVo.AssetGroupCode = "PR";
                //            assetAssociationVo.AssetTable = "CustomerPropertyNetPosition";
                //            if (Session["propertyVo"] != null)
                //            {
                //                assetAssociationVo.AssetId = propertyVo.PropertyId;
                //            }
                //            else
                //            {
                //                assetAssociationVo.AssetId = int.Parse(ddlExistingAssets.SelectedValue.ToString());
                //            }
                //        }
                //        else if (liabilitiesVo.LoanTypeCode.ToString() == "2")
                //        {
                //            assetAssociationVo.AssetGroupCode = "PI";
                //            assetAssociationVo.AssetTable = "CustomerPersonalNetPosition";

                //        }

                //        assetAssociationVo.CreatedBy = userVo.UserId;
                //        assetAssociationVo.ModifiedBy = userVo.UserId;
                //        assetAssociationVo.LiabilitiesId = LiabilityId;
                //        liabilitiesBo.CreatLiabilityAssetAssociation(assetAssociationVo);
                //    }

                //}
                if (LiabilityId != 0)
                {
                    //if (gvCoBorrower.Visible == true && gvCoBorrower.Rows.Count > 0)
                    //{
                    //    foreach (GridViewRow gvr in gvCoBorrower.Rows)
                    //    {
                    //        liabilityAssociateVo = new LiabilityAssociateVo();
                    //        if (gvr.RowIndex != 0)
                    //        {
                    //            if ((DropDownList)gvr.FindControl("ddlCoBorrowers") != null)
                    //            {
                    //                DropDownList ddl1 = (DropDownList)gvr.FindControl("ddlCoBorrowers");
                    //                familyVo = new CustomerFamilyVo();
                    //                if (ddl1.SelectedValue != "Select")
                    //                    familyVo = customerFamilyBo.GetCustomerFamilyAssociateDetails(int.Parse(ddl1.SelectedValue.ToString()));
                    //                liabilityAssociateVo.AssociationId = int.Parse(familyVo.AssociationId.ToString());
                    //                liabilityAssociateVo.LoanAssociateCode = "CB";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if ((Label)gvr.FindControl("lblMainCustomer") != null)
                    //            {
                    //                Label lblMainCustomer = (Label)gvr.FindControl("lblMainCustomer");
                    //                liabilityAssociateVo.LoanAssociateCode = "MC";
                    //                //familyVo = customerFamilyBo.GetCustomerFamilyAssociateDetails(customerVo.CustomerId);
                    //                //liabilityAssociateVo.AssociationId = int.Parse(familyVo.AssociationId.ToString());
                    //                liabilityAssociateVo.AssociationId = customerVo.AssociationId;
                    //            }
                    //        }

                    //        if (liabilitiesVo.LoanTypeCode.ToString() == "1")
                    //        {
                    //            if ((TextBox)gvr.FindControl("txtAssetOwnership") != null)
                    //            {
                    //                TextBox txtAsset = (TextBox)gvr.FindControl("txtAssetOwnership");
                    //                float share = float.Parse(txtAsset.Text);
                    //                int propertyId = 0;
                    //                if (Session["propertyVo"] != null)
                    //                {
                    //                    propertyVo = (PropertyVo)Session["propertyVo"];
                    //                }
                    //                propertyId = propertyVo.AccountId;
                    //                liabilitiesBo.UpdatePropertyAccountAssociates(propertyId, share, liabilityAssociateVo.AssociationId, liabilityAssociateVo.LoanAssociateCode);
                    //            }
                    //        }
                    //        if ((TextBox)gvr.FindControl("txtLoanObligation") != null)
                    //        {
                    //            TextBox txtOligation = (TextBox)gvr.FindControl("txtLoanObligation");
                    //            liabilityAssociateVo.LiabilitySharePer = int.Parse(txtOligation.Text);
                    //        }
                    //        if ((TextBox)gvr.FindControl("txtMargin") != null)
                    //        {
                    //            TextBox txtMargin = (TextBox)gvr.FindControl("txtMargin");
                    //            liabilityAssociateVo.MarginPer = int.Parse(txtMargin.Text);
                    //        }
                    //        liabilityAssociateVo.LiabilitiesId = LiabilityId;
                    //        liabilityAssociateVo.CreatedBy = userVo.UserId;
                    //        liabilityAssociateVo.ModifiedBy = userVo.UserId;
                    //        liabilitiesBo.CreateLiabilityAssociates(liabilityAssociateVo);
                    //        Session.Remove("test");
                    //    }
                    //}
                    //else
                    //{
                    //familyVo = customerFamilyBo.GetCustomerFamilyAssociateDetails(customerVo.CustomerId);
                    //liabilityAssociateVo.AssociationId = int.Parse(familyVo.AssociationId.ToString());
                    liabilityAssociateVo.LiabilitiesId = LiabilityId;
                    if (customerVo.AssociationId == 0)
                    {
                        DataTable dt = new DataTable();
                        dt = customerFamilyBo.GetAllCustomerAssociates(customerVo.CustomerId);
                        if (dt != null && dt.Rows.Count > 0)
                            customerVo.AssociationId = int.Parse(dt.Rows[0]["CA_AssociationId"].ToString());
                    }
                    liabilityAssociateVo.AssociationId = customerVo.AssociationId;
                    liabilityAssociateVo.LoanAssociateCode = "MC";
                    liabilityAssociateVo.LiabilitySharePer = 100;
                    liabilityAssociateVo.MarginPer = 100;
                    liabilityAssociateVo.CreatedBy = userVo.UserId;
                    liabilityAssociateVo.ModifiedBy = userVo.UserId;
                    liabilitiesBo.CreateLiabilityAssociates(liabilityAssociateVo);
                    Session.Remove("test");
                    //}
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LiabilityView','none');", true);
                }
                //}
                //else if (gvValidationResult == 2)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Please check the allocation percentage..!');", true);
                //}
                //else if (gvValidationResult == 3)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Please select a Co-Borrower!');", true);
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
                FunctionInfo.Add("Method", "LiabilitiesMaintenanceForm.ascx.cs:btnSubmit_Click()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ViewLiability()
        {

            List<LiabilityAssociateVo> liabilityAssociateListVo = new List<LiabilityAssociateVo>();
            try
            {
                //trExistingAssets.Visible = false;
                //if (liabilityVo.LoanTypeCode.ToString() == "1" || liabilityVo.LoanTypeCode.ToString() == "2")
                //{

                //    trLAP.Visible = false;
                //    trLAPTitle.Visible = false;
                //    trLAPError.Visible = false;

                //}

                //if (liabilityVo.LoanTypeCode.ToString() == "3" || liabilityVo.LoanTypeCode.ToString() == "4")
                //{
                //    trLAP.Visible = true;
                //    trLAPTitle.Visible = true;
                //    BindLAPCheckList();
                //}
                //else
                //{
                //    trLAP.Visible = false;
                //    trLAPTitle.Visible = false;
                //    trLAPError.Visible = false;
                //}
                //trLoanExceptionsTitle.Visible = false;
                //trLoanExceptions.Visible = false;
                //txtAmountPrepaid.Text = liabilityVo.AmountPrepaid.ToString();
                //txtAmountPrepaid.Enabled = false;
                txtEMIAmount.Text = liabilityVo.EMIAmount.ToString();
                txtEMIAmount.Enabled = false;
                if (liabilityVo.InstallmentEndDate != DateTime.MinValue)
                    txtInstallmentEndDt.Text = liabilityVo.InstallmentEndDate.ToShortDateString();
                txtInstallmentEndDt.Enabled = false;

                if (liabilityVo.InstallmentStartDate != DateTime.MinValue)
                    txtInstallmentStartDt.Text = liabilityVo.InstallmentStartDate.ToShortDateString();
                txtInstallmentStartDt.Enabled = false;

                txtInterestRate.Text = liabilityVo.RateOfInterest.ToString();
                txtInterestRate.Enabled = false;

                txtLoanAmount.Text = liabilityVo.LoanAmount.ToString();
                txtLoanAmount.Enabled = false;

                if (liabilityVo.Tenure != 0)
                {
                    int noOfYears = 0;
                    int noOfMonths = 0;
                    noOfYears = liabilityVo.Tenure / 12;
                    noOfMonths = liabilityVo.Tenure % 12;
                    txtTenture.Text = noOfYears.ToString();
                    txtTenureMonths.Text = noOfMonths.ToString();
                }
                txtTenureMonths.Enabled = false;
                txtTenture.Enabled = false;

                if (liabilityVo.LoanStartDate != DateTime.MinValue)
                    txtLoanStartDate.Text = liabilityVo.LoanStartDate.ToShortDateString();
                txtLoanStartDate.Enabled = false;

                txtGuarantor.Text = liabilityVo.Guarantor.ToString();
                txtGuarantor.Enabled = false;

                txtNoOfInstallments.Text = liabilityVo.NoOfInstallments.ToString();
                txtNoOfInstallments.Enabled = false;

                txtLumpsumRepaymentAmount.Text = liabilityVo.LumpsumRepaymentAmount.ToString();
                txtLumpsumRepaymentAmount.Enabled = false;

                txtLoanOutstandingAmount.Text = liabilityVo.OutstandingAmount.ToString();
                txtLoanOutstandingAmount.Enabled = false;



                DataTable dt = XMLBo.GetLoanType(path);
                ddlLoanType.DataSource = dt;
                ddlLoanType.DataTextField = dt.Columns["XLT_LoanType"].ToString();
                ddlLoanType.DataValueField = dt.Columns["XLT_LoanTypeCode"].ToString();
                ddlLoanType.DataBind();
                ddlLoanType.Items.Insert(0, new ListItem("Select Loan Type", "Select Loan Type"));
                ddlLoanType.SelectedValue = liabilityVo.LoanTypeCode.ToString();
                ddlLoanType.Enabled = false;
                // BINDING LENDERS

                DataTable dtLender = XMLBo.GetLoanPartner(path);
                ddlLender.DataSource = dtLender;
                ddlLender.DataTextField = dtLender.Columns["XLP_LoanPartner"].ToString();
                ddlLender.DataValueField = dtLender.Columns["XLP_LoanPartnerCode"].ToString();
                ddlLender.DataBind();
                ddlLender.Items.Insert(0, new ListItem("Select Lender", "Select Lender"));
                ddlLender.SelectedValue = liabilityVo.LoanPartnerCode.ToString();
                ddlLender.Enabled = false;
                if (liabilityVo.LoanPartnerCode == 15)
                {
                    txtOtherLender.Text = liabilityVo.OtherLenderName.ToString();
                    txtOtherLender.Visible = true;
                    txtOtherLender.Enabled = false;
                }
                else
                {
                    txtOtherLender.Visible = false;
                }
                // BINDING REPAYMENT TYPE

                DataTable dtRepayment = XMLBo.GetInstallmentType(path);
                ddlRepaymentType.DataSource = dtRepayment;
                ddlRepaymentType.DataTextField = dtRepayment.Columns["XIT_InstallmentType"].ToString();
                ddlRepaymentType.DataValueField = dtRepayment.Columns["XIT_InstallmentTypeCode"].ToString();
                ddlRepaymentType.DataBind();
                ddlRepaymentType.Items.Insert(0, new ListItem("Select the Installment Type", "Select the Installment Type"));
                ddlRepaymentType.SelectedValue = liabilityVo.InstallmentTypeCode.ToString();
                ddlRepaymentType.Enabled = false;
                // BINDING Payment Option

                DataTable dtPaymentOption = XMLBo.GetPaymentOption(path);
                ddlPaymentOption.DataSource = dtPaymentOption;
                ddlPaymentOption.DataTextField = dtPaymentOption.Columns["XPO_PaymentOption"].ToString();
                ddlPaymentOption.DataValueField = dtPaymentOption.Columns["XPO_PaymentOptionCode"].ToString();
                ddlPaymentOption.DataBind();
                ddlPaymentOption.Items.Insert(0, new ListItem("Select the Payment Option", "Select the Payment Option"));
                ddlPaymentOption.SelectedValue = liabilityVo.PaymentOptionCode.ToString();
                ddlPaymentOption.Enabled = false;
                // BINDING Compound FREQUENCY
                if (liabilityVo.PaymentOptionCode == 1)
                {
                    trInstallmentHeader.Visible = false;
                    trInstallment1.Visible = false;
                    trInstallment2.Visible = false;
                    trInstallment3.Visible = false;

                    trLumpsum.Visible = true;

                }
                else if (liabilityVo.PaymentOptionCode == 2)
                {
                    trInstallmentHeader.Visible = true;
                    trInstallment1.Visible = true;
                    trInstallment2.Visible = true;
                    trInstallment3.Visible = true;

                    trLumpsum.Visible = false;

                }
                else
                {
                    trInstallmentHeader.Visible = false;
                    trInstallment1.Visible = false;
                    trInstallment2.Visible = false;
                    trInstallment3.Visible = false;

                    trLumpsum.Visible = false;
                }


                DataTable dtCompoundFrequency = XMLBo.GetFrequency(path);
                for (int i = 0; i < dtCompoundFrequency.Rows.Count; i++)
                {
                    if (dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "MN" && dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "YR" && dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "QT" && dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "HY")
                    {
                        dtCompoundFrequency.Rows[i].Delete();
                        i--;
                    }
                }
                ddlCompoundFrequency.DataSource = dtCompoundFrequency;
                ddlCompoundFrequency.DataTextField = dtCompoundFrequency.Columns["Frequency"].ToString();
                ddlCompoundFrequency.DataValueField = dtCompoundFrequency.Columns["FrequencyCode"].ToString();
                ddlCompoundFrequency.DataBind();
                ddlCompoundFrequency.Items.Insert(0, new ListItem("Select the Frequency", "Select the Frequency"));
                ddlCompoundFrequency.SelectedValue = liabilityVo.CompoundFrequency.ToString();
                ddlCompoundFrequency.Enabled = false;
                // BINDING EMI FREQUENCY

                DataTable dtFrequency = XMLBo.GetFrequency(path);
                for (int i = 0; i < dtFrequency.Rows.Count; i++)
                {
                    if (dtFrequency.Rows[i]["FrequencyCode"].ToString() != "MN" && dtFrequency.Rows[i]["FrequencyCode"].ToString() != "YR" && dtFrequency.Rows[i]["FrequencyCode"].ToString() != "QT" && dtFrequency.Rows[i]["FrequencyCode"].ToString() != "HY")
                    {
                        dtFrequency.Rows[i].Delete();
                        i--;
                    }
                }
                ddlEMIFrequency.DataSource = dtFrequency;
                ddlEMIFrequency.DataTextField = dtFrequency.Columns["Frequency"].ToString();
                ddlEMIFrequency.DataValueField = dtFrequency.Columns["FrequencyCode"].ToString();
                ddlEMIFrequency.DataBind();
                ddlEMIFrequency.Items.Insert(0, new ListItem("Select the Frequency", "Select the Frequency"));
                if (liabilityVo.FrequencyCodeEMI != null)
                    ddlEMIFrequency.SelectedValue = liabilityVo.FrequencyCodeEMI.ToString();
                ddlEMIFrequency.Enabled = false;
                //DataTable dtGuarantor = customerFamilyBo.GetCustomerAssociates(customerVo.CustomerId);
                //if (dtGuarantor != null)
                //{
                //    ddlGuarantor.DataSource = dtGuarantor;
                //    ddlGuarantor.DataValueField = dtGuarantor.Columns["CA_AssociationId"].ToString();
                //    ddlGuarantor.DataTextField = dtGuarantor.Columns["CustomerName"].ToString();
                //    ddlGuarantor.DataBind();
                //    ddlGuarantor.Enabled = false;
                //}
                //else
                //{
                //    //ddlGuarantor.Items.Insert(0, new ListItem("Select a Guarantor", "Select a Guarantor"));
                //    ddlGuarantor.Enabled = false;
                //}

                //ddlEMIDate.SelectedValue = liabilityVo.EMIDate.ToString();
                //ddlEMIDate.Enabled = false;

                //ddlGuarantor.SelectedItem.Value=liabilitiesVo.
                //BindCoBorrowerGridView(liabilityVo.LiabilitiesId);
                //txtNoCoBorrowers.Enabled = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesMaintenanceForm.ascx.cs:ViewLiability()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        //private void BindCoBorrowerGridView(int LiabilityId)
        //{

        //    DataRow drTemp;
        //    try
        //    {
        //        dtLiabilityAssociates = liabilitiesBo.GetLiabilityAssociates(LiabilityId);
        //        // dtLiabilityAssociates = liabilitiesBo.GetAssetOwnerShip(LiabilityId);
        //        dtTempCoBorrower.Columns.Add("CLA_LiabilitiesAssociationId");
        //        dtTempCoBorrower.Columns.Add("CA_AssociationId");
        //        dtTempCoBorrower.Columns.Add("Loan Obligation %");
        //        dtTempCoBorrower.Columns.Add("Margin %");

        //        for (int i = 0; i < dtLiabilityAssociates.Rows.Count; i++)
        //        {
        //            if (dtLiabilityAssociates.Rows[i]["XLAT_LoanAssociateCode"].ToString() == "GR")
        //            {
        //                ddlGuarantor.SelectedValue = dtLiabilityAssociates.Rows[i]["CA_AssociationId"].ToString();
        //            }

        //            else
        //            {
        //                drTemp = dtTempCoBorrower.NewRow();
        //                drTemp[0] = dtLiabilityAssociates.Rows[i]["CLA_LiabilitiesAssociationId"].ToString();
        //                drTemp[1] = dtLiabilityAssociates.Rows[i]["CA_AssociationId"].ToString();
        //                drTemp[2] = dtLiabilityAssociates.Rows[i]["CLA_LiabilitySharePer"].ToString();
        //                drTemp[3] = dtLiabilityAssociates.Rows[i]["CLA_MarginPer"].ToString();
        //                dtTempCoBorrower.Rows.Add(drTemp);
        //            }
        //        }
        //        txtNoCoBorrowers.Text = dtTempCoBorrower.Rows.Count.ToString();
        //        DataTable dtCoborrower = new DataTable();
        //        DataRow drCoborrower;
        //        dtCoborrower.Columns.Add("CLA_LiabilitiesAssociationId");
        //        dtCoborrower.Columns.Add("CA_AssociationId");
        //        dtCoborrower.Columns.Add("Loan Obligation %");
        //        dtCoborrower.Columns.Add("Margin %");

        //        DataRow dr;
        //        for (int i = 0; i < dtTempCoBorrower.Rows.Count; i++)
        //        {
        //            drCoborrower = dtCoborrower.NewRow();
        //            dr = dtTempCoBorrower.Rows[i];
        //            drCoborrower[0] = dr["CLA_LiabilitiesAssociationId"].ToString();
        //            drCoborrower[1] = dr["CA_AssociationId"].ToString();
        //            drCoborrower[1] = dr["Loan Obligation %"].ToString();
        //            drCoborrower[2] = dr["Margin %"].ToString();
        //            dtCoborrower.Rows.Add(drCoborrower);
        //        }

        //        gvCoBorrower.DataSource = dtCoborrower;
        //        gvCoBorrower.DataBind();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "LiabilitiesMaintenanceForm.ascx.cs:BindCoBorrowerGridView()");
        //        object[] objects = new object[1];
        //        objects[0] = LiabilityId;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //}
        public void CalculateNumberOfInvestements()
        {

            int numberOfInstallments = 0;
            int noOfYears = 0;
            int noOfMonths = 0;
            bool yResult = int.TryParse(txtTenture.Text.ToString(), out noOfYears);
            bool mResult = int.TryParse(txtTenureMonths.Text.ToString(), out noOfMonths);

            //bool result=int.TryParse(txtTenture.Text.ToString(),out i);
            if (ddlEMIFrequency.SelectedValue != "Select the Frequency")
            {
                switch (ddlEMIFrequency.SelectedValue)
                {
                    case "MN":
                        numberOfInstallments = (noOfYears * 12) + noOfMonths;
                        break;
                    case "QT":
                        numberOfInstallments = (noOfYears * 4) + (noOfMonths / 3);
                        break;

                    case "HY":
                        numberOfInstallments = (noOfYears * 2) + (noOfMonths / 6);
                        break;

                    case "YR":
                        numberOfInstallments = (noOfYears) + (noOfMonths / 12);
                        break;
                }
                txtNoOfInstallments.Text = numberOfInstallments.ToString();
            }
        }
        public void CalcualteLumpSum()
        {
            int frequencyCount = 0;
            double lumpSumAmount = 0;
            double loanAmount = 0;
            double interestRate = 0;
            int numberOfInstallments = 0;
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            bool sdResult = DateTime.TryParse(txtInstallmentStartDt.Text, out startDate);
            bool edResult = DateTime.TryParse(txtInstallmentEndDt.Text, out endDate);
            bool laResult = double.TryParse(txtLoanAmount.Text, out loanAmount);
            bool iResult = double.TryParse(txtInterestRate.Text, out interestRate);
            int noOfYears = 0;
            int noOfMonths = 0;
            bool yResult = int.TryParse(txtTenture.Text.ToString(), out noOfYears);
            bool mResult = int.TryParse(txtTenureMonths.Text.ToString(), out noOfMonths);

            if (laResult && iResult && ddlCompoundFrequency.SelectedValue.ToString() != "Select the Frequency")
            {
                switch (ddlCompoundFrequency.SelectedValue)
                {
                    case "MN":
                        frequencyCount = 12;
                        numberOfInstallments = (noOfYears * 12) + noOfMonths;
                        break;
                    case "QT":
                        frequencyCount = 4;
                        numberOfInstallments = (noOfYears * 4) + (noOfMonths / 3);
                        break;

                    case "HY":
                        frequencyCount = 2;
                        numberOfInstallments = (noOfYears * 2) + (noOfMonths / 6);
                        break;

                    case "YR":
                        frequencyCount = 1;
                        numberOfInstallments = (noOfYears) + (noOfMonths / 12);
                        break;
                }
                lumpSumAmount = Math.Abs(System.Numeric.Financial.Fv((interestRate / 100) / frequencyCount, numberOfInstallments, 0, loanAmount, 0));
                txtLumpsumRepaymentAmount.Text = Math.Round(lumpSumAmount, 4).ToString();
                if (ddlPaymentOption.SelectedValue == "1")
                    txtLoanOutstandingAmount.Text = Math.Round((calculator.GetLoanOutstanding(ddlCompoundFrequency.SelectedValue.ToString(), loanAmount, startDate, endDate, int.Parse(ddlPaymentOption.SelectedValue.ToString()), lumpSumAmount, numberOfInstallments)), 4).ToString();
            }
        }
        public void CalculateInstallmentAmount()
        {

            double installmentAmount = 0;
            int frequencyCount = 0;
            double loanAmount = 0;
            double interestRate = 0;
            int numberOfInstallments = 0;
            DateTime startDate = new DateTime();    
            DateTime endDate = new DateTime();
            bool sdResult = DateTime.TryParse(txtInstallmentStartDt.Text, out startDate);
            bool edResult = DateTime.TryParse(txtInstallmentEndDt.Text, out endDate);
            bool laResult = double.TryParse(txtLoanAmount.Text, out loanAmount);
            bool iResult = double.TryParse(txtInterestRate.Text, out interestRate);
            bool lpResult = int.TryParse(txtNoOfInstallments.Text, out numberOfInstallments);
            if (laResult && iResult && lpResult && ddlRepaymentType.SelectedValue.ToString() != "Select the Installment Type")
            {
                switch (ddlEMIFrequency.SelectedValue)
                {
                    case "MN":
                        frequencyCount = 12;
                        break;
                    case "QT":
                        frequencyCount = 4;
                        break;

                    case "HY":
                        frequencyCount = 2;
                        break;

                    case "YR":
                        frequencyCount = 1;
                        break;
                }
                int loanPeriod = 0;
                bool result = int.TryParse(txtTenture.Text, out loanPeriod);
                double noOfIns = 0;
                noOfIns = frequencyCount;
                double effectiveRate = (Math.Pow((1 + (interestRate / 100) / noOfIns), noOfIns)) - 1;
                double effectiveRatePerPeriod = Math.Pow(1 + effectiveRate, 1 / noOfIns) - 1;
                switch (ddlRepaymentType.SelectedValue)
                {
                    case "1":
                        installmentAmount = loanAmount / numberOfInstallments;
                        break;
                    case "2":
                        installmentAmount = Math.Abs(System.Numeric.Financial.Pmt(effectiveRatePerPeriod, numberOfInstallments, loanAmount, 0, 0));

                        break;
                }
                txtEMIAmount.Text = Math.Round(installmentAmount, 4).ToString();
                txtLoanOutstandingAmount.Text = Math.Round((calculator.GetLoanOutstanding(ddlEMIFrequency.SelectedValue.ToString(), loanAmount, startDate, endDate, int.Parse(ddlPaymentOption.SelectedValue.ToString()), installmentAmount, numberOfInstallments)), 4).ToString();

            }
        }
        protected void EditLiability()
        {



            LiabilitiesVo liabilitiesVo = new LiabilitiesVo();
            liabilitiesVo = (LiabilitiesVo)Session["liabilityVo"];

            //if (liabilitiesVo.LoanTypeCode.ToString() == "1" || liabilitiesVo.LoanTypeCode.ToString() == "2")
            //{

            //    trLAP.Visible = false;
            //    trLAPTitle.Visible = false;
            //    trLAPError.Visible = false;
            //}


            //else if (liabilitiesVo.LoanTypeCode.ToString() == "3" || liabilitiesVo.LoanTypeCode.ToString() == "4")
            //{
            //    trLAP.Visible = true;
            //    trLAPTitle.Visible = true;
            //    trExistingAssets.Visible = false;
            //    BindLAPCheckList();
            //}
            //else
            //{
            //    trLAP.Visible = false;
            //    trLAPTitle.Visible = false;
            //    //trAssets.Visible = false;
            //    // btnAddAsset0.Visible = false;
            //    trExistingAssets.Visible = false;
            //}

            //trLoanExceptionsTitle.Visible = true;
            //trLoanExceptions.Visible = false;
            //txtAmountPrepaid.Text = liabilitiesVo.AmountPrepaid.ToString();
            //txtAmountPrepaid.Enabled = true;
            txtEMIAmount.Text = liabilityVo.EMIAmount.ToString();
            txtEMIAmount.Enabled = true;
            if (liabilityVo.InstallmentEndDate != DateTime.MinValue)
                txtInstallmentEndDt.Text = liabilityVo.InstallmentEndDate.ToShortDateString();
            txtInstallmentEndDt.Enabled = true;

            if (liabilityVo.InstallmentStartDate != DateTime.MinValue)
                txtInstallmentStartDt.Text = liabilityVo.InstallmentStartDate.ToShortDateString();
            txtInstallmentStartDt.Enabled = true;

            txtInterestRate.Text = liabilityVo.RateOfInterest.ToString();
            txtInterestRate.Enabled = true;

            txtLoanAmount.Text = liabilityVo.LoanAmount.ToString();
            txtLoanAmount.Enabled = true;

            if (liabilityVo.Tenure != 0)
            {
                int noOfYears = 0;
                int noOfMonths = 0;
                noOfYears = liabilityVo.Tenure / 12;
                noOfMonths = liabilityVo.Tenure % 12;
                txtTenture.Text = noOfYears.ToString();
                txtTenureMonths.Text = noOfMonths.ToString();
            }
            txtTenureMonths.Enabled = true;
            txtTenture.Enabled = true;

            if (liabilityVo.LoanStartDate != DateTime.MinValue)
                txtLoanStartDate.Text = liabilityVo.LoanStartDate.ToShortDateString();
            txtLoanStartDate.Enabled = true;
            if (!string.IsNullOrEmpty(liabilityVo.Guarantor))
                txtGuarantor.Text = liabilityVo.Guarantor.ToString();
            txtGuarantor.Enabled = true;

            txtNoOfInstallments.Text = liabilityVo.NoOfInstallments.ToString();
            txtNoOfInstallments.Enabled = true;

            txtLumpsumRepaymentAmount.Text = liabilityVo.LumpsumRepaymentAmount.ToString();
            txtLumpsumRepaymentAmount.Enabled = true;

            txtLoanOutstandingAmount.Text = liabilityVo.OutstandingAmount.ToString();
            txtLoanOutstandingAmount.Enabled = true;



            DataTable dt = XMLBo.GetLoanType(path);
            ddlLoanType.DataSource = dt;
            ddlLoanType.DataTextField = dt.Columns["XLT_LoanType"].ToString();
            ddlLoanType.DataValueField = dt.Columns["XLT_LoanTypeCode"].ToString();
            ddlLoanType.DataBind();
            ddlLoanType.Items.Insert(0, new ListItem("Select Loan Type", "Select Loan Type"));
            ddlLoanType.SelectedValue = liabilityVo.LoanTypeCode.ToString();
            ddlLoanType.Enabled = true;
            // BINDING LENDERS

            DataTable dtLender = XMLBo.GetLoanPartner(path);
            ddlLender.DataSource = dtLender;
            ddlLender.DataTextField = dtLender.Columns["XLP_LoanPartner"].ToString();
            ddlLender.DataValueField = dtLender.Columns["XLP_LoanPartnerCode"].ToString();
            ddlLender.DataBind();
            ddlLender.Items.Insert(0, new ListItem("Select Lender", "Select Lender"));
            ddlLender.SelectedValue = liabilityVo.LoanPartnerCode.ToString();
            ddlLender.Enabled = true;
            if (liabilityVo.LoanPartnerCode == 15)
            {
                txtOtherLender.Text = liabilityVo.OtherLenderName.ToString();
                txtOtherLender.Visible = true;
                txtOtherLender.Enabled = true;
            }
            else
            {
                txtOtherLender.Visible = false;
            }
            // BINDING REPAYMENT TYPE

            DataTable dtRepayment = XMLBo.GetInstallmentType(path);
            ddlRepaymentType.DataSource = dtRepayment;
            ddlRepaymentType.DataTextField = dtRepayment.Columns["XIT_InstallmentType"].ToString();
            ddlRepaymentType.DataValueField = dtRepayment.Columns["XIT_InstallmentTypeCode"].ToString();
            ddlRepaymentType.DataBind();
            ddlRepaymentType.Items.Insert(0, new ListItem("Select the Installment Type", "Select the Installment Type"));
            if (liabilityVo.InstallmentTypeCode != 0)
                ddlRepaymentType.SelectedValue = liabilityVo.InstallmentTypeCode.ToString();
            ddlRepaymentType.Enabled = true;
            // BINDING Payment Option

            DataTable dtPaymentOption = XMLBo.GetPaymentOption(path);
            ddlPaymentOption.DataSource = dtPaymentOption;
            ddlPaymentOption.DataTextField = dtPaymentOption.Columns["XPO_PaymentOption"].ToString();
            ddlPaymentOption.DataValueField = dtPaymentOption.Columns["XPO_PaymentOptionCode"].ToString();
            ddlPaymentOption.DataBind();
            ddlPaymentOption.Items.Insert(0, new ListItem("Select the Payment Option", "Select the Payment Option"));
            ddlPaymentOption.SelectedValue = liabilityVo.PaymentOptionCode.ToString();
            ddlPaymentOption.Enabled = true;

            if (liabilityVo.PaymentOptionCode == 1)
            {
                trInstallmentHeader.Visible = false;
                trInstallment1.Visible = false;
                trInstallment2.Visible = false;
                trInstallment3.Visible = false;


                trLumpsum.Visible = true;

            }
            else if (liabilityVo.PaymentOptionCode == 2)
            {
                trInstallmentHeader.Visible = true;
                trInstallment1.Visible = true;
                trInstallment2.Visible = true;
                trInstallment3.Visible = true;

                trLumpsum.Visible = false;

            }
            else
            {
                trInstallmentHeader.Visible = false;
                trInstallment1.Visible = false;
                trInstallment2.Visible = false;
                trInstallment3.Visible = false;

                trLumpsum.Visible = false;
            }

            // BINDING Compound FREQUENCY

            DataTable dtCompoundFrequency = XMLBo.GetFrequency(path);


            for (int i = 0; i < dtCompoundFrequency.Rows.Count; i++)
            {
                if (dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "MN" && dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "YR" && dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "QT" && dtCompoundFrequency.Rows[i]["FrequencyCode"].ToString() != "HY")
                {
                    dtCompoundFrequency.Rows[i].Delete();
                    i--;
                }
            }
            ddlCompoundFrequency.DataSource = dtCompoundFrequency;
            ddlCompoundFrequency.DataTextField = dtCompoundFrequency.Columns["Frequency"].ToString();
            ddlCompoundFrequency.DataValueField = dtCompoundFrequency.Columns["FrequencyCode"].ToString();
            ddlCompoundFrequency.DataBind();
            ddlCompoundFrequency.Items.Insert(0, new ListItem("Select the Frequency", "Select the Frequency"));
            if (liabilityVo.CompoundFrequency != null)
                ddlCompoundFrequency.SelectedValue = liabilityVo.CompoundFrequency.ToString();
            else
                ddlCompoundFrequency.SelectedValue = "0";
            ddlCompoundFrequency.Enabled = true;
            // BINDING EMI FREQUENCY

            DataTable dtFrequency = XMLBo.GetFrequency(path);
            for (int i = 0; i < dtFrequency.Rows.Count; i++)
            {
                if (dtFrequency.Rows[i]["FrequencyCode"].ToString() != "MN" && dtFrequency.Rows[i]["FrequencyCode"].ToString() != "YR" && dtFrequency.Rows[i]["FrequencyCode"].ToString() != "QT" && dtFrequency.Rows[i]["FrequencyCode"].ToString() != "HY")
                {
                    dtFrequency.Rows[i].Delete();
                    i--;
                }
            }
            ddlEMIFrequency.DataSource = dtFrequency;
            ddlEMIFrequency.DataTextField = dtFrequency.Columns["Frequency"].ToString();
            ddlEMIFrequency.DataValueField = dtFrequency.Columns["FrequencyCode"].ToString();
            ddlEMIFrequency.DataBind();
            ddlEMIFrequency.Items.Insert(0, new ListItem("Select the Frequency", "Select the Frequency"));
            if (liabilityVo.FrequencyCodeEMI != null)
                ddlEMIFrequency.SelectedValue = liabilityVo.FrequencyCodeEMI.ToString();
            ddlEMIFrequency.Enabled = true;
        }

        protected void gvAddAlterations_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //protected void btnAddException_Click(object sender, EventArgs e)
        //{
        //    EditLogVo editLogVo = new EditLogVo();
        //    LiabilitiesVo liabilitiesVo = new LiabilitiesVo();
        //    liabilitiesVo = (LiabilitiesVo)Session["liabilityVo"];
        //    string newEditDate = string.Empty;
        //    string newExceptionType = string.Empty;
        //    string newAmount = string.Empty;
        //    string newRefNo = string.Empty;
        //    string newRemark = string.Empty;
        //    CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        //    GridViewRow footerRow = gvAddAlterations.FooterRow;
        //    DropDownList ddl = (DropDownList)footerRow.FindControl("ddlExceptionType");
        //    if (footerRow.Enabled)
        //    {
        //        if (((TextBox)footerRow.FindControl("txtDate")).Text.Trim() != "")
        //        {
        //            newEditDate = ((TextBox)footerRow.FindControl("txtDate")).Text;
        //        }
        //        if (((DropDownList)footerRow.FindControl("ddlExceptionType")).SelectedItem.Value.Trim().ToString() != "")
        //        {
        //            if (ddl.SelectedIndex != 0)
        //            {
        //                newExceptionType = ddl.SelectedItem.Value.ToString();
        //            }
        //        }
        //        if (((TextBox)footerRow.FindControl("txtAmount")).Text.Trim() != "")
        //        {
        //            newAmount = ((TextBox)footerRow.FindControl("txtAmount")).Text;
        //        }
        //        if (((TextBox)footerRow.FindControl("txtRefNo")).Text.Trim() != "")
        //        {
        //            newRefNo = ((TextBox)footerRow.FindControl("txtRefNo")).Text;
        //        }
        //        if (((TextBox)footerRow.FindControl("txtRemark")).Text.Trim() != "")
        //        {
        //            newRemark = ((TextBox)footerRow.FindControl("txtRemark")).Text;
        //        }

        //        if (newAmount == "" && newRefNo == "" && ddl.SelectedIndex == 0 && newEditDate == "")
        //        {

        //        }
        //        else
        //        {
        //            editLogVo.CreatedBy = userVo.UserId;
        //            editLogVo.EditAmount = double.Parse(newAmount);
        //            editLogVo.EditOccurenceDate = DateTime.Parse(newEditDate);
        //            editLogVo.EditTypeCode = int.Parse(newExceptionType.ToString());
        //            editLogVo.LiabilitiesId = liabilitiesVo.LiabilitiesId;
        //            editLogVo.ModifiedBy = userVo.UserId;
        //            editLogVo.ReferenceNum = newRefNo;
        //            editLogVo.Remark = newRemark;

        //        }
        //    }

        //    liabilitiesBo.CreatEditLogInfo(editLogVo);
        //    BindExceptionGrid();
        //}

        //protected void btnAddAsset_Click(object sender, EventArgs e)
        //{
        //    CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        //    PortfolioBo portfolioBo = new PortfolioBo();
        //    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
        //    Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
        //    Session["test"] = "test";
        //    if (ddlLoanType.SelectedValue.ToString() == "1")
        //    {
        //        SaveCurrentPageState();
        //        string url = "?retURL=liabilities";
        //        Session["action"] = "PR";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerAccountAdd','" + url + "');", true);
        //    }
        //    else if (ddlLoanType.SelectedValue.ToString() == "2")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioPersonalEntry','none');", true);
        //    }

        //}

        private void SaveCurrentPageState()
        {

            Hashtable hashtable = new Hashtable();
            hashtable.Add("LoanAmount", txtLoanAmount.Text);
            hashtable.Add("InterestRate", txtInterestRate.Text);
            //hashtable.Add("NoCoBorrowers", txtNoCoBorrowers.Text);

            hashtable.Add("Lender", ddlLender.SelectedIndex);
            //hashtable.Add("FloatYes", rbtnFloatYes.Checked);

            //hashtable.Add("Guaranter", ddlGuarantor.SelectedIndex);

            Session["LiabilitiesMaintenanceHT"] = hashtable;
        }

        /// <summary>
        /// Maintain values after adding Asset
        /// </summary>
        private void RestorePreviousState()
        {
            if (Session["LiabilitiesMaintenanceHT"] != null && Request.QueryString["prevPage"] != null && Request.QueryString["prevPage"] == "propertyEntry")
            {
                Hashtable hashtable = new Hashtable();
                hashtable = (Hashtable)Session["LiabilitiesMaintenanceHT"];
                txtLoanAmount.Text = hashtable["LoanAmount"].ToString();
                txtInterestRate.Text = hashtable["InterestRate"].ToString();
                //txtNoCoBorrowers.Text = hashtable["NoCoBorrowers"].ToString();
                ddlLender.SelectedIndex = (int)hashtable["Lender"];
                //rbtnFloatYes.Checked = (bool)hashtable["FloatYes"];
                //if (hashtable["Guaranter"] != null)
                //{

                //    ddlGuarantor.SelectedIndex = (int)hashtable["Guaranter"];
                //}
                LoanTypeChange();
                //BindAssetsDropDown();
            }
            Session["EqTransactionHT"] = null;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            liabilityVo = (LiabilitiesVo)Session["liabilityVo"];
            if (liabilityVo.LoanTypeCode.ToString() == "1")
            {
                //propertyVo = propertyBo.GetPropertyAsset(liabilityVo.LiabilitiesId);
                dtAssetOwnership = liabilitiesBo.GetAssetOwnerShip(liabilityVo.LiabilitiesId);
                if (dtAssetOwnership != null)
                {
                    for (int i = 0; i < dtAssetOwnership.Rows.Count; i++)
                    {
                        if (dtAssetOwnership.Rows[i]["XLAT_LoanAssociateCode"].ToString() == "CB")
                            CB_Share = float.Parse(dtAssetOwnership.Rows[i]["Share"].ToString()) + CB_Share;
                    }
                }
            }
            trUpdate.Visible = true;
            //trLAPTitle.Visible = false;
            //trLAPError.Visible = false;
            Session["menu"] = "Edit";
            EditLiability();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            LiabilitiesVo newLiabilitiesVo;
            LiabilityAssociateVo newLiabilityAssociateVo;
            DataTable dtLiabilityAssetAssociation = new DataTable();
            int LiabilityId = 0;
            int gvValidationResult;
            LiabilitiesVo liabilitiesVo = new LiabilitiesVo();

            LiabilityAssociateVo liabilityAssociateVo;
            AssetAssociationVo assetAssociationVo;

            CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
            CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
            PropertyVo propertyVo = new PropertyVo();
            CustomerAccountBo customerAccountBo = new CustomerAccountBo();
            PropertyBo propertyBo = new PropertyBo();
            try
            {
                // gvValidationResult = GridViewValidation();
                //if (gvValidationResult == 1)
                //{
                liabilitiesVo = (LiabilitiesVo)Session["liabilityVo"];
                newLiabilitiesVo = new LiabilitiesVo();
                //newLiabilitiesVo.AmountPrepaid = double.Parse(txtAmountPrepaid.Text);
                newLiabilitiesVo.CommissionAmount = 0;
                newLiabilitiesVo.CommissionPer = 0;
                if(txtEMIAmount.Text!="")
                    newLiabilitiesVo.EMIAmount = double.Parse(txtEMIAmount.Text);
                //liabilitiesVo.EMIDate = int.Parse(ddl.SelectedItem.Value.ToString());
                
                if (ddlEMIFrequency.SelectedItem.Value.ToString() != "Select the Frequency")
                    newLiabilitiesVo.FrequencyCodeEMI = ddlEMIFrequency.SelectedItem.Value.ToString();
                if (ddlCompoundFrequency.SelectedItem.Value.ToString() != "Select the Frequency")
                    newLiabilitiesVo.CompoundFrequency = ddlCompoundFrequency.SelectedItem.Value.ToString();
                if (ddlPaymentOption.SelectedItem.Value.ToString() != "Select the Payment Option")
                    newLiabilitiesVo.PaymentOptionCode = int.Parse(ddlPaymentOption.SelectedItem.Value.ToString());
                if (txtLoanOutstandingAmount.Text != "")
                    newLiabilitiesVo.OutstandingAmount = double.Parse(txtLoanOutstandingAmount.Text.ToString());
                if (txtLoanStartDate.Text != "")
                    newLiabilitiesVo.LoanStartDate = DateTime.Parse(txtLoanStartDate.Text);
                if (txtInstallmentEndDt.Text != "")
                    newLiabilitiesVo.InstallmentEndDate = DateTime.Parse(txtInstallmentEndDt.Text);
                if (txtInstallmentStartDt.Text != "")
                    newLiabilitiesVo.InstallmentStartDate = DateTime.Parse(txtInstallmentStartDt.Text);
                newLiabilitiesVo.OtherLenderName = txtOtherLender.Text;
                newLiabilitiesVo.LumpsumRepaymentAmount = Double.Parse(txtLumpsumRepaymentAmount.Text);
                newLiabilitiesVo.Guarantor = txtGuarantor.Text.ToString();
                int noOfYears = 0;
                int noOfMonths = 0;
                if (int.TryParse(txtTenture.Text, out noOfYears) && int.TryParse(txtTenureMonths.Text, out noOfMonths))
                {
                    newLiabilitiesVo.Tenure = (noOfYears * 12) + noOfMonths;
                }
                //if (rbtnFloatYes.Checked)
                //{
                //    liabilitiesVo.IsFloatingRateInterest = 1;
                //}
                //else
                //{
                //    liabilitiesVo.IsFloatingRateInterest = 0;
                //}
                newLiabilitiesVo.IsInProcess = 0;
                newLiabilitiesVo.LoanAmount = double.Parse(txtLoanAmount.Text);
                newLiabilitiesVo.LoanPartnerCode = int.Parse(ddlLender.SelectedItem.Value.ToString());
                newLiabilitiesVo.LoanTypeCode = int.Parse(ddlLoanType.SelectedItem.Value.ToString());
                newLiabilitiesVo.ModifiedBy = userVo.UserId;
                newLiabilitiesVo.NoOfInstallments = int.Parse(txtNoOfInstallments.Text);
                newLiabilitiesVo.RateOfInterest = float.Parse(txtInterestRate.Text);
                if (ddlRepaymentType.SelectedItem.Value.ToString() != "Select the Installment Type")
                    newLiabilitiesVo.InstallmentTypeCode = int.Parse(ddlRepaymentType.SelectedItem.Value.ToString());
                newLiabilitiesVo.LiabilitiesId = liabilitiesVo.LiabilitiesId;

                //dtLiabilityAssetAssociation = liabilitiesBo.GetLiabilityAssetAssociation(liabilitiesVo.LiabilitiesId);

                if (liabilitiesBo.UpdateLiabilities(newLiabilitiesVo))
                {


                    //    foreach (GridViewRow gvr in gvCoBorrower.Rows)
                    //    {
                    //        newLiabilityAssociateVo = new LiabilityAssociateVo();
                    //        if (gvr.RowIndex == 0)
                    //        {
                    //            newLiabilityAssociateVo.AssociationId = customerVo.AssociationId;

                    //        }
                    //        else
                    //        {
                    //            if ((DropDownList)gvr.FindControl("ddlCoBorrowers") != null)
                    //            {
                    //                DropDownList ddl1 = (DropDownList)gvr.FindControl("ddlCoBorrowers");
                    //                familyVo = new CustomerFamilyVo();
                    //                familyVo = customerFamilyBo.GetCustomerFamilyAssociateDetails(int.Parse(ddl1.SelectedValue.ToString()));
                    //                newLiabilityAssociateVo.AssociationId = int.Parse(familyVo.AssociationId.ToString());

                    //                //newLiabilityAssociateVo.AssociationId = int.Parse(ddl1.SelectedValue.ToString());

                    //            }
                    //        }

                    //        if (newLiabilityAssociateVo.AssociationId == customerVo.AssociationId)
                    //        {
                    //            newLiabilityAssociateVo.LoanAssociateCode = "MC";
                    //        }
                    //        else
                    //        {
                    //            newLiabilityAssociateVo.LoanAssociateCode = "CB";
                    //        }
                    //        if ((TextBox)gvr.FindControl("txtLoanObligation") != null)
                    //        {
                    //            TextBox txtAsset = (TextBox)gvr.FindControl("txtLoanObligation");
                    //            newLiabilityAssociateVo.LiabilitySharePer = float.Parse(txtAsset.Text);
                    //        }


                    //        if ((TextBox)gvr.FindControl("txtMargin") != null)
                    //        {
                    //            TextBox txtMargin = (TextBox)gvr.FindControl("txtMargin");
                    //            newLiabilityAssociateVo.MarginPer = float.Parse(txtMargin.Text);
                    //        }
                    //        newLiabilityAssociateVo.LiabilitiesId = liabilitiesVo.LiabilitiesId;
                    //        newLiabilityAssociateVo.CreatedBy = userVo.UserId;
                    //        newLiabilityAssociateVo.ModifiedBy = userVo.UserId;
                    //        newLiabilityAssociateVo.LiabilitiesAssociationId = int.Parse(gvCoBorrower.DataKeys[gvr.RowIndex].Value.ToString());
                    //        liabilitiesBo.UpdateLiabilityAssociates(newLiabilityAssociateVo);
                    //        if (liabilitiesVo.LoanTypeCode == 1)
                    //        {
                    //            if ((TextBox)gvr.FindControl("txtAssetOwnership") != null)
                    //            {
                    //                TextBox txtAssetOwnership = (TextBox)gvr.FindControl("txtAssetOwnership");
                    //                float share = float.Parse(txtAssetOwnership.Text);
                    //                txtAssetOwnership.Enabled = true;
                    //                liabilitiesBo.UpdatePropertyAccountAssociates(liabilitiesVo.LiabilitiesId, share, newLiabilityAssociateVo.AssociationId, newLiabilityAssociateVo.LoanAssociateCode);
                    //            }

                    //        }
                    //        else if (liabilitiesVo.LoanTypeCode == 3 || liabilitiesVo.LoanTypeCode == 4)
                    //        {
                    //            if (liabilitiesBo.DeleteLiabilityAssetAssociation(liabilitiesVo.LiabilitiesId))
                    //            {
                    //                for (int i = 0; i < chkLstAsset.Items.Count; i++)
                    //                {
                    //                    if (chkLstAsset.Items[i].Selected)
                    //                    {
                    //                        assetAssociationVo = new AssetAssociationVo();
                    //                        if (liabilitiesVo.LoanTypeCode.ToString() == "3")
                    //                        {
                    //                            assetAssociationVo.AssetGroupCode = "PR";
                    //                            assetAssociationVo.AssetTable = "CustomerPropertyNetPosition";
                    //                        }
                    //                        else if (liabilitiesVo.LoanTypeCode.ToString() == "4")
                    //                        {
                    //                            assetAssociationVo.AssetGroupCode = "EQ";
                    //                            assetAssociationVo.AssetTable = "CustomerEquityNetPosition";
                    //                        }
                    //                        assetAssociationVo.AssetId = int.Parse(chkLstAsset.Items[i].Value.ToString());
                    //                        assetAssociationVo.CreatedBy = userVo.UserId;
                    //                        assetAssociationVo.ModifiedBy = userVo.UserId;
                    //                        assetAssociationVo.LiabilitiesId = liabilitiesVo.LiabilitiesId;
                    //                        liabilitiesBo.CreatLiabilityAssetAssociation(assetAssociationVo);
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LiabilityView','none');", true);
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
                FunctionInfo.Add("Method", "LiabilitiesMaintenanceForm.ascx.cs:btnSubmit_Click()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        //protected void gvCoBorrower_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (Session["menu"] != null)
        //    {
        //        if (Session["menu"].ToString() == "View")
        //        {
        //            DataTable dtCoborrower = new DataTable();
        //            dtCoborrower.Columns.Add("CLA_LiabilitiesAssociationId");
        //            dtCoborrower.Columns.Add("CA_AssociationId");
        //            dtCoborrower.Columns.Add("Loan Obligation %");
        //            dtCoborrower.Columns.Add("Margin %");


        //            DataRowView drv = e.Row.DataItem as DataRowView;


        //            if (e.Row.RowType == DataControlRowType.DataRow)
        //            {
        //                GridViewRow gvr = e.Row;
        //                DropDownList ddlType;
        //                TextBox txtObligation;
        //                TextBox txtMargin;
        //                TextBox txtAssetOwnership = e.Row.FindControl("txtAssetOwnership") as TextBox;
        //                ddlType = e.Row.FindControl("ddlCoBorrowers") as DropDownList;
        //                txtObligation = e.Row.FindControl("txtLoanObligation") as TextBox;
        //                txtMargin = e.Row.FindControl("txtMargin") as TextBox;
        //                Label lblMainCustomer = e.Row.FindControl("lblMainCustomer") as Label;


        //                if (ddlType != null)
        //                {
        //                    if (gvr.RowIndex == 0)
        //                    {
        //                        if (lblMainCustomer != null)
        //                        {
        //                            lblMainCustomer.Text = customerVo.FirstName + customerVo.LastName;
        //                            ddlType.Visible = false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ddlType.DataSource = dtCoBorrower;
        //                        ddlType.DataValueField = dtCoBorrower.Columns["AssociationId"].ToString();
        //                        ddlType.DataTextField = dtCoBorrower.Columns["AssociateName"].ToString();
        //                        ddlType.DataBind();
        //                        ddlType.Items.Insert(0, new ListItem("Select", "Select"));
        //                        ddlType.SelectedValue = dtTempCoBorrower.Rows[gvr.RowIndex]["CA_AssociationId"].ToString();
        //                        ddlType.Enabled = false;
        //                    }

        //                }
        //                if (liabilityVo.LoanTypeCode.ToString() == "1")
        //                {
        //                    if (gvr.RowIndex == 0)
        //                    {
        //                        txtAssetOwnership.Text = (100 - CB_Share).ToString();
        //                        txtAssetOwnership.Enabled = true;
        //                    }
        //                    else
        //                    {
        //                        if (txtAssetOwnership != null)
        //                        {
        //                            if (dtAssetOwnership != null)
        //                            {
        //                                txtAssetOwnership.Text = dtAssetOwnership.Rows[gvr.RowIndex - 1]["Share"].ToString();
        //                                txtAssetOwnership.Enabled = false;
        //                            }
        //                        }
        //                    }
        //                }

        //                if (txtObligation != null)
        //                {
        //                    txtObligation.Text = dtTempCoBorrower.Rows[gvr.RowIndex]["Loan Obligation %"].ToString();
        //                    txtObligation.Enabled = false;
        //                }
        //                if (txtMargin != null)
        //                {
        //                    txtMargin.Text = dtTempCoBorrower.Rows[gvr.RowIndex]["Margin %"].ToString();
        //                    txtMargin.Enabled = false;
        //                }

        //            }
        //        }
        //        else if (Session["menu"].ToString() == "Edit")
        //        {
        //            DataTable dtCoborrower = new DataTable();
        //            dtCoborrower.Columns.Add("CLA_LiabilitiesAssociationId");
        //            dtCoborrower.Columns.Add("CA_AssociationId");
        //            dtCoborrower.Columns.Add("Loan Obligation %");
        //            dtCoborrower.Columns.Add("Margin %");


        //            DataRowView drv = e.Row.DataItem as DataRowView;


        //            if (e.Row.RowType == DataControlRowType.DataRow)
        //            {
        //                GridViewRow gvr = e.Row;
        //                DropDownList ddlType;
        //                TextBox txtObligation;
        //                TextBox txtMargin;
        //                TextBox txtAssetOwnership = e.Row.FindControl("txtAssetOwnership") as TextBox;
        //                ddlType = e.Row.FindControl("ddlCoBorrowers") as DropDownList;
        //                txtObligation = e.Row.FindControl("txtLoanObligation") as TextBox;
        //                txtMargin = e.Row.FindControl("txtMargin") as TextBox;
        //                Label lblMainCustomer = e.Row.FindControl("lblMainCustomer") as Label;
        //                if (ddlType != null)
        //                {
        //                    if (gvr.RowIndex == 0)
        //                    {
        //                        if (lblMainCustomer != null)
        //                        {
        //                            lblMainCustomer.Text = customerVo.FirstName + customerVo.LastName;
        //                            ddlType.Visible = false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ddlType.DataSource = dtCoBorrower;
        //                        ddlType.DataValueField = dtCoBorrower.Columns["AssociationId"].ToString();
        //                        ddlType.DataTextField = dtCoBorrower.Columns["AssociateName"].ToString();
        //                        ddlType.DataBind();
        //                        ddlType.Items.Insert(0, new ListItem("Select", "Select"));
        //                        ddlType.SelectedValue = dtTempCoBorrower.Rows[gvr.RowIndex]["CA_AssociationId"].ToString();
        //                        ddlType.Enabled = true;
        //                    }

        //                }
        //                if (liabilityVo.LoanTypeCode.ToString() == "1")
        //                {
        //                    if (gvr.RowIndex == 0)
        //                    {
        //                        txtAssetOwnership.Text = (100 - CB_Share).ToString();
        //                        txtAssetOwnership.Enabled = true;
        //                    }
        //                    else
        //                    {
        //                        if (txtAssetOwnership != null)
        //                        {
        //                            if (dtAssetOwnership != null)
        //                            {
        //                                txtAssetOwnership.Text = dtAssetOwnership.Rows[gvr.RowIndex - 1]["Share"].ToString();
        //                                txtAssetOwnership.Enabled = true;
        //                            }

        //                        }
        //                    }
        //                }
        //                if (txtObligation != null)
        //                {
        //                    txtObligation.Text = dtTempCoBorrower.Rows[gvr.RowIndex]["Loan Obligation %"].ToString();
        //                    txtObligation.Enabled = true;
        //                }
        //                if (txtMargin != null)
        //                {
        //                    txtMargin.Text = dtTempCoBorrower.Rows[gvr.RowIndex]["Margin %"].ToString();
        //                    txtMargin.Enabled = true;
        //                }
        //            }
        //        }
        //    }


        //    else
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {


        //            GridViewRow gvr = e.Row;
        //            if (gvr.RowIndex == 0)
        //            {
        //                DropDownList ddlType;
        //                Label lblMainCustomer;
        //                TextBox txtMargin;
        //                TextBox txtObligation = e.Row.FindControl("txtObligation") as TextBox;
        //                TextBox txtAssetOwnership = e.Row.FindControl("txtAssetOwnership") as TextBox;
        //                ddlType = e.Row.FindControl("ddlCoBorrowers") as DropDownList;
        //                lblMainCustomer = e.Row.FindControl("lblMainCustomer") as Label;
        //                txtMargin = e.Row.FindControl("txtMargin") as TextBox;

        //                if (ddlType != null)
        //                {

        //                    //ddlType.DataSource = dtCoBorrower;
        //                    //ddlType.DataValueField = dtCoBorrower.Columns["CA_AssociationId"].ToString();
        //                    //ddlType.DataTextField = dtCoBorrower.Columns["CustomerName"].ToString();
        //                    //ddlType.DataBind();
        //                    //ddlType.Items.Insert(0, new ListItem("Select", "Select"));
        //                    //ddlType.SelectedValue = dtTempCoBorrower.Rows[gvr.RowIndex]["CA_AssociationId"].ToString();
        //                    ddlType.Visible = false;

        //                }
        //                if (lblMainCustomer != null)
        //                {
        //                    lblMainCustomer.Text = customerVo.FirstName + customerVo.LastName;
        //                    lblMainCustomer.Enabled = true;
        //                }
        //                if (txtAssetOwnership != null)
        //                {
        //                    // txtObligation.Text = dtLiabilityAssociates.Rows[gvr.RowIndex]["CLA_LiabilitySharePer"].ToString();
        //                    txtAssetOwnership.Enabled = true;
        //                }
        //                if (txtObligation != null)
        //                {
        //                    txtObligation.Text = "";
        //                    txtObligation.Enabled = true;
        //                }
        //                if (txtMargin != null)
        //                {
        //                    txtMargin.Text = "";
        //                    txtMargin.Enabled = true;
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!rbtnYes.Checked && rbtnNo.Checked)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please add your asset...!');", true);
        //    }
        //}

        //protected void rbtnNo_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!rbtnYes.Checked && rbtnNo.Checked)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please add your asset...!');", true);
        //    }
        //}

        //protected void btnCoborrowers_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtNoCoBorrowers.Text.Trim() != "")
        //        {
        //            if (int.Parse(txtNoCoBorrowers.Text) > 0)
        //            {
        //                if (int.Parse(txtNoCoBorrowers.Text) > ddlGuarantor.Items.Count)
        //                {
        //                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Check the number of Co-Borrowers..!');", true);
        //                }
        //                else
        //                {
        //                    DataTable dt = new DataTable();
        //                    DataRow dr;
        //                    dt.Columns.Add("CLA_LiabilitiesAssociationId");
        //                    dt.Columns.Add("CA_AssociationId");
        //                    dt.Columns.Add("Loan Obligation %");
        //                    dt.Columns.Add("Margin %");
        //                    dr = dt.NewRow();
        //                    dr[0] = customerVo.FirstName + customerVo.LastName;
        //                    dr[1] = "";
        //                    dr[2] = "";
        //                    dt.Rows.Add(dr);
        //                    for (int i = 1; i <= int.Parse(txtNoCoBorrowers.Text); i++)
        //                    {
        //                        dr = dt.NewRow();
        //                        dr[0] = "";
        //                        dr[1] = "";
        //                        dr[2] = "";
        //                        dt.Rows.Add(dr);
        //                    }

        //                    gvCoBorrower.DataSource = dt;
        //                    gvCoBorrower.DataBind();
        //                    BindGridDroDown();
        //                    gvCoBorrower.Visible = true;
        //                }
        //            }
        //            else
        //                gvCoBorrower.Visible = false;
        //        }
        //        else
        //            gvCoBorrower.Visible = false;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "LiabilitiesMaintenanceForm.ascx.cs:btnCoborrowers_Click()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LiabilityView','none');", true);
        }

        //private void BindAssetsDropDown()
        //{
        //    DataSet dsAssets = new DataSet();
        //    Dictionary<int, string> dictAssets = new Dictionary<int, string>();
        //    PropertyBo propertyBo = new PropertyBo();
        //    PersonalBo personalBo = new PersonalBo();
        //    List<int> custBorrowerIds = new List<int>();
        //    CustomerFamilyVo familyVo;
        //    foreach (GridViewRow gvr in gvCoBorrower.Rows)
        //    {

        //        if (gvr.RowIndex != 0)
        //        {
        //            if ((DropDownList)gvr.FindControl("ddlCoBorrowers") != null)
        //            {
        //                DropDownList ddl1 = (DropDownList)gvr.FindControl("ddlCoBorrowers");
        //                familyVo = new CustomerFamilyVo();
        //                if (ddl1.SelectedValue.ToString() != "Select")
        //                    familyVo = customerFamilyBo.GetCustomerFamilyAssociateDetails(int.Parse(ddl1.SelectedValue.ToString()));
        //                custBorrowerIds.Add(familyVo.AssociateCustomerId);
        //                //  liabilityAssociateVo.AssociationId = int.Parse(ddl1.SelectedValue.ToString());

        //            }
        //        }
        //    }
        //    //if (txltNoCoBorrowers.Text.Trim() != "0" && txtNoCoBorrowers.Text.Trim() != "")
        //    //{
        //    //    for (int i = 1; i <= Convert.ToInt32(txtNoCoBorrowers.Text); i++)
        //    //    {
        //    //        DropDownList ddlBorrow = new DropDownList();
        //    //        ddlBorrow = (DropDownList)dvCoBorrowers.FindControl("ddlCoBorrower" + i.ToString());
        //    //        int custId = Convert.ToInt32(ddlBorrow.SelectedValue);
        //    //        custBorrowerIds.Add(custId);
        //    //    }
        //    //}

        //    if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "8")
        //    {
        //        dictAssets = propertyBo.GetPropertyDropDown(customerVo.CustomerId.ToString(), custBorrowerIds);

        //        ddlExistingAssets.DataSource = dictAssets;
        //        ddlExistingAssets.DataTextField = "Value";
        //        ddlExistingAssets.DataValueField = "Key";
        //        ddlExistingAssets.DataBind();
        //        ddlExistingAssets.Items.Insert(0, new ListItem("Pick One", "Pick One"));
        //    }
        //    //else if (ddlLoanType.SelectedValue == "2")
        //    //{
        //    //    dsAssets = personalBo.GetPersonalDropDown(ddlClientName.SelectedValue);

        //    //    ddlExistingAssets.DataSource = dsAssets.Tables[0];
        //    //    ddlExistingAssets.DataTextField = "AssetName";
        //    //    ddlExistingAssets.DataValueField = "AssetId";
        //    //    ddlExistingAssets.DataBind();
        //    //    ddlExistingAssets.Items.Insert(0, new ListItem("Pick One", "Pick One"));
        //    //}

        //}

        protected void ddlLender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLender.SelectedValue.ToString() == "15")
            {
                txtOtherLender.Visible = true;
            }
            else
            {
                txtOtherLender.Visible = false;
            }
        }

        protected void ddlPaymentOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentOption.SelectedValue.ToString() == "1")
            {
                trInstallmentHeader.Visible = false;
                trInstallment1.Visible = false;
                trInstallment2.Visible = false;
                trInstallment3.Visible = false;


                trLumpsum.Visible = true;
                CalcualteLumpSum();
            }
            else if (ddlPaymentOption.SelectedValue.ToString() == "2")
            {
                trInstallmentHeader.Visible = true;
                trInstallment1.Visible = true;
                trInstallment2.Visible = true;
                trInstallment3.Visible = true;

                trLumpsum.Visible = false;
                CalculateInstallmentAmount();
            }
            else
            {
                trInstallmentHeader.Visible = false;
                trInstallment1.Visible = false;
                trInstallment2.Visible = false;
                trInstallment3.Visible = false;

                trLumpsum.Visible = false;
            }


        }

        protected void ddlEMIFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {

            CalculateNumberOfInvestements();
            CalculateInstallmentAmount();

        }

        protected void ddlRepaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateNumberOfInvestements();
            CalculateInstallmentAmount();
        }

        protected void ddlCompoundFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentOption.SelectedValue.ToString() == "1")
            {
                CalcualteLumpSum();
            }

        }

        protected void txtTenture_TextChanged(object sender, EventArgs e)
        {
            if (ddlPaymentOption.SelectedValue.ToString() == "1")
            {
                CalcualteLumpSum();
            }
            else if (ddlPaymentOption.SelectedValue.ToString() == "2")
            {
                CalculateNumberOfInvestements();
                CalculateInstallmentAmount();
            }
        }

        protected void txtTenureMonths_TextChanged(object sender, EventArgs e)
        {
            if (ddlPaymentOption.SelectedValue.ToString() == "1")
            {
                CalcualteLumpSum();
            }
            else if (ddlPaymentOption.SelectedValue.ToString() == "2")
            {
                CalculateNumberOfInvestements();
                CalculateInstallmentAmount();
            }
        }

        protected void txtLoanAmount_TextChanged(object sender, EventArgs e)
        {
            if (ddlPaymentOption.SelectedValue.ToString() == "1")
            {
                CalcualteLumpSum();
            }
            else if (ddlPaymentOption.SelectedValue.ToString() == "2")
            {
                CalculateNumberOfInvestements();
                CalculateInstallmentAmount();
            }
        }

        protected void txtInterestRate_TextChanged(object sender, EventArgs e)
        {
            if (ddlPaymentOption.SelectedValue.ToString() == "1")
            {
                CalcualteLumpSum();
            }
            else if (ddlPaymentOption.SelectedValue.ToString() == "2")
            {
                CalculateNumberOfInvestements();
                CalculateInstallmentAmount();
            }
        }

        protected void txtInstallmentStartDt_TextChanged(object sender, EventArgs e)
        {
            if (ddlPaymentOption.SelectedValue.ToString() == "1")
            {
                CalcualteLumpSum();
            }
            else if (ddlPaymentOption.SelectedValue.ToString() == "2")
            {
                CalculateNumberOfInvestements();
                CalculateInstallmentAmount();
            }
        }

        protected void txtInstallmentEndDt_TextChanged(object sender, EventArgs e)
        {
            if (ddlPaymentOption.SelectedValue.ToString() == "1")
            {
                CalcualteLumpSum();
            }
            else if (ddlPaymentOption.SelectedValue.ToString() == "2")
            {
                CalculateNumberOfInvestements();
                CalculateInstallmentAmount();
            }
        }

    }
}
