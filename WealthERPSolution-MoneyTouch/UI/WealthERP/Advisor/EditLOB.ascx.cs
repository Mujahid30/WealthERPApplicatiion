using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoAdvisorProfiling;

using VoUser;
using BoCommon;
using BoUser;
using BoProductMaster;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace WealthERP.Advisor
{
    public partial class EditLOB : System.Web.UI.UserControl
    {
    
        AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
        ProductMFBo productMfBo = new ProductMFBo();
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        DataSet dsProductAmc;
        int  LOBId;
        string path = "";
        AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
           
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            LOBId = int.Parse(Session["LOBId"].ToString());
            advisorLOBVo = advisorLOBBo.GetLOB(LOBId);

            cvMFExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();
            cvInsExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();
            cvPostExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();
            cvRealEstExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();
            cvFixIncExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();
            cvLiabExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();

            if(!IsPostBack)
                editLOBDetails();

            if (Session["LOBGridAction"].ToString() == "LOBView")
            {
                trMandatory.Visible = false;
                DisableControls(this);
                lblView.Visible = true;
                lblEdit.Visible = false;
            }
            else if (Session["LOBGridAction"].ToString() == "LOBEdit")
            {
                trMandatory.Visible = true;
                lnkEdit.Visible = false;
                lblView.Visible = false;
                lblEdit.Visible = true;
            }
        }
     

        /// <summary>
        /// This method will loop through the child controls in user control and disable the controls.
        /// </summary>
        /// <param name="control"></param>
        private void DisableControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl.GetType().Name == "TextBox")
                {
                    ((TextBox)(ctrl)).Enabled = false;
                }
                else if (ctrl.GetType().Name == "DropDownList")
                {
                    ((DropDownList)(ctrl)).Enabled = false;

                }
                else if (ctrl.GetType().Name == "RadioButton")
                {
                    ((RadioButton)(ctrl)).Enabled = false;
                }
                else if (ctrl.GetType().Name == "Button")
                {
                    ((Button)(ctrl)).Visible = false;
                }
                else if (ctrl.GetType().Name == "CheckBox")
                {
                    ((CheckBox)(ctrl)).Enabled = false;
                }
                if (ctrl.Controls.Count > 0) // Loop through the inner controls
                {
                    DisableControls(ctrl);
                }
                
            }

        }

        private void EnableControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl.GetType().Name == "TextBox")
                {
                    ((TextBox)(ctrl)).Enabled = true;
                }
                else if (ctrl.GetType().Name == "DropDownList")
                {
                    ((DropDownList)(ctrl)).Enabled = true;

                }
                else if (ctrl.GetType().Name == "RadioButton")
                {
                    ((RadioButton)(ctrl)).Enabled = true;
                }
                else if (ctrl.GetType().Name == "Button")
                {
                    ((Button)(ctrl)).Visible = true;
                }
                else if (ctrl.GetType().Name == "CheckBox")
                {
                    ((CheckBox)(ctrl)).Enabled = true;
                }
                if (ctrl.Controls.Count > 0) // Loop through the inner controls
                {
                    EnableControls(ctrl);
                }
            }

        }

        public void editLOBDetails()
        {
            
            string lobCode = "";
            try
            {
              
                
                Session["LOBClassCode"] = advisorLOBVo.LOBClassificationCode.ToString();
                lobCode = advisorLOBVo.LOBClassificationCode.ToString();
                setVisibility();
                setLOBVisibility(lobCode);
               
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EdiEditLOB.ascx:editLOBDetails()");


                object[] objects = new object[3];

                objects[0] = path;
                objects[1] = advisorLOBVo;
                objects[2] = LOBId;



                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void setVisibility()
         {
            
                divMFDetails.Visible = false;
                BrokerCash.Visible = false;
                BrokerDerivative.Visible = false;
                EquitySubBrokerCash.Visible = false;
                EquitySubBrokerDerivative.Visible = false;
                RemissaryCash.Visible = false;
                RemissaryDerivative.Visible = false;
                //btnLogin.Visible = false;
                Insurance.Visible = false;
                PostalSavings.Visible = false;
                FixedIncome.Visible = false;
                RealEstate.Visible = false;
                Liabilities.Visible = false;
                PMSBrokerCash.Visible = false;
                PMSBrokerDerivative.Visible = false;
                PMSSubBrokerCash.Visible = false;
                PMSSubBrokerDerivative.Visible = false;
                PMSRemissaryCash.Visible = false;
                PMSRemissaryDerivative.Visible = false;
                CommoditiesBrokerCash.Visible = false;
                CommoditiesBrokerDerivatives.Visible = false;
                CommoditiesSubBrokerCash.Visible = false;
                CommoditiesSubBrokerDerivatives.Visible = false;
                CommoditiesRemissaryCash.Visible = false;
                CommoditiesRemissaryDerivatives.Visible = false;
                trBCBse.Visible = false;
                trBCNse.Visible = false;
                trBDBse.Visible = false;
                trBDNse.Visible = false;
                trCommBCBse.Visible = false;
                trCommBCNse.Visible = false;
                trCommBDBse.Visible = false;
                trCommBDNse.Visible = false;
                trCommRCBse.Visible = false;
                trCommRCNse.Visible = false;
                trCommRDBse.Visible = false;
                trCommRDNse.Visible = false;
                trCommSBCBse.Visible = false;
                trCommSBCNse.Visible = false;
                trCommSBDBse.Visible = false;
                trCommSBDBse.Visible = false;
                trCommSBDNse.Visible = false;
                trMFAddVariation.Visible = false;
                trPMSBCBse.Visible = false;
                trPMSBCNse.Visible = false;
                trPMSBDBse.Visible = false;
                trPMSBDNse.Visible = false;
                trPMSRCBse.Visible = false;
                trPMSRCNse.Visible = false;
                trPMSRDBse.Visible = false;
                trPMSRDNse.Visible = false;
                trPMSSBCBse.Visible = false;
                trPMSSBCNse.Visible = false;
                trPMSSBDBse.Visible = false;
                trPMSSBDNse.Visible = false;
                trRCBse.Visible = false;
                trRCNse.Visible = false;
                trRDBse.Visible = false;
                trRDNse.Visible = false;
                trSCBse.Visible = false;
                trSCNse.Visible = false;
                trSDBse.Visible = false;
                trSDNse.Visible = false;            
            
         }

        public void setLOBVisibility(string lobCode)
        {
            switch (lobCode)
            {
                case "LCBC":
                    setLCBC();
                    break;
                case "LCBD":
                    setLCBD();
                    break;
                case "LCRC":
                    setLCRC();
                    break;
                case "LCRD":
                    setLCRD();
                    break;
                case "LCSC":
                    setLCSC();
                    break;
                case "LCSD":
                    setLCSD();
                    break;
                case "LDSA":
                    setLDSA();
                    break;
                case "LEBC":
                    setLEBC();
                    break;
                case "LEBD":
                    setLEBD();
                    break;
                case "LERC":
                    setLERC();
                    break;
                case "LERD":
                    setLERD();
                    break;
                case "LESC":
                    setLESC();
                    break;
                case "LESD":
                    setLESD();
                    break;
                case "LFIA":
                    setLFIA();
                    break;
                case "LIAG":
                    setLIAG();
                    break;
                case "LMIT":
                    setLMIT();
                    break;
                case "LPAG":
                    setLPAG();
                    break;
                case "LPBC":
                    setLPBC();
                    break;
                case "LPBD":
                    setLPBD();
                    break;
                case "LPRC":
                    setLPRC();
                    break;
                case "LPRD":
                    setLPRD();
                    break;
                case "LPSC":
                    setLPSC();
                    break;
                case "LPSD":
                    setLPSD();
                    break;
                case "LREA":
                    setLREA();
                    break;

                    

            }

        }

        #region Commodities
        private void setLCBC()
        {
           LoadSubBrokerCode();
           CommoditiesBrokerCash.Visible = true;
           ddlCommBrCashBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
           ddlCommBrCashIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
           if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
           {
               trCommBCNse.Visible = true;
               txtCommBrCashNSENum.Text = advisorLOBVo.Identifier.ToString();
           }
           else
           {
               trCommBCBse.Visible = true;
               txtCommBrCashBSENum.Text = advisorLOBVo.Identifier.ToString();
           }
           
        }

        private void setLCBD()
        {
            LoadSubBrokerCode();
            CommoditiesBrokerDerivatives.Visible = true;
            ddlCommBrDerBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlCommBrDerIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            txtCommBrDerLicenseNum.Text = advisorLOBVo.LicenseNumber.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trCommBDNse.Visible = true;
                txtCommBrDerNSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trCommBDBse.Visible = true;
                txtCommBrDerBSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            
        }

        private void setLCRC()
        {
            LoadSubBrokerCode();
            CommoditiesRemissaryCash.Visible = true;
            ddlCommRemCashBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlCommRemissCashIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trCommRCNse.Visible = true;
                txtCommRemissCashNSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trCommRCNse.Visible = true;
                txtCommRemissCashBSENum.Text = advisorLOBVo.Identifier.ToString();
            }
           
        }

        private void setLCRD()
        {
            LoadSubBrokerCode();
             CommoditiesRemissaryDerivatives.Visible = true;
             ddlCommRemDerBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
             txtCommRemissDerLicenseNum.Text = advisorLOBVo.LicenseNumber.ToString();
             ddlCommRemissIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
             if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
             {
                 trCommRDNse.Visible = true;
                 txtCommRemissDerNSENum.Text = advisorLOBVo.Identifier.ToString();
             }
             else
             {
                 trCommRDBse.Visible = true;
                 txtCommRemissDerBSENum.Text = advisorLOBVo.Identifier.ToString();
             }
        }

        private void setLCSC()
        {
            LoadSubBrokerCode();
            CommoditiesSubBrokerCash.Visible = true;
            ddlCommCashBrokerCode.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlCommSubBrCashIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trCommSBCNse.Visible = true;
                txtCommSubBrCashNSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trCommSBCBse.Visible = true;
                txtCommSubBrCashBSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            
        }

        private void setLCSD()
        {
            LoadSubBrokerCode();
            CommoditiesSubBrokerDerivatives.Visible = true;
            ddlCommDeriBrokerCde.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlCommSubBrDerIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            txtCommSubBrDerLicenseNum.Text = advisorLOBVo.LicenseNumber.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trCommSBDNse.Visible = true;
                txtCommSubBrDerNSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trCommSBDBse.Visible = true;
                txtCommSubBrDerBSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            
        }

        //protected void ddlCommBrCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCommBrCashIdType.SelectedItem.Value == "BSE")
        //    {
        //        trCommBCBse.Visible = true;
        //        trCommBCNse.Visible = false;
        //    }
        //    else
        //        if (ddlCommBrCashIdType.SelectedItem.Value == "NSE")
        //        {
        //            trCommBCNse.Visible = true;
        //            trCommBCBse.Visible = false;
        //        }
        //        else
        //        {
        //            trCommBCBse.Visible = true;
        //            trCommBCNse.Visible = true;
        //        }
        //}

        //protected void ddlCommBrDerIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCommBrDerIdType.SelectedItem.Value == "BSE")
        //    {
        //        trCommBDBse.Visible = true;
        //        trCommBDNse.Visible = false;
        //    }
        //    else
        //        if (ddlCommBrDerIdType.SelectedItem.Value == "NSE")
        //        {
        //            trCommBDNse.Visible = true;
        //            trCommBDBse.Visible = false;
        //        }
        //        else
        //        {
        //            trCommBDBse.Visible = true;
        //            trCommBDNse.Visible = true;
        //        }
        //}

        //protected void ddlCommSubBrCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCommSubBrCashIdType.SelectedItem.Value == "BSE")
        //    {
        //        trCommSBCBse.Visible = true;
        //        trCommSBCNse.Visible = false;
        //    }
        //    else
        //        if (ddlCommSubBrCashIdType.SelectedItem.Value == "NSE")
        //        {
        //            trCommSBCNse.Visible = true;
        //            trCommSBCBse.Visible = false;
        //        }
        //        else
        //        {
        //            trCommSBCBse.Visible = true;
        //            trCommSBCNse.Visible = true;
        //        }
        //}

        //protected void ddlCommSubBrDerIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCommSubBrDerIdType.SelectedItem.Value == "BSE")
        //    {
        //        trCommSBDBse.Visible = true;
        //        trCommSBDNse.Visible = false;
        //    }
        //    else
        //        if (ddlCommSubBrDerIdType.SelectedItem.Value == "NSE")
        //        {
        //            trCommSBDNse.Visible = true;
        //            trCommSBDBse.Visible = false;
        //        }
        //        else
        //        {
        //            trCommSBDBse.Visible = true;
        //            trCommSBDNse.Visible = true;
        //        }
        //}

        //protected void ddlCommRemissCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCommRemissCashIdType.SelectedItem.Value == "BSE")
        //    {
        //        trCommRCBse.Visible = true;
        //        trCommRCNse.Visible = false;
        //    }
        //    else
        //        if (ddlCommRemissCashIdType.SelectedItem.Value == "NSE")
        //        {
        //            trCommRCNse.Visible = true;
        //            trCommRCBse.Visible = false;
        //        }
        //        else
        //        {
        //            trCommRCBse.Visible = true;
        //            trCommRCNse.Visible = true;
        //        }
        //}

        //protected void ddlCommRemissIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCommRemissIdType.SelectedItem.Value == "BSE")
        //    {
        //        trCommRDBse.Visible = true;
        //        trCommRDNse.Visible = false;
        //    }
        //    else
        //        if (ddlCommRemissIdType.SelectedItem.Value == "NSE")
        //        {
        //            trCommRDNse.Visible = true;
        //            trCommRDBse.Visible = false;
        //        }
        //        else
        //        {
        //            trCommRDBse.Visible = true;
        //            trCommRDNse.Visible = true;
        //        }
        //}

        protected void btnCommBrCash_Click(object sender, EventArgs e)
        {

            string assetClass = "CM";
            string category = "BKR";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommBrCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlCommBrCashBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommBrCashBrokerName.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommBrCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnCommBrCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCommBrDer_Click(object sender, EventArgs e)
        {
           
            string assetClass = "CM";
            string category = "BKR";
            string segment = "DER";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommBrDerIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlCommBrDerBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommBrDerBrokerName.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtCommBrDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnCommBrDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCommSubBrCash_Click(object sender, EventArgs e)
        {
            string assetClass = "CM";
            string category = "SBR";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommSubBrCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlCommCashBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommCashBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommSubBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommSubBrCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnCommSubBrCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCommSubBrDer_Click(object sender, EventArgs e)
        {
         
            string assetClass = "CM";
            string category = "SBR";
            string segment = "DER";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommSubBrDerIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                //advisorLOBVo.OrganizationName = txtCommSubBrDerBrokerName.Text.ToString();
                advisorLOBVo.OrganizationName = ddlCommDeriBrokerCde.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommDeriBrokerCde.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtCommSubBrDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommSubBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommSubBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnCommSubBrDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCommRemissCash_Click(object sender, EventArgs e)
        {
            string assetClass = "CM";
            string category = "REM";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommRemissCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlCommRemCashBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommRemCashBrokerName.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommRemissCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommRemissCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnCommRemissCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCommRemissDer_Click(object sender, EventArgs e)
        {
            string assetClass = "CM";
            string category = "REM";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommRemissIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlCommRemDerBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommRemDerBrokerName.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtCommRemissDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommRemissDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommRemissDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnCommRemissDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        #endregion Commodities

        #region Liabilities
        private void setLDSA()
        {
            Liabilities.Visible = true;
            txtLiabOrgName.Text = advisorLOBVo.OrganizationName.ToString();
            txtLiabAgencyExpiry.Text = advisorLOBVo.ValidityDate.ToShortDateString();
            if (advisorLOBVo.AgentNum != null)
            {
                txtLiabAgentNum.Text = advisorLOBVo.AgentNum.ToString();
            }
            if (advisorLOBVo.TargetAccount != null)
            {
                txtLiabTargetAccount.Text = advisorLOBVo.TargetAccount.ToString();
            }
            if (advisorLOBVo.TargetAmount != null)
            {
                txtLiabTargetAmt.Text = advisorLOBVo.TargetAmount.ToString();
            }
            if (advisorLOBVo.AgentType == "CLA")
            {
                chkLiabCarLoan.Checked = true;
                chkLiabPerLoan.Enabled = false;
                chkLiabBothLoan.Enabled = false;
            }
            else if (advisorLOBVo.AgentType == "PLA")
            {
                chkLiabPerLoan.Checked = true;
                chkLiabCarLoan.Enabled = false;
                chkLiabBothLoan.Enabled = false;
            }
            else
            {
                chkLiabBothLoan.Checked = true;
                chkLiabPerLoan.Enabled = false;
                chkLiabCarLoan.Enabled = false;
            }
        }
        protected void btnLiab_Click(object sender, EventArgs e)
        {

            string assetClass = "DSP";
            string category = "AGN";
            string segment = "";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                if (chkLiabCarLoan.Checked)
                    advisorLOBVo.AgentType = "CLA";
                else if (chkLiabPerLoan.Checked)
                    advisorLOBVo.AgentType = "PLA";
                else
                    advisorLOBVo.AgentType = "CPLA";

                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = txtLiabOrgName.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtLiabAgencyExpiry.Text.ToString());
                advisorLOBVo.AgentNum = txtLiabAgentNum.Text.ToString();
                if (txtLiabTargetAccount.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAccount = float.Parse(txtLiabTargetAccount.Text.ToString().Trim());
                if (txtLiabTargetAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAmount = double.Parse(txtLiabTargetAmt.Text.ToString().Trim());

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);


                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnLiab_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;

                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void chkLiabCarLoan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLiabCarLoan.Checked)
            {
                chkLiabPerLoan.Enabled = false;
                chkLiabBothLoan.Enabled = false;
            }
            else
            {
                chkLiabPerLoan.Enabled = true;
                chkLiabBothLoan.Enabled = true;
            }
        }

        protected void chkLiabPerLoan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLiabPerLoan.Checked)
            {
                chkLiabCarLoan.Enabled = false;
                chkLiabBothLoan.Enabled = false;
            }
            else
            {
                chkLiabCarLoan.Enabled = true;
                chkLiabBothLoan.Enabled = true;
            }
        }

        protected void chkLiabBothLoan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLiabBothLoan.Checked)
            {
                chkLiabCarLoan.Enabled = false;
                chkLiabPerLoan.Enabled = false;
            }
            else
            {
                chkLiabCarLoan.Enabled = true;
                chkLiabPerLoan.Enabled = true;
            }
        }


        #endregion Liabilities

        #region Equity
        private void setLEBC()
        {
            LoadSubBrokerCode();
            BrokerCash.Visible = true;
            ddlEqBrCashBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlBrokerCashId.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            if(advisorLOBVo.IdentifierTypeCode.ToString()=="NSE")
            {
                trBCNse.Visible = true;
                txtBrokerCashNseNumber.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trBCBse.Visible=true;
                txtBrokerCashBseNumber.Text = advisorLOBVo.Identifier.ToString();
            }
        }
        private void setLEBD()
        {
            LoadSubBrokerCode();
            BrokerDerivative.Visible = true;
            ddlEqBrDerBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlBrokerDeivIdentifier.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            txtBrokerDerivLicenseNumber.Text = advisorLOBVo.LicenseNumber.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trBDNse.Visible = true;
                txtBrokerDerivNseNUmber.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trBDBse.Visible = true;
                txtBrokerDerivBseNUmber.Text = advisorLOBVo.Identifier.ToString();
            }
        }
        private void setLERC()
        {
            LoadSubBrokerCode();
            RemissaryCash.Visible = true;
            ddlEqRemCashBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlRemissCashIdentifier.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trRCNse.Visible = true;
                txtRemissCashNseNumber.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trRCBse.Visible = true;
                txtRemissCashBseNumber.Text = advisorLOBVo.Identifier.ToString();
            }
        }
        private void setLERD()
        {
            LoadSubBrokerCode();
            RemissaryDerivative.Visible = true;
            ddlEqRemDerBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            txtRemissDerivLicenseNumber.Text = advisorLOBVo.LicenseNumber.ToString();
            ddlRemissDerivIdentifer.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trRDNse.Visible = true;
                txtRemissDerivNseNumber.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trRDBse.Visible = true;
                txtRemissDerivBseNumber.Text = advisorLOBVo.Identifier.ToString();
            }
            
        }
        private void setLESC()
        {
            LoadSubBrokerCode();
            EquitySubBrokerCash.Visible = true;
            ddlBrokerCode.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlSubCashIdentifier.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trSCNse.Visible = true;
                txtSubCashNseNumber.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trSCBse.Visible = true;
                txtSubCashBseNumber.Text = advisorLOBVo.Identifier.ToString();
            }
        }
        private void setLESD()
        {
            LoadSubBrokerCode();
            EquitySubBrokerDerivative.Visible = true;
            ddlSubBrokerCode.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlSubDerivIdentifier.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            txtSubDerivLicenseNumber.Text = advisorLOBVo.LicenseNumber.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trSDNse.Visible = true;
                txtSubDerivNseNumber.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trSDBse.Visible = true;
                txtSubDerivBseNumber.Text = advisorLOBVo.Identifier.ToString();
            }
        }
        protected void btnBrokerCash_Click(object sender, EventArgs e)
        {

            string assetClass = "EQ";
            string category = "BKR";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string identifierType = ddlBrokerCashId.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                //  advisorLOBVo.LOBId = advisorBo.getId();
                advisorLOBVo.OrganizationName = ddlEqBrCashBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlEqBrCashBrokerName.SelectedItem.Value.ToString();
                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;
                advisorLOBVo.LicenseNumber = " ";
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtBrokerCashBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                else if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtBrokerCashNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }
                advisorLOBBo.UpdateLOB(advisorLOBVo, advisorId, userId);
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnBrokerCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        //protected void ddlBrokerCashId_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlBrokerCashId.SelectedItem.Value == "BSE")
        //    {
        //        trBCNse.Visible = false;
        //        trBCBse.Visible = true;

        //    }
        //    else if (ddlBrokerCashId.SelectedItem.Value == "NSE")
        //    {
        //        trBCNse.Visible = true;
        //        trBCBse.Visible = false;

        //    }
        //}

        protected void btnBrokerDeriv_Click(object sender, EventArgs e)
        {


            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string assetClass = "EQ";
            string category = "BKR";
            string segment = "DER";
            string identifierType = ddlBrokerDeivIdentifier.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                //advisorLOBVo.LOBId = advisorBo.getId();

                advisorLOBVo.OrganizationName = ddlEqBrDerBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlEqBrDerBrokerName.SelectedItem.Value.ToString();
                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;
                advisorLOBVo.LicenseNumber = txtBrokerDerivLicenseNumber.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtBrokerDerivBseNUmber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";

                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtBrokerDerivNseNUmber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";

                }
                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                btnBrokerDeriv.Enabled = false;

                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnBrokerDeriv_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        //protected void ddlBrokerDeivIdentifier_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (ddlBrokerDeivIdentifier.SelectedItem.Value == "BSE")
        //    {
        //        trBDBse.Visible = true;
        //        trBDNse.Visible = false;
        //    }
        //    else if (ddlBrokerDeivIdentifier.SelectedItem.Value == "NSE")
        //    {
        //        trBDNse.Visible = true;
        //        trBDBse.Visible = false;

        //    }
        //}

        //protected void ddlSubCashIdentifier_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlSubCashIdentifier.SelectedItem.Value == "BSE")
        //    {
        //        trSCNse.Visible = false;
        //        trSCBse.Visible = true;

        //    }
        //    if (ddlSubCashIdentifier.SelectedItem.Value == "NSE")
        //    {
        //        trSCBse.Visible = false;
        //        trSCNse.Visible = true;

        //    }
        //    if (ddlSubCashIdentifier.SelectedItem.Value == "BOTH BSE and NSE")
        //    {
        //        trSCNse.Visible = true;
        //        trSCBse.Visible = true;

        //    }
        //}

        protected void btnSubCash_Click(object sender, EventArgs e)
        {


            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string assetClass = "EQ";
            string category = "SBR";
            string segment = "CSH";
            string identifierType = ddlSubCashIdentifier.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlBrokerCode.SelectedItem.Value.ToString();
                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;
                advisorLOBVo.LicenseNumber = " ";
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtSubCashBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";

                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtSubCashNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";

                }

                btnSubCash.Enabled = false;
                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnSubCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;

                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        //protected void ddlSubDerivIdentifier_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlSubDerivIdentifier.SelectedItem.Value == "BSE")
        //    {
        //        trSDBse.Visible = true;
        //        trSDNse.Visible = false;

        //    }
        //    if (ddlSubDerivIdentifier.SelectedItem.Value == "NSE")
        //    {
        //        trSDBse.Visible = false;
        //        trSDNse.Visible = true;

        //    }
        //    if (ddlSubDerivIdentifier.SelectedItem.Value == "BOTH BSE and NSE")
        //    {
        //        trSDBse.Visible = true;
        //        trSDNse.Visible = true;

        //    }
        //}

        protected void btnSubDeriv_Click(object sender, EventArgs e)
        {


            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string assetClass = "EQ";
            string category = "SBR";
            string segment = "DER";
            string identifierType = ddlSubDerivIdentifier.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);


                advisorLOBVo.BrokerCode = ddlSubBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.OrganizationName = ddlSubBrokerCode.SelectedItem.Text.ToString();
                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;
                advisorLOBVo.LicenseNumber = txtSubDerivLicenseNumber.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtSubDerivBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtSubDerivNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }


                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnSubDeriv_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        //protected void ddlRemissCashIdentifier_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (ddlRemissCashIdentifier.SelectedItem.Value == "BSE")
        //    {
        //        trRCBse.Visible = true;
        //        trRCNse.Visible = false;
        //    }
        //    if (ddlRemissCashIdentifier.SelectedItem.Value == "NSE")
        //    {
        //        trRCBse.Visible = false;
        //        trRCNse.Visible = true;
        //    }
        //    if (ddlRemissCashIdentifier.SelectedItem.Value == "BOTH BSE and NSE")
        //    {
        //        trRCBse.Visible = true;
        //        trRCNse.Visible = true;
        //    }
        //}

        protected void btnRemissCash_Click(object sender, EventArgs e)
        {

            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string assetClass = "EQ";
            string category = "REM";
            string segment = "CSH";
            string identifierType = ddlRemissCashIdentifier.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                // advisorLOBVo.LOBId = advisorBo.getId();
                advisorLOBVo.OrganizationName = ddlEqRemCashBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlEqRemCashBrokerName.SelectedItem.Value.ToString();
                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;
                advisorLOBVo.LicenseNumber = " ";
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtRemissCashBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtRemissCashNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnRemissCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        //protected void ddlRemissDerivIdentifer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlRemissDerivIdentifer.SelectedItem.Value == "BSE")
        //    {
        //        trRDBse.Visible = true;
        //        trRDNse.Visible = false;


        //    }
        //    if (ddlRemissDerivIdentifer.SelectedItem.Value == "NSE")
        //    {
        //        trRDBse.Visible = false;
        //        trRDNse.Visible = true;


        //    }
        //    if (ddlRemissDerivIdentifer.SelectedItem.Value == "BOTH BSE and NSE")
        //    {
        //        trRDBse.Visible = true;
        //        trRDNse.Visible = true;

        //    }
        //}

        protected void btnRemissDeriv_Click(object sender, EventArgs e)
        {


            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string assetClass = "EQ";
            string category = "REM";
            string segment = "DER";
            string identifierType = ddlRemissDerivIdentifer.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                // advisorLOBVo.LOBId = advisorBo.getId();
                advisorLOBVo.OrganizationName = ddlEqRemDerBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlEqRemDerBrokerName.SelectedItem.Value.ToString();
                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;

                advisorLOBVo.LicenseNumber = txtRemissDerivLicenseNumber.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtRemissDerivBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";

                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtRemissDerivNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";

                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnRemissDeriv_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        #endregion Equity

        #region Fixed Income
        private void setLFIA()
        {
            FixedIncome.Visible = true;
            txtFixIncOrgName.Text = advisorLOBVo.OrganizationName.ToString();
            txtFixIncAgencyExpiry.Text = advisorLOBVo.ValidityDate.ToShortDateString();
            txtFixIncAgentNum.Text = advisorLOBVo.AgentNum.ToString();
            txtFixIncTargetAccount.Text = advisorLOBVo.TargetAccount.ToString();
            txtFixIncTargetAmt.Text = advisorLOBVo.TargetAmount.ToString();
        }
        protected void btnFixInc_Click(object sender, EventArgs e)
        {

            string assetClass = "FI";
            string category = "AGN";
            string segment = "";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = txtFixIncOrgName.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtFixIncAgencyExpiry.Text.ToString());
                advisorLOBVo.AgentNum = txtFixIncAgentNum.Text.ToString();
                if (txtFixIncTargetAccount.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAccount = float.Parse(txtFixIncTargetAccount.Text.ToString().Trim());
                if (txtFixIncTargetAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAmount = double.Parse(txtFixIncTargetAmt.Text.ToString().Trim());

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);


                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnFixInc_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        #endregion Fixed Income

        #region Insurance
        private void setLIAG()
        {
            Insurance.Visible = true;
            txtInsOrgName.Text = advisorLOBVo.OrganizationName.ToString();
            txtInsIRDANum.Text = advisorLOBVo.Identifier.ToString();
            txtInsAgentNum.Text = advisorLOBVo.AgentNum.ToString();
            txtInsAgencyExpiry.Text = advisorLOBVo.ValidityDate.ToShortDateString();
            txtInsTargetPolicies.Text = advisorLOBVo.TargetAccount.ToString();
            txtInsTargetSumAssuredAmt.Text = advisorLOBVo.TargetAmount.ToString();
            txtInsTargetPremiumAmt.Text = advisorLOBVo.TargetPremiumAmount.ToString();
        }
        protected void btnIns_Click(object sender, EventArgs e)
        {

            string assetClass = "INS";
            string category = "AGN";
            string segment = "";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;


            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = txtInsOrgName.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtInsAgencyExpiry.Text.ToString());
                advisorLOBVo.AgentNum = txtInsAgentNum.Text.ToString();
                advisorLOBVo.Identifier = txtInsIRDANum.Text.ToString();
                if (txtInsTargetPolicies.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAccount = float.Parse(txtInsTargetPolicies.Text.ToString().Trim());
                if (txtInsTargetSumAssuredAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAmount = double.Parse(txtInsTargetSumAssuredAmt.Text.ToString().Trim());
                if (txtInsTargetPremiumAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetPremiumAmount = double.Parse(txtInsTargetPremiumAmt.Text.ToString().Trim());

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);


                PageRedirect();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnIns_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;

                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        #endregion Insurance

        #region Mutual Fund
        private void setLMIT()
        {
            divMFDetails.Visible = true;
            txtMFARNCode.Text = advisorLOBVo.Identifier.ToString();
            txtMFOrgName.Text = advisorLOBVo.OrganizationName.ToString();
            txtMFValidity.Text = advisorLOBVo.ValidityDate.ToShortDateString();
        }
        protected void btnMFSubmit_Click(object sender, EventArgs e)
        {

            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string segment = "";
            string assetClass = "MF";
            string category = "INT";
            try
            {


                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.IdentifierTypeCode = "ARN";
                advisorLOBVo.OrganizationName = txtMFOrgName.Text.ToString();
                advisorLOBVo.Identifier = txtMFARNCode.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtMFValidity.Text.ToString());
                advisorLOBVo.LicenseNumber = "";
                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                btnMFSubmit.Enabled = false;
                divMFDetails.Visible = false;
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnMFSubmit_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void btnAddARNVariations_Click(object sender, EventArgs e)
        {
            trMFAddVariation.Visible = true;
            btnAddARNVariations.Enabled = false;
        }

        protected void btnAddVariant_Click(object sender, EventArgs e)
        {
            //System.Text.StringBuilder sb =new System.Text.StringBuilder();
            //sb.Append(";" + txtMFARNVariation.Text.ToString()); 
            txtMFARNCode.Text += ";" + txtMFARNVariation.Text.ToString().Trim();
            txtMFARNVariation.Text = String.Empty;
            trMFAddVariation.Visible = false;
            btnAddARNVariations.Enabled = true;
        }

        #endregion Mutual Fund

        #region PMS
        private void setLPBC()
        {
            LoadSubBrokerCode();
            PMSBrokerCash.Visible = true;
            ddlPMSBrCashBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlPMSBrCashIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trPMSBCNse.Visible = true;
                txtPMSBrCashNSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trPMSBCBse.Visible = true;
                txtPMSBrCashBSENum.Text = advisorLOBVo.Identifier.ToString();
            }
        }
        private void setLPBD()
        {
            LoadSubBrokerCode();
            PMSBrokerDerivative.Visible = true;

            ddlPMSBrDerBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlPMSBrDerIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            txtPMSBrDerLicenseNum.Text = advisorLOBVo.LicenseNumber.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trPMSBDNse.Visible = true;
                txtPMSBrDerNSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trPMSBDBse.Visible = true;
                txtPMSBrDerBSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            
        }
        private void setLPRC()
        {
            LoadSubBrokerCode();
            PMSRemissaryCash.Visible = true;
            ddlPMSRemCashBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlPMSRemissCashIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trPMSRCNse.Visible = true;
                txtPMSRemissCashNSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trPMSRCBse.Visible = true;
                txtPMSRemissCashBSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            
        }
        private void setLPRD()
        {
            LoadSubBrokerCode();
           PMSRemissaryDerivative.Visible = true;
           ddlPMSRemDerBrokerName.SelectedValue = advisorLOBVo.BrokerCode.ToString();
           txtPMSRemissDerLicenseNum.Text = advisorLOBVo.LicenseNumber.ToString();
           ddlPMSRemissDerIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
           if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
           {
               trPMSRDBse.Visible = true;
               txtPMSRemissDerNSENum.Text = advisorLOBVo.Identifier.ToString();
           }
           else
           {
               trRDBse.Visible = true;
               txtPMSRemissDerBSENum.Text = advisorLOBVo.Identifier.ToString();
           }
          
        }
        private void setLPSC()
        {
            LoadSubBrokerCode();
            PMSSubBrokerCash.Visible = true;
            ddlPMSBrokerCode.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlPMSSubBrCashIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trPMSSBCNse.Visible = true;
                txtPMSSubBrCashNSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trPMSSBCBse.Visible = true;
                txtPMSSubBrCashBSENum.Text = advisorLOBVo.Identifier.ToString();
            }
           
        }
        private void setLPSD()
        {
            LoadSubBrokerCode();
            PMSSubBrokerDerivative.Visible = true;
            ddlPMSDeriBrokerCode.SelectedValue = advisorLOBVo.BrokerCode.ToString();
            ddlPMSBrDerIdType.SelectedValue = advisorLOBVo.IdentifierTypeCode.ToString();
            txtPMSSubBrDerLicenseNum.Text = advisorLOBVo.LicenseNumber.ToString();
            if (advisorLOBVo.IdentifierTypeCode.ToString() == "NSE")
            {
                trPMSSBDNse.Visible = true;
                txtPMSSubBrDerNSENum.Text = advisorLOBVo.Identifier.ToString();
            }
            else
            {
                trPMSSBDBse.Visible = true;
                txtPMSSubBrDerBSENum.Text = advisorLOBVo.Identifier.ToString();
            }

        }

        //protected void ddlPMSBrCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPMSBrCashIdType.SelectedItem.Value == "BSE")
        //    {
        //        trPMSBCBse.Visible = true;
        //        trPMSBCNse.Visible = false;
        //    }
        //    else
        //        if (ddlPMSBrCashIdType.SelectedItem.Value == "NSE")
        //        {
        //            trPMSBCNse.Visible = true;
        //            trPMSBCBse.Visible = false;
        //        }
        //        else
        //        {
        //            trPMSBCBse.Visible = true;
        //            trPMSBCNse.Visible = true;
        //        }
        //}

        //protected void ddlPMSBrDerIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPMSBrDerIdType.SelectedItem.Value == "BSE")
        //    {
        //        trPMSBDBse.Visible = true;
        //        trPMSBDNse.Visible = false;
        //    }
        //    else
        //        if (ddlPMSBrDerIdType.SelectedItem.Value == "NSE")
        //        {
        //            trPMSBDNse.Visible = true;
        //            trPMSBDBse.Visible = false;
        //        }
        //        else
        //        {
        //            trPMSBDBse.Visible = true;
        //            trPMSBDNse.Visible = true;
        //        }
        //}

        //protected void ddlPMSSubBrCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPMSSubBrCashIdType.SelectedItem.Value == "BSE")
        //    {
        //        trPMSSBCBse.Visible = true;
        //        trPMSSBCNse.Visible = false;
        //    }
        //    else
        //        if (ddlPMSSubBrCashIdType.SelectedItem.Value == "NSE")
        //        {
        //            trPMSSBCNse.Visible = true;
        //            trPMSSBCBse.Visible = false;
        //        }
        //        else
        //        {
        //            trPMSSBCBse.Visible = true;
        //            trPMSSBCNse.Visible = true;
        //        }
        //}

        //protected void ddlPMSSubBrDerIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPMSSubBrDerIdType.SelectedItem.Value == "BSE")
        //    {
        //        trPMSSBDBse.Visible = true;
        //        trPMSSBDNse.Visible = false;
        //    }
        //    else
        //        if (ddlPMSSubBrDerIdType.SelectedItem.Value == "NSE")
        //        {
        //            trPMSSBDNse.Visible = true;
        //            trPMSSBDBse.Visible = false;
        //        }
        //        else
        //        {
        //            trPMSSBDBse.Visible = true;
        //            trPMSSBDNse.Visible = true;
        //        }

        //}

        //protected void ddlPMSRemissCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPMSRemissCashIdType.SelectedItem.Value == "BSE")
        //    {
        //        trPMSRCBse.Visible = true;
        //        trPMSRCNse.Visible = false;
        //    }
        //    else
        //        if (ddlPMSRemissCashIdType.SelectedItem.Value == "NSE")
        //        {
        //            trPMSRCNse.Visible = true;
        //            trPMSRCBse.Visible = false;
        //        }
        //        else
        //        {
        //            trPMSRCBse.Visible = true;
        //            trPMSRCNse.Visible = true;
        //        }

        //}

        //protected void ddlPMSRemissDerIdType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPMSRemissDerIdType.SelectedItem.Value == "BSE")
        //    {
        //        trPMSRDBse.Visible = true;
        //        trPMSRDNse.Visible = false;
        //    }
        //    else
        //        if (ddlPMSRemissDerIdType.SelectedItem.Value == "NSE")
        //        {
        //            trPMSRDNse.Visible = true;
        //            trPMSRDBse.Visible = false;
        //        }
        //        else
        //        {
        //            trPMSRDBse.Visible = true;
        //            trPMSRDNse.Visible = true;
        //        }
        //}

        protected void btnPMSBrCash_Click(object sender, EventArgs e)
        {

            string assetClass = "PMS";
            string category = "BKR";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlPMSBrCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                //  advisorLOBVo.LOBId = advisorBo.getId();
                advisorLOBVo.OrganizationName = ddlPMSBrCashBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSBrCashBrokerName.SelectedItem.Value.ToString();
                //DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtBrokerCashNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }
                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);

                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnPMSBrCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnPMSBrDer_Click(object sender, EventArgs e)
        {

            string assetClass = "PMS";
            string category = "BKR";
            string segment = "DER";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;



            string identifierType = ddlPMSBrDerIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlPMSBrDerBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSBrDerBrokerName.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtPMSBrDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtPMSBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnPMSBrDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnPMSSubBrCash_Click(object sender, EventArgs e)
        {

            string assetClass = "PMS";
            string category = "SBR";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;



            string identifierType = ddlPMSSubBrCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlPMSBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSSubBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";

                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtPMSSubBrCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";

                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnPMSSubBrCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnPMSSubBrDer_Click(object sender, EventArgs e)
        {

            string assetClass = "PMS";
            string category = "SBR";
            string segment = "DER";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlPMSSubBrDerIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlPMSDeriBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSDeriBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtPMSSubBrDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSSubBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtPMSSubBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnPMSSubBrDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnPMSRemissCash_Click(object sender, EventArgs e)
        {

            string assetClass = "PMS";
            string category = "REM";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlPMSRemissCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlPMSRemCashBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSRemCashBrokerName.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSRemissCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtPMSRemissCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnPMSRemissCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnPMSRemissDer_Click(object sender, EventArgs e)
        {

            string assetClass = "PMS";
            string category = "REM";
            string segment = "DER";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlPMSRemissDerIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlPMSRemDerBrokerName.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSRemDerBrokerName.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtPMSRemissDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSRemissDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtPMSRemissDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                }

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnPMSRemissDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;

                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        #endregion PMS

        #region Real Estate
        private void setLREA()
        {
            RealEstate.Visible = true;
            txtRealEstOrgName.Text = advisorLOBVo.OrganizationName.ToString();
            txtRealEstAgencyExpiry.Text = advisorLOBVo.ValidityDate.ToShortDateString();
            txtRealEstAgentNum.Text = advisorLOBVo.AgentNum.ToString();
            txtRealEstTargetAccount.Text = advisorLOBVo.TargetAccount.ToString();
            txtRealEstTargetAmt.Text = advisorLOBVo.TargetAmount.ToString();
        }
        protected void btnRealEst_Click(object sender, EventArgs e)
        {

            string assetClass = "RE";
            string category = "AGN";
            string segment = "";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = txtRealEstOrgName.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtRealEstAgencyExpiry.Text.ToString());
                advisorLOBVo.AgentNum = txtRealEstAgentNum.Text.ToString();
                if (txtRealEstTargetAccount.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAccount = float.Parse(txtRealEstTargetAccount.Text.ToString().Trim());
                if (txtRealEstTargetAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAmount = double.Parse(txtRealEstTargetAmt.Text.ToString().Trim());

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);


                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnRealEst_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        #endregion Real Estate

        #region Postal
        private void setLPAG()
        {
            PostalSavings.Visible = true;
            txtPostalOrgName.Text = advisorLOBVo.OrganizationName.ToString();
            txtPostalAgentNum.Text = advisorLOBVo.AgentNum.ToString();
            txtPostalAgencyExpiry.Text = advisorLOBVo.ValidityDate.ToShortDateString();
            txtPostalTargetAccount.Text = advisorLOBVo.TargetAccount.ToString();
            txtPostalTargetAmt.Text = advisorLOBVo.TargetAmount.ToString();
        }
        protected void btnPostal_Click(object sender, EventArgs e)
        {

            string assetClass = "PS";
            string category = "AGN";
            string segment = "";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = txtPostalOrgName.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtPostalAgencyExpiry.Text.ToString());
                advisorLOBVo.AgentNum = txtPostalAgentNum.Text.ToString();
                if (txtPostalTargetAccount.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAccount = float.Parse(txtPostalTargetAccount.Text.ToString().Trim());
                if (txtPostalTargetAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAmount = double.Parse(txtPostalTargetAmt.Text.ToString().Trim());

                advisorLOBBo.UpdateLOB(advisorLOBVo,advisorId,userId);


                PageRedirect();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditLOB.ascx:btnPostal_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        #endregion Postal


        private void LoadSubBrokerCode()
        {
            dsProductAmc = productMfBo.GetBrokerCodeForLOB();

            ddlSubBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlSubBrokerCode.DataTextField = "XB_BrokerName";
            ddlSubBrokerCode.DataValueField = "XB_BrokerCode";
            ddlSubBrokerCode.DataBind();
            ddlSubBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlBrokerCode.DataTextField = "XB_BrokerName";
            ddlBrokerCode.DataValueField = "XB_BrokerCode";
            ddlBrokerCode.DataBind();
            ddlBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlEqBrCashBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlEqBrCashBrokerName.DataTextField = "XB_BrokerName";
            ddlEqBrCashBrokerName.DataValueField = "XB_BrokerCode";
            ddlEqBrCashBrokerName.DataBind();
            ddlEqBrCashBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlEqBrDerBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlEqBrDerBrokerName.DataTextField = "XB_BrokerName";
            ddlEqBrDerBrokerName.DataValueField = "XB_BrokerCode";
            ddlEqBrDerBrokerName.DataBind();
            ddlEqBrDerBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlEqRemCashBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlEqRemCashBrokerName.DataTextField = "XB_BrokerName";
            ddlEqRemCashBrokerName.DataValueField = "XB_BrokerCode";
            ddlEqRemCashBrokerName.DataBind();
            ddlEqRemCashBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlEqRemDerBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlEqRemDerBrokerName.DataTextField = "XB_BrokerName";
            ddlEqRemDerBrokerName.DataValueField = "XB_BrokerCode";
            ddlEqRemDerBrokerName.DataBind();
            ddlEqRemDerBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));


            ddlPMSBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlPMSBrokerCode.DataTextField = "XB_BrokerName";
            ddlPMSBrokerCode.DataValueField = "XB_BrokerCode";
            ddlPMSBrokerCode.DataBind();
            ddlPMSBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));


            ddlPMSDeriBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlPMSDeriBrokerCode.DataTextField = "XB_BrokerName";
            ddlPMSDeriBrokerCode.DataValueField = "XB_BrokerCode";
            ddlPMSDeriBrokerCode.DataBind();
            ddlPMSDeriBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlPMSBrCashBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlPMSBrCashBrokerName.DataTextField = "XB_BrokerName";
            ddlPMSBrCashBrokerName.DataValueField = "XB_BrokerCode";
            ddlPMSBrCashBrokerName.DataBind();
            ddlPMSBrCashBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlPMSBrDerBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlPMSBrDerBrokerName.DataTextField = "XB_BrokerName";
            ddlPMSBrDerBrokerName.DataValueField = "XB_BrokerCode";
            ddlPMSBrDerBrokerName.DataBind();
            ddlPMSBrDerBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlPMSRemCashBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlPMSRemCashBrokerName.DataTextField = "XB_BrokerName";
            ddlPMSRemCashBrokerName.DataValueField = "XB_BrokerCode";
            ddlPMSRemCashBrokerName.DataBind();
            ddlPMSRemCashBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlPMSRemDerBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlPMSRemDerBrokerName.DataTextField = "XB_BrokerName";
            ddlPMSRemDerBrokerName.DataValueField = "XB_BrokerCode";
            ddlPMSRemDerBrokerName.DataBind();
            ddlPMSRemDerBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlCommCashBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlCommCashBrokerCode.DataTextField = "XB_BrokerName";
            ddlCommCashBrokerCode.DataValueField = "XB_BrokerCode";
            ddlCommCashBrokerCode.DataBind();
            ddlCommCashBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));


            ddlCommDeriBrokerCde.DataSource = dsProductAmc.Tables[0];
            ddlCommDeriBrokerCde.DataTextField = "XB_BrokerName";
            ddlCommDeriBrokerCde.DataValueField = "XB_BrokerCode";
            ddlCommDeriBrokerCde.DataBind();
            ddlCommDeriBrokerCde.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlCommBrCashBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlCommBrCashBrokerName.DataTextField = "XB_BrokerName";
            ddlCommBrCashBrokerName.DataValueField = "XB_BrokerCode";
            ddlCommBrCashBrokerName.DataBind();
            ddlCommBrCashBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlCommBrDerBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlCommBrDerBrokerName.DataTextField = "XB_BrokerName";
            ddlCommBrDerBrokerName.DataValueField = "XB_BrokerCode";
            ddlCommBrDerBrokerName.DataBind();
            ddlCommBrDerBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlCommRemCashBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlCommRemCashBrokerName.DataTextField = "XB_BrokerName";
            ddlCommRemCashBrokerName.DataValueField = "XB_BrokerCode";
            ddlCommRemCashBrokerName.DataBind();
            ddlCommRemCashBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlCommRemDerBrokerName.DataSource = dsProductAmc.Tables[0];
            ddlCommRemDerBrokerName.DataTextField = "XB_BrokerName";
            ddlCommRemDerBrokerName.DataValueField = "XB_BrokerCode";
            ddlCommRemDerBrokerName.DataBind();
            ddlCommRemDerBrokerName.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            // txtMFValidity.Text = Calendar2.SelectedDate.ToShortDateString();
        }    
               
        private void PageRedirect()
        {
            if (Session["LOBGridAction"] != null)
            {
                if (Session["LOBGridAction"].ToString() == "LOBEdit")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFFAdd','?action=ViewLOB');", true);
                }
                else if (Session["LOBGridAction"].ToString() == "AdvisorLOBEdit")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewLOB','?action=ViewLOB');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewLOB','?action=ViewLOB');", true);
            }
                
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);

        }

        private void DeleteLOB()
        {
            bool res = false;
            try
            {

                LOBId = int.Parse(Session["LOBId"].ToString());
                res = advisorLOBBo.DeleteLOB(LOBId);
                if (res)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewLOB','none');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Sorry.. LOB is not deleted');", true);
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

                FunctionInfo.Add("Method", "EditEditLOB.ascx.cs:btnDelete_Click()");

                object[] objects = new object[2];
                objects[0] = LOBId;
                objects[1] = res;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewLOB','none');", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewLOB', 'none')", true);
        }

        protected void hiddenDelete_Click(object sender, EventArgs e)
        {
            //string val = Convert.ToString(hdnMsgValue.Value);
            //if (val == "1")
            //{
            //    DeleteLOB();
            //}
           
        }
        
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            EnableControls(this);
            lnkEdit.Visible = false;
            Session["LOBGridAction"] = "LOBEdit";
            trMandatory.Visible = true;
            lblView.Visible = false;
            lblEdit.Visible = true;
        }

    }
}