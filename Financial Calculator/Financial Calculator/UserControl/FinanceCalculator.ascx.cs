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
using Utility;

public partial class UserControl_FinanceCalculator : System.Web.UI.UserControl
{
	ArrayList alInputFields = new ArrayList();
	private int nInstTypeIndx = 1;
	private int nInstOutputTypeIndx = 1;

	List<KeyValuePair<string, string>> alInput = new List<KeyValuePair<string, string>>();
	
	protected void Page_Load(object sender, EventArgs e)
	{	
		System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
		System.Threading.Thread.CurrentThread.CurrentCulture = culture;
		if (!Page.IsPostBack)
		{
			trError.Style.Add("display","none");
			LoadInstrumentType();
			LoadInstrumentOutput(null);
		}

		CreateInputFields();

		lblResult.Text = "&nbsp;";
	}

	private void LoadInstrumentType()
	{	ddlInstType.Items.Clear();
		try
		{	ListItem lItem;
			DataSet dsInstType = BoCalculator.CalculatorBo.dsInstType;
			if (dsInstType != null)
			{
				foreach(DataRow dr in dsInstType.Tables[0].Rows)
				{	lItem = new ListItem();
					lItem.Text = dr[Constants.cStr_InstrumentType].ToString();
					lItem.Value = dr[Constants.cStr_Id].ToString();
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
		ddlInstType.Items.Insert(0, new ListItem(Constants.cStr_InstrumentType_DefaultMsg, Constants.cStr_InstrumentType_DefaultMsg_Value));
	}

	// Load Output Type
	private void LoadInstrumentOutput(DataSet dsOutput)
	{	
		ddlInstOutputType.Items.Clear();
		try
		{	ListItem lItem;
			if (dsOutput != null)
			{	
				DataTable dtOutputType = dsOutput.Tables[0];
				var outputForSIT = from output in dtOutputType.AsEnumerable()
							where output.Field<Int32>(Constants.cStr_InstrumentTypeId) == Convert.ToInt32(ddlInstType.SelectedValue)
							select output;
				foreach(var input in outputForSIT)
				{	lItem = new ListItem();
					lItem.Text = input[Constants.cStr_OutputType].ToString();
					lItem.Value = input[Constants.cStr_OutputId].ToString();
					lItem.Attributes.Add("title", lItem.Text);
					ddlInstOutputType.Items.Add(lItem);
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

			FunctionInfo.Add("Method","FinanceCalculator.cs:LoadInstrumentOutput()");

			object[] objects = new object[1];
			objects[0] = dsOutput;

			FunctionInfo = exBase.AddObject(FunctionInfo,objects);
			exBase.AdditionalInformation = FunctionInfo;
			ExceptionManager.Publish(exBase);

			throw exBase;
		}
		ddlInstOutputType.Items.Insert(0, new ListItem("-Select Instrument OutputType-", "-1"));
	}

	protected void ddlInstOutputType_SelectedIndexChanged(object sender, EventArgs e)
	{
		trError.Style.Add("display","none");
		lblResult.Text = "&nbsp;";
		lblResultValue.Text = string.Empty;
		string sSelctedInstType = ddlInstType.SelectedItem.Text;
		try
		{
			if (tblInput.Visible)
			{
				tblInput.Style.Add("display", "none");
				ClearSelection();
				Control cntrl = this.FindControl(Constants.cStr_TblInputField);
				if (pnlInputFields.Controls.Contains(cntrl))
				{
					pnlInputFields.Controls.Remove(cntrl);
					cntrl.Dispose();
				}
				CreateInputFields();
			}
			tblInput.Style.Add("display", "display");
			tblInput.Visible = true;
		}
		catch(BaseApplicationException Ex)
		{
			throw Ex;
		}
		catch(Exception Ex)
		{
			BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
			NameValueCollection FunctionInfo = new NameValueCollection();

			FunctionInfo.Add("Method","FinanceCalculator.cs:ddlInstOutputType_SelectedIndexChanged()");
			FunctionInfo = exBase.AddObject(FunctionInfo, null);
			exBase.AdditionalInformation = FunctionInfo;
			ExceptionManager.Publish(exBase);
			throw exBase;
		}

	}

	protected void ddlInstType_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			lblResult.Text = "&nbsp;";
			lblResultValue.Text = string.Empty;
			trError.Style.Add("display","none");
			if (tblInput.Visible)
				tblInput.Style.Add("display", "none");
			
			string sSelctedInstTypeId = ddlInstType.SelectedValue;
			DataSet dsOutputTypeForSIT = null;
			dsOutputTypeForSIT = BoCalculator.CalculatorBo.dsOutputType;
			LoadInstrumentOutput(dsOutputTypeForSIT);
		}
		catch(BaseApplicationException Ex)
		{
			throw Ex;
		}
		catch(Exception Ex)
		{
			BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
			NameValueCollection FunctionInfo = new NameValueCollection();

			FunctionInfo.Add("Method","FinanceCalculator.cs:ddlInstType_SelectedIndexChanged()");

			FunctionInfo = exBase.AddObject(FunctionInfo,null);
			exBase.AdditionalInformation = FunctionInfo;
			ExceptionManager.Publish(exBase);
			throw exBase;
		}
	}

	protected void btnCalculate_Click(object sender, System.EventArgs e)
	{
		try
		{
			trError.Style.Add("display","none");
			string sInstrumentType = ddlInstType.SelectedItem.Text;
			string sErrMsg = null;
			double sResult = 0;
			ArrayList alErrMsg = new ArrayList();
			sResult = CalculateResult(ref sResult, ref sErrMsg, ref alErrMsg);

			if (sResult > 0)
			{
				lblResult.Text = ddlInstOutputType.SelectedItem.Text + " of " + ddlInstType.SelectedItem.Text + " : ";
				lblResultValue.Text = sResult.ToString();
				pnlInputFields.Visible = true;
			}
			else
			{
				string sResultText = null;
				if (alErrMsg.Count > 0)
				{
					foreach(string msg in alErrMsg)
					{
						sResultText += msg.ToString()+ " <br />";
					}
					trError.Style.Add("display","display");
				}
				lblMessage.Text = sResultText;
				lblResult.Text = string.Empty;
				lblResultValue.Text = string.Empty;
				alErrMsg.Clear();
			}
			pnlInputFields.Visible = true;
		}
		catch(BaseApplicationException Ex)
		{
			throw Ex;
		}
		catch(Exception Ex)
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
			foreach(string arr in alInputFields)
			{
				string sArr = arr.Replace("txt","");
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
		catch(BaseApplicationException Ex)
		{
			throw Ex;
		}
		catch(Exception Ex)
		{
			BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
			NameValueCollection FunctionInfo = new NameValueCollection();

			FunctionInfo.Add("Method","FinanceCalculator:ClearSelection()");
			FunctionInfo = exBase.AddObject(FunctionInfo,null);
			exBase.AdditionalInformation = FunctionInfo;

			throw exBase;
		}
	}

	protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
	{
		DropDownList ddlICB = (DropDownList) this.FindControl("txtICB");
		Label lblICOM = (Label)this.FindControl("lblICOM");
		DropDownList ddlICOM = (DropDownList) this.FindControl("txtICOM");
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
			DataSet dsInputFields = BoCalculator.CalculatorBo.dsInputForSelOutput;
			int nCount = dsInputFields.Tables[0].Rows.Count;
			DataTable dtInputFields = dsInputFields.Tables[0];

			DataSet dsInterestFreq = BoCalculator.CalculatorBo.dsInterestFreq;

			if (ddlInstType.SelectedIndex > 0)
				nInstTypeIndx = Convert.ToInt32(ddlInstType.SelectedValue);
			if (ddlInstOutputType.SelectedIndex > 0)
				nInstOutputTypeIndx = Convert.ToInt32(ddlInstOutputType.SelectedValue);

			var inputFields = from input in dtInputFields.AsEnumerable()
						where input.Field<Int32>(Constants.cStr_IOM_InstrumentTypeId) == nInstTypeIndx && 
						input.Field<Int32>(Constants.cStr_IOM_OutputTypeId) == nInstOutputTypeIndx
						select input;
			foreach(var input in inputFields)
			{
				alInput.Add(new KeyValuePair<string,string>(input[Constants.cStr_IM_InputType].ToString(), input[Constants.cStr_IM_Abbrevation].ToString()));
			}
			
				foreach(var input in inputFields)
				{
					if(Convert.ToBoolean(input[Constants.cStr_IOM_InputFlag]))
					{
						Label lbl = new Label();

						lbl.Text = "<span style='color:red;font-weight:bold;'>*&nbsp;</span>" + input[Constants.cStr_IM_InputType].ToString();
						lbl.ID = "lbl" + input[Constants.cStr_IM_Abbrevation].ToString();
						
						tc = new TableCell();
						tc.CssClass = "labelText";
						tr = new TableRow();
						tc.Controls.Add(lbl);
						tr.Controls.Add(tc);

						tc = new TableCell();
						tc.CssClass = "labelText";
						if(input[Constants.cStr_IOM_FieldType].Equals("Text") || input[Constants.cStr_IOM_FieldType].Equals("Date"))
						{
							if (input[Constants.cStr_IOM_FieldType].Equals("Text"))
							{
								string sAbb = input[Constants.cStr_IM_Abbrevation].ToString();
								TextBox txt = new TextBox();
								txt.ID = "txt" + sAbb;
								txt.CssClass = "labelText";
								alInputFields.Add(txt.ID);
								tc.Controls.Add(txt);
								RequiredFieldValidator rfv = new RequiredFieldValidator();
								rfv.ID = "rfv" + sAbb;
								rfv.ControlToValidate = txt.ID;
								rfv.ErrorMessage = input[Constants.cStr_IM_InputType].ToString() + " " +"value canot be empty";
								rfv.Text = "*";
								rfv.SetFocusOnError = true;
								tc.Controls.Add(rfv);
								tr.Controls.Add(tc);
								tbl.Controls.Add(tr);
							}
							if (input[Constants.cStr_IM_Abbrevation].ToString().Equals("IRA", StringComparison.CurrentCultureIgnoreCase))
							{
								Label lblPercentage = new Label();
								lblPercentage.Text = "%";
								lblPercentage.CssClass = "labelText";
								lblPercentage.Style.Add("width","25px");
								lblPercentage.Style.Add("height","20px");
								lblPercentage.Style.Add("position","absolute");
								tc.Controls.Add(lblPercentage);
							}
							if(input[Constants.cStr_IOM_FieldType].Equals("Date"))
							{
								string sAbb = input[Constants.cStr_IM_Abbrevation].ToString();
								TextBox txt = new TextBox();
								txt.ID = "txt" + sAbb;
								txt.CssClass = "labelText";
								alInputFields.Add(txt.ID);
								
								AjaxControlToolkit.CalendarExtender  ajaxCalender = new CalendarExtender();
								ajaxCalender.ID = "ajaxCalender" + sAbb;
								ajaxCalender.Format = "dd/MM/yyyy";
								ajaxCalender.TargetControlID = txt.ID;

								AjaxControlToolkit.TextBoxWatermarkExtender  ajaxTxtWaterMark = new TextBoxWatermarkExtender();
								ajaxTxtWaterMark.ID = "ajaxWatermark" + sAbb;
								ajaxTxtWaterMark.TargetControlID = txt.ID;
								ajaxTxtWaterMark.WatermarkText = "DD/MM/YYYY";

								tc.Controls.Add(txt);
								tc.Controls.Add(ajaxCalender);
								tc.Controls.Add(ajaxTxtWaterMark);

								RequiredFieldValidator rfv = new RequiredFieldValidator();
								rfv.ID = "rfv" + sAbb;
								rfv.ControlToValidate = txt.ID;
								rfv.ErrorMessage = input[Constants.cStr_IM_InputType].ToString() + " " +"value canot be empty";
								rfv.Text = "*";
								rfv.SetFocusOnError = true;
								tc.Controls.Add(rfv);
								tr.Controls.Add(tc);
								tbl.Controls.Add(tr);
							}
						}
						else if(input[Constants.cStr_IOM_FieldType].Equals("DDL"))
						{
							
							DropDownList ddl = new DropDownList();
							ddl.CssClass = "labelText";
							ddl.ID = "txt" + input[Constants.cStr_IM_Abbrevation].ToString();
							ddl.Attributes.Add("name", "txt" + input[Constants.cStr_IM_Abbrevation].ToString());

							if(ddlInstType.SelectedItem.Text.ToLower() == "annuity" && ddlInstOutputType.SelectedItem.Text.ToLower() == "withdrawl amount")
							{
								ListItem liAnuity = new ListItem("Monthly","12");
								ddl.Items.Add(liAnuity);
								liAnuity = new ListItem("Quartely","3");
								ddl.Items.Add(liAnuity);
								liAnuity = new ListItem("Half Yearly","6");
								ddl.Items.Add(liAnuity);
								liAnuity = new ListItem("Annually","1");
								ddl.Items.Add(liAnuity);
							}
							else if((ddlInstType.SelectedItem.Text.ToLower() == "fixed deposits" || ddlInstType.SelectedItem.Text.ToLower() == "company fd"
								|| ddlInstType.SelectedItem.Text.ToLower() == "goi relief bonds" || ddlInstType.SelectedItem.Text.ToLower() == "goi tax saving bonds"
								|| ddlInstType.SelectedItem.Text.ToLower() == "tax saving bonds") && input[Constants.cStr_IM_Abbrevation].ToString() == "ICOM")
							{
								ddl.Items.Clear();
								try
								{	ListItem lItem;
									
									if (dsInterestFreq != null)
									{
										foreach(DataRow dr in dsInterestFreq.Tables[0].Rows)
										{	lItem = new ListItem();
											lItem.Text = dr[Constants.cStr_IF_InterestFreqauency].ToString();
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
								
								//ddl.Attributes.Add("onchange", "document.txtICOM.visible=false;"); 
								ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
							}
							alInputFields.Add(ddl.ID);
							tc.CssClass = "dropdownList";
							tc.Controls.Add(ddl);
							tr.Controls.Add(tc);
							tbl.Controls.Add(tr);
						}
						
						pnlInputFields.Controls.Add(tbl);
					}
					else
					{
						alInputFields.Add(input[Constants.cStr_IM_Abbrevation].ToString());
					}
				}
			}
		
		catch(BaseApplicationException Ex)
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
	private double CalculateResult(ref double sResult, ref string sErrMsg, ref ArrayList alErroMsg)
	{
		Control cntrl = this.FindControl("tblInputField");
		
		Hashtable hInputValues = new Hashtable();
		try
		{
			// Add input values into the hashtable.
			foreach(string arr in alInputFields)
			{
				string sArr = arr.Replace("txt","");
				if (cntrl.FindControl(arr) != null)
				{
					if (cntrl.FindControl(arr).GetType().Name == "TextBox")
					{
						TextBox txt = (TextBox)cntrl.FindControl(arr);
						string sValue = txt.Text;
						hInputValues.Add(sArr,sValue);
					}
					if (cntrl.FindControl(arr).GetType().Name == "DropDownList")
					{
						DropDownList ddl = (DropDownList)cntrl.FindControl(arr);
						string sValue = ddl.SelectedValue;
						hInputValues.Add(sArr,sValue);
					}
				}
			}

			// Add the calculated fields into the hastable with values empty.
			foreach(string arr in alInputFields)
			{
				string sArr = arr.Replace("txt","");
				if(hInputValues[sArr] == null)
				{
					hInputValues.Add(sArr,"");
				}
			}

			sResult = BoCalculator.CalculatorBo.DoCalculate(Convert.ToInt32(ddlInstType.SelectedValue),Convert.ToInt32(ddlInstOutputType.SelectedValue), hInputValues, ref sErrMsg, ref alErroMsg);
			return sResult;
		}
		catch(BaseApplicationException Ex)
		{
			throw Ex;
		}
		catch(Exception Ex)
		{
			BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
			NameValueCollection FunctionInfo = new NameValueCollection();
			FunctionInfo.Add("Method","FinanceCalculator:CalculateResult()");

			object [] objects = new object[4];
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
