using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoCalculator;
using System.Collections;
using System.Globalization;
using Utility;
namespace BoCalculator
{

	/// <summary>
	/// Summary description for CalculatorBo
	/// </summary>
	public class CalculatorBo
	{
		public static DataSet dsInstType = new DataSet();
		public static DataSet dsOutputType = new DataSet();
		public static DataSet dsInputForSelOutput = new DataSet();
		public static DataSet dsFormula = new DataSet();
		public static DataSet dsInterestFreq = new DataSet();
		public static IFormatProvider format = new CultureInfo("en-GB");

		static CalculatorBo()
		{
			dsInstType = GetInstrumentType();
			dsOutputType = GetOutputType();
			dsInputForSelOutput = GetInputForSelOutput();
			dsFormula = GetFormula();
			dsInterestFreq = GetInterestFrequency();
		}

		/// <summary>Get List of InstrumentTypes
		/// </summary>
		/// <returns>Dataset</returns>
		public static DataSet GetInstrumentType()
		{
			DataSet dsInstrumentType;
			CalculatorDao calculatorDao = new CalculatorDao();
			try
			{
				dsInstrumentType = calculatorDao.GetInstrumentType();
			}
			catch (BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();

				FunctionInfo.Add("Method", "CalculatorBo.cs:GetInstrumentType()");

				FunctionInfo = exBase.AddObject(FunctionInfo, null);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;

			}
			return dsInstrumentType;
		}

		/// <summary>
		/// </summary>
		/// <returns>Dataset of OutputType Details</returns>
		public static DataSet GetOutputType()
		{
			DataSet dsOutputType;
			CalculatorDao calculatorDao = new CalculatorDao();
			try
			{
				dsOutputType = calculatorDao.GetOutputType();
			}
			catch (BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();

				FunctionInfo.Add("Method", "CalculatorBo.cs:GetOutputType()");

				FunctionInfo = exBase.AddObject(FunctionInfo, null);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;

			}
			return dsOutputType;
		}

		/// <summary>
		/// </summary>
		/// <returns>Dataset of Input Details</returns>
		public static DataSet GetInputForSelOutput()
		{
			DataSet dsInputDetails;
			CalculatorDao calculatorDao = new CalculatorDao();
			try
			{
				dsInputDetails = calculatorDao.GetInputForSelOutput();
			}
			catch (BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();

				FunctionInfo.Add("Method", "CalculatorBo.cs:GetInputForSelOutput()");

				FunctionInfo = exBase.AddObject(FunctionInfo, null);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;

			}
			return dsInputDetails;
		}

		/// <summary>
		/// 
		/// </summary>
		
		/// <returns>Dataset of Formula</returns>
		public static DataSet GetFormula()
		{
			DataSet dsFormula;
			CalculatorDao calculatorDao = new CalculatorDao();
			try
			{
				dsFormula = calculatorDao.GetFormula();
			}
			catch (BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();

				FunctionInfo.Add("Method", "CalculatorBo.cs:GetFormula()");

				FunctionInfo = exBase.AddObject(FunctionInfo, null);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;

			}
			return dsFormula;
		}

		/// <summary>
		/// </summary>
		/// <returns>Dataset of InterestFrequency Details</returns>
		public static DataSet GetInterestFrequency()
		{
			DataSet dsInterestFreq;
			CalculatorDao calculatorDao = new CalculatorDao();
			try
			{
				dsInterestFreq = calculatorDao.GetInterestFrequency();
			}
			catch (BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();

				FunctionInfo.Add("Method", "CalculatorBo.cs:GetInterestFrequency()");

				FunctionInfo = exBase.AddObject(FunctionInfo, null);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;

			}
			return dsInterestFreq;
		}

