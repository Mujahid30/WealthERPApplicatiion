using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCalculator;
using AjaxControlToolkit;
using WealthERP.Base;
using BoValuation;
using System.Text;

namespace WealthERP.General
{
    public partial class InterestCalculator : System.Web.UI.UserControl
    {
        ArrayList alInputFields = new ArrayList();
        private int nInstTypeIndx = 1;
        ArrayList alOutpuType = new ArrayList();
        Hashtable hResult = new Hashtable();

        List<KeyValuePair<string, string>> alInput = new List<KeyValuePair<string, string>>();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            if (!Page.IsPostBack)
            {
                trError.Style.Add("display", "none");
                LoadInstrumentType();
            }

            CreateInputFields();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "IC", "displayCalendar()", true);
            lblResult.Text = "&nbsp;";
        }

        private void LoadInstrumentType()
        {
            ddlInstType.Items.Clear();
            try
            {
                ListItem lItem;
                DataSet dsInstrumentType = (DataSet)Cache["InstrumentType"];
                if (dsInstrumentType == null)
                    dsInstrumentType = Caching.GetDataFromDB(Contants.cStr_InstrumentTypeMaster, "InstrumentType");
                if (dsInstrumentType != null)
                {
                    foreach (DataRow dr in dsInstrumentType.Tables[0].Rows)
                    {
                        lItem = new ListItem();
                        lItem.Text = dr[Contants.cStr_InstrumentType].ToString();
                        lItem.Value = dr[Contants.cStr_Id].ToString();
                        ddlInstType.Items.Add(lItem);
                    }
                }
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "FinanceCalculator.cs:LoadInstrumentType()");

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);

                throw exBase;
            }
            ddlInstType.Items.Insert(0, new ListItem(Contants.cStr_InstrumentType_DefaultMsg, Contants.cStr_InstrumentType_DefaultMsg_Value));
        }

        protected void ddlInstType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblResult.Text = "&nbsp;";
                lblResultValue.Text = string.Empty;
                trError.Style.Add("display", "none");

                if (tblInput.Visible)
                {
                    alInput.Clear();
                    alInputFields.Clear();
                    tblInput.Style.Add("display", "none");
                    ClearSelection();
                    Control cntrl = this.FindControl(Contants.cStr_TblInputField);
                    if (pnlInputFields.Controls.Contains(cntrl))
                    {
                        pnlInputFields.Controls.Remove(cntrl);
                        cntrl.Dispose();
                    }
                    CreateInputFields();
                }
                tblInput.Style.Add("display", "display");
                tblInput.Visible = true;

                string sSelctedInstTypeId = ddlInstType.SelectedValue;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "FinanceCalculator.cs:ddlInstType_SelectedIndexChanged()");

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        //protected void btnCalculate_Click(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        trError.Style.Add("display","none");
        //        string sInstrumentType = ddlInstType.SelectedItem.Text;
        //        string sErrMsg = null;
        //        double sResult = 0;
        //        ArrayList alErrMsg = new ArrayList();
        //        sResult = CalculateResult(ref sResult, ref sErrMsg, ref alErrMsg, ref hResult);

        //        if (hResult.Count > 0)
        //        {
        //            StringBuilder sb = new StringBuilder();
        //            foreach(DictionaryEntry dEntry in hResult)
        //            {
        //                string sValue = dEntry.Value.ToString();
        //                if (sValue == "0")
        //                    continue;
        //                else
        //                {
        //                    sb.Append(dEntry.Key + " of " + ddlInstType.SelectedItem.Text + " : ");
        //                    sb.Append("<b> " + dEntry.Value.ToString() + "</b>" );
        //                    sb.Append("<br />");
        //                }
        //            }
        //            lblResult.Text = sb.ToString();
        //            pnlInputFields.Visible = true;
        //            hResult.Clear();
        //        }
        //        else
        //        {
        //            string sResultText = null;
        //            if (alErrMsg.Count > 0)
        //            {
        //                foreach(string msg in alErrMsg)
        //                {
        //                    sResultText += msg.ToString()+ " <br />";
        //                }
        //                trError.Style.Add("display","display");
        //            }
        //            lblMessage.Text = sResultText;
        //            lblResult.Text = string.Empty;
        //            lblResultValue.Text = string.Empty;
        //            alErrMsg.Clear();
        //        }
        //        pnlInputFields.Visible = true;
        //    }
        //    catch(BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch(Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "FinanceCalculator:btnCalculate_Click()");
        //        FunctionInfo = exBase.AddObject(FunctionInfo, null);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);

        //        throw exBase;
        //    }
        //}

        protected void btnCalculate_Click(object sender, System.EventArgs e)
        {
            try
            {
                trError.Style.Add("display", "none");
                string sInstrumentType = ddlInstType.SelectedItem.Text;
                string sErrMsg = null;
                double sResult = 0;
                ArrayList alErrMsg = new ArrayList();
                sResult = CalculateResult(ref sResult, ref sErrMsg, ref alErrMsg, ref hResult);

                if (hResult.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table>");
                    foreach (DictionaryEntry dEntry in hResult)
                    {
                        sb.Append("<tr>");
                        string sValue = dEntry.Value.ToString();
                        if (sValue == "0")
                            continue;
                        else
                        {
                            sb.Append("<td>");
                            //sb.Append(dEntry.Key + " of " + ddlInstType.SelectedItem.Text + " : ");
                            //sb.Append("<b> " + dEntry.Value.ToString() + "</b>" );
                            //sb.Append("<br />");
                            sb.Append(dEntry.Key + " of " + ddlInstType.SelectedItem.Text);
                            sb.Append("</td>");
                            sb.Append("<td align='right'>");
                            sb.Append("<b> " + dEntry.Value.ToString() + "</b>");
                            sb.Append("</td>");
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    lblResult.Text = sb.ToString();
                    pnlInputFields.Visible = true;
                    hResult.Clear();
                }
                else
                {
                    string sResultText = null;
                    if (alErrMsg.Count > 0)
                    {
                        foreach (string msg in alErrMsg)
                        {
                            sResultText += msg.ToString() + " <br />";
                        }
                        trError.Style.Add("display", "display");
                    }
                    lblMessage.Text = sResultText;
                    lblResult.Text = string.Empty;
                    lblResultValue.Text = string.Empty;
                    alErrMsg.Clear();
                }
                pnlInputFields.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "FinanceCalculator:btnCalculate_Click()");
                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);

                throw exBase;
            }
        }

        protected void btnClear_Click(object sender, System.EventArgs e)
        {
            ClearSelection();
            lblResultValue.Text = string.Empty;
        }

        // Clear input values
        private void ClearSelection()
        {
            try
            {
                Control cntrl = this.FindControl("tblInputField");
                foreach (string arr in alInputFields)
                {
                    string sArr = arr.Replace("txt", "");
                    if (cntrl.FindControl(arr) != null)
                    {
                        if (cntrl.FindControl(arr).GetType().Name == "TextBox")
                        {
                            TextBox txt = (TextBox)cntrl.FindControl(arr);
                            txt.Text = string.Empty;
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

                FunctionInfo.Add("Method", "FinanceCalculator:ClearSelection()");
                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;

                throw exBase;
            }
        }

        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlICB = (DropDownList)this.FindControl("txtICB");
            Label lblICOM = (Label)this.FindControl("lblICOM");
            DropDownList ddlICOM = (DropDownList)this.FindControl("txtICOM");
            if (ddlICB.SelectedValue.Equals("Compound"))
            {
                ddlICOM.Visible = true;
                lblICOM.Visible = true;
            }
            else
            {
                ddlICOM.Visible = false;
                ddlICOM.SelectedValue = "Monthly";
                lblICOM.Visible = false;
                lblResultValue.Text = string.Empty;
            }

        }

        // Creating input fields according to the instrument type selection
        private void CreateInputFields()
        {

            Table tbl = new Table();
            tbl.EnableViewState = false;
            tbl.ID = "tblInputField";
            tbl.Attributes.Add("align", "center");

            tbl.CellPadding = 0;
            tbl.CellPadding = 0;
            try
            {
                pnlInputFields.Controls.Add(tbl);
                TableRow tr;
                TableCell tc;
                DataSet dsInputFields = (DataSet)Cache["InputFields"];
                if (dsInputFields == null)
                    dsInputFields = Caching.GetDataFromDB(Contants.cStr_InputMaster, "InputFields");
                int nCount = dsInputFields.Tables[0].Rows.Count;
                DataTable dtInputFields = dsInputFields.Tables[0];

                DataSet dsInterestFreq = (DataSet)Cache["InterestFrequency"];
                if (dsInterestFreq == null)
                    dsInterestFreq = Caching.GetDataFromDB(Contants.cStr_InterestFrequency, "InterestFrequency");

                if (ddlInstType.SelectedIndex > 0)
                    nInstTypeIndx = Convert.ToInt32(ddlInstType.SelectedValue);

                var inputFields = (from input in dtInputFields.AsEnumerable()
                                   where input.Field<Int32>(Contants.cStr_IOM_InstrumentTypeId) == nInstTypeIndx
                                   select input).Distinct();
                foreach (var input in inputFields)
                {
                    string sInputType = input[Contants.cStr_IM_InputType].ToString();
                    bool IsInputPresent = false;
                    if (alInput.Count > 0)
                        foreach (KeyValuePair<string, string> inputType in alInput)
                        {
                            if (inputType.Key.Contains(sInputType))
                            {
                                IsInputPresent = true;
                                break;
                            }
                        }
                    if (!IsInputPresent)
                        alInput.Add(new KeyValuePair<string, string>(sInputType, input[Contants.cStr_IM_Abbrevation].ToString()));
                    else
                        continue;
                }

                foreach (var input in inputFields)
                {
                    if (Convert.ToBoolean(input[Contants.cStr_IOM_InputFlag]))
                    {
                        Label lbl = new Label();

                        lbl.Text = "<span style='color:red;font-weight:bold;'>*&nbsp;</span>" + input[Contants.cStr_IM_InputType].ToString();
                        lbl.ID = "lbl" + input[Contants.cStr_IM_Abbrevation].ToString();

                        tc = new TableCell();
                        tc.CssClass = "labelText";
                        tr = new TableRow();
                        tc.Controls.Add(lbl);
                        tr.Controls.Add(tc);

                        tc = new TableCell();
                        tc.CssClass = "labelText";
                        if (input[Contants.cStr_IOM_FieldType].Equals("Text") || input[Contants.cStr_IOM_FieldType].Equals("Date"))
                        {
                            if (input[Contants.cStr_IOM_FieldType].Equals("Text"))
                            {
                                string sAbb = input[Contants.cStr_IM_Abbrevation].ToString();
                                TextBox txt = new TextBox();
                                txt.ID = "txt" + sAbb;
                                txt.CssClass = "labelText";
                                if (!alInputFields.Contains(txt.ID))
                                {
                                    alInputFields.Add(txt.ID);
                                    tc.Controls.Add(txt);

                                    if (sAbb == "DAMT" || sAbb == "DDAT" || sAbb == "IRA" || sAbb == "ADAMT" || sAbb == "AMTLFY"
                                        || sAbb == "ECFCY" || sAbb == "ERCFY" || sAbb == "YCFCFY" || sAbb == "LDS" || sAbb == "CYOS"
                                        || sAbb == "NOMC")
                                    {
                                        RequiredFieldValidator rfv = new RequiredFieldValidator();
                                        rfv.ID = "rfv" + sAbb;
                                        rfv.ControlToValidate = txt.ID;
                                        rfv.ErrorMessage = input[Contants.cStr_IM_InputType].ToString() + " " + "value canot be empty";
                                        rfv.Text = "*";
                                        rfv.SetFocusOnError = true;
                                        tc.Controls.Add(rfv);
                                    }
                                    tr.Controls.Add(tc);
                                    tbl.Controls.Add(tr);
                                }
                            }
                            if (input[Contants.cStr_IM_Abbrevation].ToString().Equals("IRA", StringComparison.CurrentCultureIgnoreCase))
                            {
                                Label lblPercentage = new Label();
                                lblPercentage.Text = "%";
                                lblPercentage.CssClass = "labelText";
                                lblPercentage.Style.Add("width", "25px");
                                lblPercentage.Style.Add("height", "20px");
                                lblPercentage.Style.Add("position", "absolute");
                                tc.Controls.Add(lblPercentage);
                            }
                            if (input[Contants.cStr_IOM_FieldType].Equals("Date"))
                            {
                                string sAbb = input[Contants.cStr_IM_Abbrevation].ToString();
                                TextBox txt = new TextBox();
                                txt.ID = "txt" + sAbb;
                                txt.CssClass = "labelText";
                                if (!alInputFields.Contains(txt.ID))
                                {
                                    alInputFields.Add(txt.ID);

                                    CalendarExtender ajaxCalender = new CalendarExtender();
                                    ajaxCalender.ID = "ajaxCalender" + sAbb;
                                    ajaxCalender.Format = "dd/MM/yyyy";
                                    ajaxCalender.TargetControlID = txt.ID;

                                    TextBoxWatermarkExtender ajaxTxtWaterMark = new TextBoxWatermarkExtender();
                                    ajaxTxtWaterMark.ID = "ajaxWatermark" + sAbb;
                                    ajaxTxtWaterMark.TargetControlID = txt.ID;
                                    ajaxTxtWaterMark.WatermarkText = "DD/MM/YYYY";

                                    tc.Controls.Add(txt);
                                    tc.Controls.Add(ajaxCalender);
                                    tc.Controls.Add(ajaxTxtWaterMark);

                                    if (sAbb == "DDAT")
                                    {
                                        RequiredFieldValidator rfv = new RequiredFieldValidator();
                                        rfv.ID = "rfv" + sAbb;
                                        rfv.ControlToValidate = txt.ID;
                                        rfv.ErrorMessage = input[Contants.cStr_IM_InputType].ToString() + " " + "value canot be empty";
                                        rfv.Text = "*";
                                        rfv.SetFocusOnError = true;
                                        tc.Controls.Add(rfv);
                                    }
                                    tr.Controls.Add(tc);
                                    tbl.Controls.Add(tr);
                                }
                            }
                        }
                        else if (input[Contants.cStr_IOM_FieldType].Equals("DDL"))
                        {

                            DropDownList ddl = new DropDownList();
                            ddl.CssClass = "labelText";
                            ddl.ID = "txt" + input[Contants.cStr_IM_Abbrevation].ToString();
                            ddl.Attributes.Add("name", "txt" + input[Contants.cStr_IM_Abbrevation].ToString());

                            //if(ddlInstType.SelectedItem.Text.ToLower() == "annuity" && ddlInstOutputType.SelectedItem.Text.ToLower() == "withdrawl amount")
                            if (ddlInstType.SelectedItem.Text.ToLower() == "annuity")
                            {
                                ListItem liAnuity = new ListItem("Monthly", "12");
                                ddl.Items.Add(liAnuity);
                                liAnuity = new ListItem("Quartely", "3");
                                ddl.Items.Add(liAnuity);
                                liAnuity = new ListItem("Half Yearly", "6");
                                ddl.Items.Add(liAnuity);
                                liAnuity = new ListItem("Annually", "1");
                                ddl.Items.Add(liAnuity);
                            }
                            else if ((ddlInstType.SelectedItem.Text.ToLower() == "fixed deposits" || ddlInstType.SelectedItem.Text.ToLower() == "company fd"
                                || ddlInstType.SelectedItem.Text.ToLower() == "goi relief bonds" || ddlInstType.SelectedItem.Text.ToLower() == "goi tax saving bonds"
                                || ddlInstType.SelectedItem.Text.ToLower() == "tax saving bonds") && input[Contants.cStr_IM_Abbrevation].ToString() == "ICOM")
                            {
                                ddl.Items.Clear();
                                try
                                {
                                    ListItem lItem;

                                    if (dsInterestFreq != null)
                                    {
                                        foreach (DataRow dr in dsInterestFreq.Tables[0].Rows)
                                        {
                                            lItem = new ListItem();
                                            lItem.Text = dr[Contants.cStr_IF_InterestFreqauency].ToString();
                                            lItem.Value = dr["IF_RowId"].ToString();
                                            ddl.Items.Add(lItem);
                                        }
                                    }
                                }
                                catch (System.Data.SqlClient.SqlException sqlEx)
                                {
                                    throw sqlEx;
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            else
                            {
                                ddl.AutoPostBack = true;
                                ListItem li = new ListItem("Compound", "Compound");
                                ddl.Items.Add(li);
                                li = new ListItem("Simple", "Simple");
                                ddl.Items.Add(li);

                                ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                            }
                            if (!alInputFields.Contains(ddl.ID))
                            {
                                alInputFields.Add(ddl.ID);
                                tc.CssClass = "dropdownList";
                                tc.Controls.Add(ddl);
                                tr.Controls.Add(tc);
                                tbl.Controls.Add(tr);
                            }
                        }

                        pnlInputFields.Controls.Add(tbl);
                    }
                    else
                    {
                        string sAbbrevation = input[Contants.cStr_IM_Abbrevation].ToString();
                        if (!alInputFields.Contains(sAbbrevation))
                            alInputFields.Add(sAbbrevation);
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

                FunctionInfo.Add("Method", "FinanceCalculator.cs:CreateInputFields()");

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        // calculate result
        private double CalculateResult(ref double sResult, ref string sErrMsg, ref ArrayList alErroMsg, ref Hashtable result)
        {
            Control cntrl = this.FindControl("tblInputField");

            Hashtable hInputValues = new Hashtable();
            try
            {
                // Add input values into the hashtable.
                foreach (string arr in alInputFields)
                {
                    string sArr = arr.Replace("txt", "");
                    if (cntrl.FindControl(arr) != null)
                    {
                        if (cntrl.FindControl(arr).GetType().Name == "TextBox")
                        {
                            TextBox txt = (TextBox)cntrl.FindControl(arr);
                            string sValue = txt.Text;
                            hInputValues.Add(sArr, sValue);
                        }
                        if (cntrl.FindControl(arr).GetType().Name == "DropDownList")
                        {
                            DropDownList ddl = (DropDownList)cntrl.FindControl(arr);
                            string sValue = ddl.SelectedValue;
                            hInputValues.Add(sArr, sValue);
                        }
                    }
                }

                // Add the calculated fields into the hastable with values empty.
                foreach (string arr in alInputFields)
                {
                    string sArr = arr.Replace("txt", "");
                    if (hInputValues[sArr] == null)
                    {
                        hInputValues.Add(sArr, "");
                    }
                }

                DataSet dsOutputTypeForSIT = null;
                dsOutputTypeForSIT = BoValuation.CalculatorBo.dsOutputType;
                dsOutputTypeForSIT = (DataSet)Cache["OutputType"];
                if (dsOutputTypeForSIT == null)
                    dsOutputTypeForSIT = Caching.GetDataFromDB(Contants.cStr_OutputMaster, "OutputType");

                DataTable dtOutputFields = dsOutputTypeForSIT.Tables[0];

                if (ddlInstType.SelectedIndex > 0)
                    nInstTypeIndx = Convert.ToInt32(ddlInstType.SelectedValue);
                Hashtable hOutputFields = new Hashtable();
                var outputFields = from input in dtOutputFields.AsEnumerable()
                                   where input.Field<Int32>(Contants.cStr_InstrumentTypeId) == nInstTypeIndx
                                   select input;
                foreach (var outputType in outputFields)
                {
                    if (!hOutputFields.Contains(outputType["OM_OutputId"]))
                        hOutputFields.Add(outputType["OM_OutputType"], outputType["OM_OutputId"]);
                }
                foreach (DictionaryEntry deOutput in hOutputFields)
                {
                    sResult = BoValuation.CalculatorBo.DoCalculate(Convert.ToInt32(ddlInstType.SelectedValue), Convert.ToInt32(deOutput.Value), hInputValues, ref sErrMsg, ref alErroMsg);
                    result.Add(deOutput.Key, sResult);
                }
                return sResult;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FinanceCalculator:CalculateResult()");

                object[] objects = new object[4];
                objects[0] = sResult;
                objects[1] = sErrMsg;
                objects[2] = alErroMsg;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

    }
}