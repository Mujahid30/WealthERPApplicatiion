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
using WealthERP.Base;
using BoCommon;
using System.Data;

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewMFTransaction : System.Web.UI.UserControl
    {
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        MFTransactionVo mfTransactionVo;
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo = new AdvisorVo();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                mfTransactionVo = (MFTransactionVo)Session["MFTransactionVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                advisorVo = (AdvisorVo)Session["advisorVo"];
                userVo = (UserVo)Session["userVo"];
                if (!Page.IsPostBack)
                {
                    Session["MFEditValue"] = "View";
                    LoadViewFields();
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

            if (userVo.UserType == "Advisor") lnkEdit.Visible = false;
            //if (userVo.UserType == "Advisor") btnCancel.Visible = true;
            if (userVo.UserType == "Associates")
            {
                lnkEdit.Visible = false;
                btnCancel.Visible = false;
            }
        }

        private void LoadViewFields()
        {
            bool isMainPortfolio = false;
            DataSet dsPortfolioType = new DataSet();
            dsPortfolioType = customerTransactionBo.GetPortfolioType(mfTransactionVo.Folio);

            try
            {
                if (dsPortfolioType.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt16(dsPortfolioType.Tables[0].Rows[0][0].ToString()) == 1)
                    {
                        isMainPortfolio = true;
                    }

                }

                if (Session["MFEditValue"].ToString() == "Edit")
                {
                    SetFields(1);
                }
                else
                {
                    SetFields(0);
                }

                //if (mfTransactionVo.IsSourceManual == 1)
                //{
                //    lnkEdit.Visible = true;
                //}
                //else
                //{
                //    lnkEdit.Visible = false;
                //}

                lblAMCName.Text = mfTransactionVo.AMCName;
                lblcategoryName.Text = mfTransactionVo.Category;
                lblScheme.Text = mfTransactionVo.SchemePlan.ToString();
                lblTransactionType.Text = mfTransactionVo.TransactionType.ToString();
                lblTransactionNoV.Text = mfTransactionVo.userTransactionNo.ToString();
                lblFolioNumber.Text = mfTransactionVo.Folio.ToString();
                txtTransactionDate.Text = mfTransactionVo.TransactionDate.ToShortDateString().ToString();
                txtSubBrokerCode.Text = mfTransactionVo.AgentCode.ToString();
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
                txtAmount.Text = String.Format("{0:n2}", decimal.Parse(mfTransactionVo.Amount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                //txtAmount.Text = mfTransactionVo.Amount.ToString();
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

                ShowHideCommandButton(isMainPortfolio, mfTransactionVo.IsSourceManual == 1 ? true : false, Session[SessionContents.CurrentUserRole].ToString() == "Customer" ? true : false, mfTransactionVo.TransactionStatusCode == 1 ? true : false);


                if (mfTransactionVo.TransactionStatusCode == 2 || mfTransactionVo.TransactionStatusCode==3)
                {
                    btnCancel.Visible = false;
                }
                else
                {
                    btnCancel.Visible = true;
                }

                //if (Session[SessionContents.CurrentUserRole].ToString() == "Customer")
                //{
                //    btnCancel.Visible = false;
                //    lnkEdit.Visible = false;
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
                FunctionInfo.Add("Method", "ViewMFTransaction.ascx:LoadViewFields()");
                object[] objects = new object[1];
                objects[0] = mfTransactionVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ShowHideCommandButton(bool isMainportfolio, bool isManulSource, bool isCustomerLogin, bool isOkTransaction)
        {
            if (isCustomerLogin == true)
            {
                lnkEdit.Visible = false;
                //btnDelete.Visible = false;
                btnCancel.Visible = false;
             }
            else if (isMainportfolio == true)
            {
                //if (isManulSource == true)
                //{
                    lnkEdit.Visible = true;
                    btnCancel.Visible = false;
                    //btnDelete.Visible = true;
                    //btnDelete.Enabled = false;
                //}
                //else
                //{
                //    lnkEdit.Visible = true;
                //    //btnDelete.Visible = false;
                //    btnCancel.Visible = false;
                //    btnCancel.Enabled = false;
                //}

            }
            else
            {
                lnkEdit.Visible = true;
                //btnDelete.Visible = true;
                //btnDelete.Enabled = false;
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
                if (txtDividentRate.Text != "")
                    mfTransactionVo.DividendRate = float.Parse(txtDividentRate.Text.ToString());
                mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
                mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());
                mfTransactionVo.AgentCode = txtSubBrokerCode.Text.ToString();
                if (txtSTT.Text != "")
                    mfTransactionVo.STT = float.Parse(txtSTT.Text.ToString());

                bool bResult = customerTransactionBo.UpdateMFTransaction(mfTransactionVo, userVo.UserId);
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','none');", true);
                btnSubmit.Visible = false;
                SetFields(0);
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
                lblAMC.Enabled = false;
                lblcategoryName.Enabled = false;
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
                txtSubBrokerCode.Enabled = false;
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

                lblAMC.Enabled = true;
                lblcategoryName.Enabled = true;
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
                txtSubBrokerCode.Enabled = true;
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
            if (regex.IsMatch(txtAmount.Text))
                txtUnits.Text = (Math.Round((double.Parse(txtAmount.Text) / double.Parse(txtPrice.Text)), 4)).ToString();
        }

        protected void txtUnits_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex("^\\d*(\\.(\\d{0,4}))?$");
            if (regex.IsMatch(txtAmount.Text))
                txtAmount.Text = Math.Round((double.Parse(txtPrice.Text) * double.Parse(txtUnits.Text)), 4).ToString();
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Session["MFEditValue"] = "View";
                btnDelete.Visible = true;               
                DataSet dsPortfolioType = new DataSet();
               dsPortfolioType = customerTransactionBo.GetPortfolioType(mfTransactionVo.Folio);

         
                if (dsPortfolioType.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt16(dsPortfolioType.Tables[0].Rows[0][0].ToString()) == 1)
                    {
                        SetFields(0);
                    }
                    else
                    {
                        SetFields(1);
                    }

                }

             
                lblAMCName.Text = mfTransactionVo.AMCName;
                lblcategoryName.Text = mfTransactionVo.Category;
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
                lnkEdit.Visible = false;
                btnSubmit.Visible = true;
                    btnDelete.Enabled = true;
                    txtSubBrokerCode.Enabled = true;
                    txtTransactionDate.Enabled = true;
                    txtAmount.Enabled = true;
                    txtPrice.Enabled = true;
                    txtUnits.Enabled = true;
                //else if (btnCancel.Visible == true)
                //{
                //    btnCancel.Enabled = true;
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
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','none');", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            int userId = ((UserVo)Session["userVo"]).UserId;
            try
            {
                if (mfTransactionVo != null)
                    customerTransactionBo.CancelMFTransaction(mfTransactionVo, userId);
                btnCancel.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','none');", true);

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



        protected void btnDelete_Click(object sender, EventArgs e)
        {

            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
           
            try
            {

                if (mfTransactionVo != null)
                {
                    if (mfTransactionVo.TransactionStatusCode == 1)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "confirm('Are you sure you want to delete transaction?');", true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage('1');", true);


                    }
                    else if (mfTransactionVo.TransactionStatusCode == 2)
                    {
                        if (!string.IsNullOrEmpty(mfTransactionVo.OriginalTransactionNumber))
                        {
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "confirm('Origional transaction not found.Please make sure if origional already deleted.Want to delete Cancel alone?');", true);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage('2');", true);
                        }
                        else
                        {
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "confirm('Origional transaction will autometically deleted on deleting cancel.Want to delete?');", true);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage('1');", true);
                        }
                    }
                    else if (mfTransactionVo.TransactionStatusCode == 3)
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage('2');", true);
                        if (!string.IsNullOrEmpty(mfTransactionVo.OriginalTransactionNumber))
                        {
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "confirm('Origional transaction not found.Please make sure if origional already deleted.Want to delete Cancel alone?');", true);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage('2');", true);
                        }
                        else
                        {
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "confirm('Origional transaction will autometically deleted on deleting cancel.Want to delete?');", true);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage('1');", true);
                        }
                    }
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
                FunctionInfo.Add("Method", "ViewMFTransaction.ascx:btnDelete_Click(object sender, EventArgs e)");
                object[] objects = new object[1];
                objects[0] = mfTransactionVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void hdnDeleteTrnx_Click(object sender, EventArgs e)
        {
            int adviserId = 0;
            int userId = ((UserVo)Session["userVo"]).UserId;
            if (advisorVo != null)
                adviserId = advisorVo.advisorId;
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                customerTransactionBo.DeleteMFTransaction(mfTransactionVo, adviserId, userId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','none');", true);
            }
        }

    }
}