		/// <summary>
		/// </summary>
		/// <param name="iInstrType"></param>
		/// <param name="iInstrOPType"></param>
		/// <param name="hInputValues"></param>
		/// <param name="sErrMsg"></param>
		/// <param name="alErrorMsg"></param>
		/// <returns>Calculated result</returns>
		public static Double DoCalculate(int iInstrType, int iInstrOPType, Hashtable hInputValues, ref string sErrMsg, ref ArrayList alErrorMsg)
		{
			Double dResult= 0;
			try
			{
				// Validate input fields
				dResult = ValidateFields(hInputValues,iInstrType,iInstrOPType, ref sErrMsg, ref alErrorMsg);
				if (dResult > 0)
				{
					if (iInstrType == Constants.cStr_IT_SavingsAccount || iInstrType == Constants.cStr_IT_CurrentAccount || iInstrType == Constants.cStr_IT_CashAtHand)
					{
						if (iInstrType == Constants.cStr_IT_CashAtHand)
							dResult = Convert.ToDouble(hInputValues["DAMT"]);
						else
							dResult = Convert.ToDouble(hInputValues["AAB"]);
						return dResult;
					}

					// Set calculated field values.
					setCalculatedFields(iInstrType, iInstrOPType, hInputValues);

					string sFormula = null;
					int nCount = dsFormula.Tables[0].Rows.Count;
					
					DataTable dtFormula = dsFormula.Tables[0];

					var resultFormula = from formula in dtFormula.AsEnumerable()
								where formula.Field<Int32>("OFM_InstrumentTypeId") == iInstrType && 
								formula.Field<Int32>("OFM_OutputId") == iInstrOPType
								select formula;

					foreach(var formula in resultFormula)
					{
						// If interest calculation basis is simple use formula 9 or formula 2 for the instruments, 
						if((iInstrType == Constants.cStr_IT_FixedDeposits || iInstrType == Constants.cStr_IT_CompanyFD || iInstrType == Constants.cStr_IT_GOIReliefBonds || iInstrType == Constants.cStr_IT_GOITaxSavingBonds || iInstrType == Constants.cStr_IT_TaxSavingBonds) && (iInstrOPType == Constants.cStr_IO_CurrentValue || iInstrOPType == Constants.cStr_IO_MaturityValue || iInstrOPType == Constants.cStr_IO_InterestAccumulatedEarnedTillDate || iInstrOPType == Constants.cStr_IO_InterestAccumulatedEarnedTillMaturity)&& hInputValues["ICB"].Equals("Simple"))
						{
							if(formula["OFM_FormulaId"].ToString().Equals(Constants.cStr_Formula_SimpleInterestBasis) && formula.ItemArray[3].ToString().Equals(Constants.cStr_Formula_SimpleInterestBasis))
							{
								sFormula = formula["FM_Formula"].ToString();
								break;
							}
							else if(formula["OFM_FormulaId"].ToString().Equals(Constants.cStr_Formula_InterestAccumulatedEarnedTillDate) && formula.ItemArray[3].ToString().Equals(Constants.cStr_Formula_InterestAccumulatedEarnedTillDate))
							{
								sFormula = formula["FM_Formula"].ToString();
								break;
							}
						}
						else if(iInstrType == Constants.cStr_IT_SeniorCitizensSavingsScheme || iInstrType == Constants.cStr_IT_PostOfficeSavingsBankAcc || iInstrType == Constants.cStr_IT_PostOfficeMIS)
						{
							sFormula = formula["FM_Formula"].ToString();
							break;
						}
						else
						{
							if(formula["OFM_FormulaId"].ToString().Equals(Constants.cStr_Formula_SimpleInterestBasis) && formula.ItemArray[3].ToString().Equals(Constants.cStr_Formula_SimpleInterestBasis))
								continue;
							else if(formula["OFM_FormulaId"].ToString().Equals(Constants.cStr_Formula_InterestAccumulatedEarnedTillDate) && formula.ItemArray[3].ToString().Equals(Constants.cStr_Formula_InterestAccumulatedEarnedTillDate))
								continue;
							else if(formula["OFM_FormulaId"].ToString().Equals(Constants.cStr_Formula_CompoundInterestAccumulatedEarnedTillDate) && formula.ItemArray[3].ToString().Equals(Constants.cStr_Formula_CompoundInterestAccumulatedEarnedTillDate))
							{
								sFormula = formula["FM_Formula"].ToString();
								break;
							}
							else if(formula["OFM_FormulaId"].ToString().Equals(Constants.cStr_Formula_CurrentValueOrMaturityValue) && formula.ItemArray[3].ToString().Equals(Constants.cStr_Formula_CurrentValueOrMaturityValue))
							{
								sFormula = formula["FM_Formula"].ToString();
								break;
							}
							else
							{
								sFormula = formula["FM_Formula"].ToString();
								break;
							}
						}
					}
					// Months will be rounded of to the nearest year if the covered under Gratuity Act otherwise no rounded off
					if (iInstrType == Constants.cStr_IT_Gratuity)
					{
						int nNoOfMonthComp = Convert.ToInt32(hInputValues["NOMC"]);
						int nNoOfYearsComp = Convert.ToInt32(hInputValues["CYOS"]);
						if (iInstrOPType == Constants.cStr_IO_GratuityAmountWhenCoveredUnderGratuityAct)
						{
							if (nNoOfMonthComp > 5)
								nNoOfYearsComp += 1;
							hInputValues["CYOS"] = nNoOfYearsComp;
						}
						else
							hInputValues["CYOS"] = nNoOfYearsComp;

					}
					IDictionaryEnumerator en = hInputValues.GetEnumerator();
					while (en.MoveNext())
					{
						string str = en.Key.ToString();
						if(sFormula.Contains(str))
						{
							sFormula = sFormula.Replace(str,en.Value.ToString());
						}
					}

					FunctionParser fn = new FunctionParser();
					fn.Parse(sFormula);
					fn.Infix2Postfix();
					fn.EvaluatePostfix();
					dResult = Math.Round(fn.Result,2);
				}
				return dResult;
			}
			catch(BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();
				
				object[] objects = new object[4];
				objects[0] = iInstrType;
				objects[1] = iInstrOPType;
				objects[2] = hInputValues;
				objects[3] = sErrMsg;
				objects[4] = alErrorMsg;
				FunctionInfo.Add("Method", "CalculatorBo.cs:DoCalculate()");
				FunctionInfo = exBase.AddObject(FunctionInfo, objects);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;
			}
		}

