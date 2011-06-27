using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewMFTransaction : System.Web.UI.UserControl
    {
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        MFTransactionVo mfTransactionVo;
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                mfTransactionVo = (MFTransactionVo)Session["MFTransactionVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                userVo = (UserVo)Session["userVo"];
                LoadViewFields();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewMFTransaction.ascx:Page_Load()");
                object[] objects = new object[3];
                objects[0] = mfTransactionVo;
                objects[1] = customerVo;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void LoadViewFields()
        {
            try
            {
                if (Session["MFEditValue"].ToString() =="Edit")
                {
                    SetFields(1);
                }
                else
                {
                    SetFields(0);
                }
                

                if (mfTransactionVo.IsSourceManual == 1)
                {
                    lnkEdit.Visible = true;
                }
                else
                {
                    lnkEdit.Visible = false;
                }
                
                lblScheme.Text = mfTransactionVo.SchemePlan.ToString();
                lblTransactionType.Text = mfTransactionVo.TransactionType.ToString();
                lblFolioNumber.Text = mfTransactionVo.Folio.ToString();
                txtTransactionDate.Text = mfTransactionVo.TransactionDate.ToShortDateString().ToString();
                if (mfTransactionVo.TransactionClassificationCode == "DVR" || mfTransactionVo.TransactionClassificationCode == "DVP")
                {
                    trDividendRate.Visible = true;                    
                    txtDividentRate.Text = mfTransactionVo.DividendRate.ToString();
                }
                else
                {
                    dvDividentRate.Visible = false;
                    trDividendRate.Visible = false;
                }
                txtAmount.Text = mfTransactionVo.Amount.ToString();
                txtNAV.Text = mfTransactionVo.NAV.ToString();
                txtPrice.Text = mfTransactionVo.Price.ToString();
                if (mfTransactionVo.BuySell == "S")
                {
                    trSTT.Visible = true;
                    txtSTT.Text = mfTransactionVo.STT.ToString();
                }
                else
                {
                    dvSTT.Visible = false;
                    trSTT.Visible = false;
                }
                txtUnits.Text = mfTransactionVo.Units.ToString("f4");
                lblTransactionStatusValue.Text = mfTransactionVo.TransactionStatus.ToString();
                if (mfTransactionVo.TransactionStatusCode == 1)
                {
                    btnCancel.Visible = true;

                }
                else
                {
                    btnCancel.Visible = false;
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
                FunctionInfo.Add("Method", "ViewMFTransaction.ascx:LoadViewFields()");
                object[] objects = new object[1];
                objects[0] = mfTransactionVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //int day = int.Parse(ddlTransactionDateDay.SelectedValue.ToString());
                //int month = int.Parse(ddlTransactionDateMonth.SelectedValue.ToString());
                //int year = int.Parse(ddlTransactionDateYear.SelectedValue.ToString());
                //DateTime tradeDate = new DateTime(year, month, day);

                mfTransactionVo.TransactionDate = DateTime.Parse(txtTransactionDate.Text);
                if(txtDividentRate.Text!="")
                    mfTransactionVo.DividendRate = float.Parse(txtDividentRate.Text.ToString());
                mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
                mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());
                if (txtSTT.Text != "")
                    mfTransactionVo.STT = float.Parse(txtSTT.Text.ToString());

                bool bResult = customerTransactionBo.UpdateMFTransaction(mfTransactionVo, userVo.UserId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TransactionsView','none');", true);
                Session["MFEditValue"] = "View";
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewMFTransaction.ascx:btnSubmit_Click()");
                object[] objects = new object[1];
                objects[0] = mfTransactionVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void SetFields(int i)
        {
            if (i == 0)
            {
                lblScheme.Enabled = false;
                lblFolioNumber.Enabled = false;
                lblTransactionType.Enabled = false;
                //ddlTransactionDateDay.Visible = false;
                //ddlTransactionDateMonth.Visible = false;
                //ddlTransactionDateYear.Visible = false;
                txtTransactionDate.Enabled = false;
                txtDividentRate.Enabled = false;
                txtAmount.Enabled = false;
                txtNAV.Enabled = false;
                txtPrice.Enabled = false;
                txtSTT.Enabled = false;
                txtUnits.Enabled = false;
                btnSubmit.Visible = false;

                // Hide the Validation Divs
                dvSTT.Visible = false;
                dvUnits.Visible = false;
                dvAmount.Visible = false;
                dvPrice.Visible = false;
                dvNAV.Visible = false;
                dvDividentRate.Visible = false;
                dvTransactionDate.Visible = false;

            }
            else
            {
                lblScheme.Enabled = true;
                lblFolioNumber.Enabled = true;
                lblTransactionType.Enabled = true;
                txtTransactionDate.Enabled = true;
                txtDividentRate.Enabled = true;
                txtAmount.Enabled = true;
                txtNAV.Enabled = true;
                txtPrice.Enabled = true;
                txtSTT.Enabled = true;
                txtUnits.Enabled = true;
                btnSubmit.Visible = true;

                // Un-Hide the Validation Divs
                dvSTT.Visible = true;
                dvUnits.Visible = true;
                dvAmount.Visible = true;
                dvPrice.Visible = true;
                dvNAV.Visible = true;
                dvDividentRate.Visible = true;
                dvTransactionDate.Visible = true;
            }
        }
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            bool istrue = RegularExpressionValidator4.IsValid;
            Regex regex = new Regex("^\\d*(\\.(\\d{0,4}))?$");
            if(regex.IsMatch(txtAmount.Text))
                txtUnits.Text = (Math.Round((double.Parse(txtAmount.Text) / double.Parse(txtPrice.Text)),4)).ToString();
        }

        protected void txtUnits_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex("^\\d*(\\.(\\d{0,4}))?$");
            if (regex.IsMatch(txtAmount.Text))
                txtAmount.Text = Math.Round((double.Parse(txtPrice.Text) * double.Parse(txtUnits.Text)),4).ToString();
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Session["MFEditValue"] = "View";
                SetFields(1);
                lblScheme.Text = mfTransactionVo.SchemePlan.ToString();
                lblTransactionType.Text = mfTransactionVo.TransactionType.ToString();
                lblFolioNumber.Text = mfTransactionVo.Folio.ToString();
                txtTransactionDate.Text = mfTransactionVo.TransactionDate.ToShortDateString().ToString();
                txtDividentRate.Text = mfTransactionVo.DividendRate.ToString();
                txtAmount.Text = mfTransactionVo.Amount.ToString();
                txtNAV.Text = mfTransactionVo.NAV.ToString();
                txtPrice.Text = mfTransactionVo.Price.ToString();
                txtSTT.Text = mfTransactionVo.STT.ToString();
                txtUnits.Text = mfTransactionVo.Units.ToString("f4");
                Session["MFEditValue"] = "Edit";

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewMFTransaction.ascx:lnkEdit_Click()");
                object[] objects = new object[1];
                objects[0] = mfTransactionVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('TransactionsView','none');", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            int userId = ((UserVo)Session["userVo"]).UserId;
            try
            {
                if(mfTransactionVo!=null)
                    customerTransactionBo.CancelMFTransaction(mfTransactionVo, userId);
                btnCancel.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TransactionsView','none');", true);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewMFTransaction.ascx:btnCancel_Click(object sender, EventArgs e)");
                object[] objects = new object[1];
                objects[0] = mfTransactionVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

    }
}