		/// <summary>
		/// Calculate duration
		/// </summary>
		/// <param name="iInstrType"></param>
		/// <param name="iInstrOPType"></param>
		/// <param name="hInputValues"></param>
		/// <returns></returns>
		private static void setCalculatedFields(int iInstrType, int iInstrOPType, Hashtable hInputValues)
		{
			string sDuration = "0";
			try
			{
			switch(iInstrType)
			{
				case Constants.cStr_IT_KVP:
				case Constants.cStr_IT_NSC:
					if (dsInterestFreq != null)
					{
						string sValue = Convert.ToString(hInputValues["ICOM"]);
						DataTable dtinterestFreg = dsInterestFreq.Tables[0];

						var interestFreg = from interFreq in dtinterestFreg.AsEnumerable()
						where interFreq.Field<String>("IF_InterestFreqauency") == "Half Yearly" 
						select interFreq;
						foreach(var intFreq in interestFreg)
						{
							hInputValues["KVAL"] = intFreq["IF_KValue"];
							hInputValues["LVAL"] = intFreq["IF_LValue"];
						}
					}
					if(iInstrOPType == Constants.cStr_IO_CurrentValue || iInstrOPType == Constants.cStr_IO_InterestAccumulatedEarnedTillDate)
					{
						
						DateTime dDepDate = Convert.ToDateTime((hInputValues["DDAT"]));
						sDuration = DateTime.Now.Subtract(dDepDate).Days.ToString();
					}
					else if(iInstrOPType == Constants.cStr_IO_MaturityValue || iInstrOPType == Constants.cStr_IO_InterestAccumulatedEarnedTillMaturity)
					{
						DateTime dDepDate = Convert.ToDateTime(hInputValues["DDAT"]);
						DateTime dMatDate = Convert.ToDateTime(hInputValues["MDAT"]);
						sDuration = dMatDate.Subtract(dDepDate).Days.ToString();
					}
					break;
				case Constants.cStr_IT_NSS:
				case Constants.cStr_IT_FixedDeposits:
				case Constants.cStr_IT_CompanyFD:
				case Constants.cStr_IT_GOIReliefBonds:
				case Constants.cStr_IT_GOITaxSavingBonds:
				case Constants.cStr_IT_TaxSavingBonds:
					if (dsInterestFreq != null)
					{
						int iValue = Convert.ToInt32(hInputValues["ICOM"]);
						DataTable dtinterestFreg = dsInterestFreq.Tables[0];

						var interestFreg = from interFreq in dtinterestFreg.AsEnumerable()
											where interFreq.Field<Int32>("IF_RowId") == iValue 
						select interFreq;
						foreach(var intFreq in interestFreg)
						{
							hInputValues["KVAL"] = intFreq["IF_KValue"];
							hInputValues["LVAL"] = intFreq["IF_LValue"];
						}
					}

					if(iInstrOPType == Constants.cStr_IO_CurrentValue || iInstrOPType == Constants.cStr_IO_InterestAccumulatedEarnedTillDate)
					{
						DateTime dDepDate = Convert.ToDateTime(hInputValues["DDAT"]);
						sDuration = DateTime.Now.Subtract(dDepDate).Days.ToString();
					}
					else if(iInstrOPType == Constants.cStr_IO_MaturityValue || iInstrOPType == Constants.cStr_IO_InterestAccumulatedEarnedTillMaturity)
					{
						DateTime dDepDate = Convert.ToDateTime(hInputValues["DDAT"]);
						DateTime dMatDate = Convert.ToDateTime(hInputValues["MDAT"]);
						sDuration = dMatDate.Subtract(dDepDate).Days.ToString();
					}
					break;

				case Constants.cStr_IT_SeniorCitizensSavingsScheme:
				case Constants.cStr_IT_PostOfficeSavingsBankAcc:
				case Constants.cStr_IT_PostOfficeMIS:
					if(iInstrOPType == Constants.cStr_IO_CurrentValue || iInstrOPType == Constants.cStr_IO_InterestAccumulatedEarnedTillDate)
					{
						DateTime dDepDate = Convert.ToDateTime(hInputValues["DDAT"]);
						sDuration = DateTime.Now.Subtract(dDepDate).Days.ToString();
					}
					else if(iInstrOPType == Constants.cStr_IO_MaturityValue || iInstrOPType == Constants.cStr_IO_InterestAccumulatedEarnedTillMaturity)
					{
						DateTime dDepDate = Convert.ToDateTime(hInputValues["DDAT"]);
						DateTime dMatDate = Convert.ToDateTime(hInputValues["MDAT"]);
						sDuration = dMatDate.Subtract(dDepDate).Days.ToString();
					}
					break;
				case Constants.cStr_IT_Gratuity:
					if(iInstrOPType == Constants.cStr_IO_CurrentValue)
					{
						DateTime dDepDate = Convert.ToDateTime(hInputValues["DDAT"]);
						sDuration = DateTime.Now.Subtract(dDepDate).Days.ToString();
					}
					else if(iInstrOPType == Constants.cStr_IO_VestingValue)
					{
						DateTime dVDate = Convert.ToDateTime(hInputValues["VDAT"]);
						sDuration = DateTime.Now.Subtract(dVDate).Days.ToString();
					}
					break;
				case Constants.cStr_IT_Annuity:
					if (dsInterestFreq != null)
					{
						string sValue = Convert.ToString(hInputValues["ICOM"]);
						DataTable dtinterestFreg = dsInterestFreq.Tables[0];

						var interestFreg = from interFreq in dtinterestFreg.AsEnumerable()
						where interFreq.Field<String>("IF_InterestFreqauency") == "Annualy" 
						select interFreq;
						foreach(var intFreq in interestFreg)
						{
							hInputValues["KVAL"] = intFreq["IF_KValue"];
							hInputValues["LVAL"] = intFreq["IF_LValue"];
						}
					}
					if(iInstrOPType == Constants.cStr_IO_WithdrawlAmount)
					{
						DateTime dPurDate = Convert.ToDateTime(hInputValues["DDAT"]);
						sDuration = DateTime.Now.Subtract(dPurDate).Days.ToString();
					}
					
					else if(iInstrOPType == Constants.cStr_IO_CurrentValue)
					{
						DateTime dDepDate = Convert.ToDateTime(hInputValues["DDAT"]);
						double nDur = 0;
						
						sDuration = DateTime.Now.Subtract(dDepDate).Days.ToString();
						nDur = Convert.ToDouble(sDuration);
						sDuration = Convert.ToString(nDur);
					}
					break;
				case Constants.cStr_IT_EPF:
				case Constants.cStr_IT_PPF:
				case Constants.cStr_IT_Superannuation:
					if(iInstrOPType == Constants.cStr_IO_CurrentValue || iInstrOPType == Constants.cStr_IO_InterestOnAccumalatedAmountAsOnLastFiscalYear || iInstrOPType == Constants.cStr_IO_InterestOnEmployeeContributionForCurrentFiscalYeare || iInstrOPType == Constants.cStr_IO_InterestOnEmployerContributionForCurrentFiscalYear || iInstrOPType == Constants.cStr_IO_InterestOnYearlyContributionForCurrentFiscalYear)
					{
						DateTime cDate = DateTime.Now;
						DateTime sFYear = new DateTime(cDate.Year,4,1);
						sDuration = DateTime.Now.Subtract(sFYear).Days.ToString();
					}
					break;
				case Constants.cStr_IT_CorporateBonds:
					if (dsInterestFreq != null)
					{
						string sValue = Convert.ToString(hInputValues["ICOM"]);
						DataTable dtinterestFreg = dsInterestFreq.Tables[0];

						var interestFreg = from interFreq in dtinterestFreg.AsEnumerable()
						where interFreq.Field<String>("IF_InterestFreqauency") == "Annualy" 
						select interFreq;
						foreach(var intFreq in interestFreg)
						{
							hInputValues["KVAL"] = intFreq["IF_KValue"];
							hInputValues["LVAL"] = intFreq["IF_LValue"];
						}
					}
					if(iInstrOPType == Constants.cStr_IO_CurrentValue || iInstrOPType == 3)
					{
						DateTime dPurDate = Convert.ToDateTime(hInputValues["DOP"]);
						sDuration = DateTime.Now.Subtract(dPurDate).Days.ToString();
					}
					break;
				default:
					break;
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
				FunctionInfo.Add("Method", "CalculatorBo.cs:setCalculatedFields()");

				object[] objects = new object[2];
				objects[0] = iInstrType;
				objects[1] = iInstrOPType;
				objects[2] = hInputValues;

				FunctionInfo = exBase.AddObject(FunctionInfo, objects);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;
			}
			hInputValues["DUR"] = sDuration;
		}

		/// <summary>
		/// </summary>
		/// <param name="hInputValues"></param>
		/// <param name="iInstrType"></param>
		/// <param name="iInstrOPType"></param>
		/// <param name="sErrorMsg"></param>
		/// <param name="alErrorMsg"></param>
		/// <returns>Validation result</returns>
		private static double ValidateFields(Hashtable hInputValues,int iInstrType, int iInstrOPType, ref string sErrorMsg, ref ArrayList alErrorMsg)
		{
			
			IDictionaryEnumerator en = hInputValues.GetEnumerator();
			double returnVal = 1;
				try
				{
					while (en.MoveNext())
					{
						string strAbb = en.Key.ToString();
						string sType = null;
						string sValue = null;
						string sInstType = null;
						DataTable dtInput = dsInputForSelOutput.Tables[0];
						var result = from inputFields in dtInput.AsEnumerable()
									where inputFields.Field<String>("IM_Abbrevation") == strAbb
									select inputFields;

						foreach(var input in result)
						{
							sType = input["IOM_FieldType"].ToString();
							sInstType = input["IM_InputType"].ToString();
							break;
						}
						sValue = en.Value.ToString();
						if (sType.Length > 0 && sType != "NULL" && sInstType != "k" && sInstType != "l")
							if (sValue.Length < 1)
							{
								sErrorMsg = string.Format("{0} : value can not be empty.", sInstType);
								alErrorMsg.Add(sErrorMsg);
								returnVal = -1;
							}
							else
							{
								if (sType == "Date")
								{
									DateTime tempDateTime;
									string sDateTime = sValue;
									
									if (!DateTime.TryParse(sDateTime, out tempDateTime))
									{		sErrorMsg = string.Format("{0}: value is not valid date format. date format should be dd/mm/yyyy", sInstType);
											alErrorMsg.Add(sErrorMsg);
											returnVal = -1;
									}
								}
								else if (sType == "DDL")
									continue;
								else if(en.Key.ToString() == "DAMT" || en.Key.ToString() == "DAMT"
									|| en.Key.ToString() == "ABAL" || en.Key.ToString() == "ADEP"
									|| en.Key.ToString() == "WAMT" || en.Key.ToString() == "AMTLFY"
									|| en.Key.ToString() == "ECFCY" || en.Key.ToString() == "ERCFY"
									|| en.Key.ToString() == "LDS" || en.Key.ToString() == "YCFCFY"
									|| en.Key.ToString() == "AAB" || en.Key.ToString() == "PP")
								{
									bool isNumeric = IsNumeric(sValue, ref sErrorMsg, sInstType);

									if (!isNumeric)
									{
										alErrorMsg.Add(sErrorMsg);
										returnVal = -1;
									}
								}
								else
								{
									bool isNumeric = IsNumeric(sValue, ref sErrorMsg, sInstType);

									if (!isNumeric)
									{
										alErrorMsg.Add(sErrorMsg);
										returnVal = -1;
									}
								}
							}
					}
					if(returnVal != -1)
					{
						string sDDAT = Convert.ToString(hInputValues["DDAT"]);
						string sMDAT = Convert.ToString(hInputValues["MDAT"]);

						if(sDDAT.Length > 0 && sMDAT.Length > 0)
						{
							DateTime dDepDate = Convert.ToDateTime(hInputValues["DDAT"]);
							DateTime dMatDate = Convert.ToDateTime(hInputValues["MDAT"]);
							if(dMatDate < dDepDate)
							{
								sErrorMsg = string.Format("Maturity Date should be greater than Deposit Date.");
								alErrorMsg.Add(sErrorMsg);
								returnVal = -1;
							}
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

				object[] objects = new object[5];
				objects[0] = hInputValues;
				objects[1] = iInstrType;
				objects[2] = iInstrOPType;
				objects[3] = sErrorMsg;
				objects[4] = alErrorMsg;

				FunctionInfo.Add("Method", "CalculatorBo.cs:ValidateFields()");
				FunctionInfo = exBase.AddObject(FunctionInfo, objects);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;
			}
			return returnVal;
		}

		/// <summary>Check whether input is date
		/// </summary>
		/// <param name="sValue"></param>
		/// <returns>bool result</returns>
		private static bool IsDateTime(string sValue)
		{
			DateTime retValue;
			bool isDateTime = false;
			
			try 
			{
				isDateTime = DateTime.TryParse(sValue, out retValue);
			}
			catch(BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();

				object[] objects = new object[1];
				objects[0] = sValue;

				FunctionInfo.Add("Method", "CalculatorBo.cs:IsNumeric()");
				FunctionInfo = exBase.AddObject(FunctionInfo, objects);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;
			}
			return isDateTime;
		}

		/// <summary>Check whether input is numeric
		/// </summary>
		/// <param name="sValue"></param>
		/// <param name="sErrorMsg"></param>
		/// <param name="sInstType"></param>
		/// <returns>bool result</returns>
		private static bool IsNumeric(string sValue, ref string sErrorMsg, string sInstType)
		{
			double retValue;
			bool isNumeric = false;
			
			try 
			{
				isNumeric = double.TryParse(sValue, out retValue);

				if (!isNumeric)
					sErrorMsg = string.Format("{0} : value is not valid.", sInstType);

			}
			catch(BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();

				object [] objects = new object[3];
				objects[0] = sValue;
				objects[1] = sErrorMsg;
				objects[2] = sInstType;

				FunctionInfo.Add("Method", "CalculatorBo.cs:IsNumeric()");
				FunctionInfo = exBase.AddObject(FunctionInfo, objects);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;
			}
			return isNumeric;
		}
	}
}
