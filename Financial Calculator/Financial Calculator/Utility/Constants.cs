using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
	public class Constants
	{
		public const string cStr_InstrumentType = "ITM_InstrumentType";
		public const string cStr_Id = "ITM_Id";
		public const string cStr_InstrumentType_DefaultMsg = "-Select Instrument Type-";
		public const string cStr_InstrumentType_DefaultMsg_Value = "-1";
		public const string cStr_InstrumentTypeId = "OM_InstrumentTypeId";
		public const string cStr_OutputType = "OM_OutputType";
		public const string cStr_OutputId = "OM_OutputId";
		public const string cStr_IOM_InstrumentTypeId = "IOM_InstrumentTypeId";
		public const string cStr_IOM_OutputTypeId = "IOM_OutputTypeId";
		public const string cStr_IM_InputType = "IM_InputType";
		public const string cStr_IM_Abbrevation = "IM_Abbrevation";
		public const string cStr_IOM_InputFlag = "IOM_InputFlag";
		public const string cStr_IOM_FieldType = "IOM_FieldType";
		public const string cStr_OFM_InstrumentTypeId = "OFM_InstrumentTypeId";
		public const string cStr_IF_InterestFreqauency = "IF_InterestFreqauency";
		public const string cStr_TblInputField = "tblInputField";

		public const int cStr_IT_KVP = 1;
		public const int cStr_IT_NSS = 2;
		public const int cStr_IT_NSC = 3;
		public const int cStr_IT_FixedDeposits = 4;
		public const int cStr_IT_CompanyFD = 5;
		public const int cStr_IT_GOIReliefBonds = 6;
		public const int cStr_IT_GOITaxSavingBonds = 7;
		public const int cStr_IT_TaxSavingBonds = 8;
		public const int cStr_IT_SeniorCitizensSavingsScheme = 9;
		public const int cStr_IT_PostOfficeSavingsBankAcc = 10;
		public const int cStr_IT_PostOfficeMIS = 11;
		public const int cStr_IT_Gratuity = 12;
		public const int cStr_IT_Annuity = 13;
		public const int cStr_IT_EPF = 14;
		public const int cStr_IT_PPF = 15;
		public const int cStr_IT_Superannuation = 16;
		public const int cStr_IT_SavingsAccount = 17;
		public const int cStr_IT_CurrentAccount = 18;
		public const int cStr_IT_CashAtHand = 19;
		public const int cStr_IT_LoansAndAdvances = 20;
		public const int cStr_IT_CorporateBonds = 21;

		public const int cStr_IO_CurrentValue = 1;
		public const int cStr_IO_MaturityValue = 2;
		public const int cStr_IO_InterestAccumulatedEarnedTillDate = 4;
		public const int cStr_IO_InterestAccumulatedEarnedTillMaturity = 5;

		public const int cStr_IO_InterestOnAccumalatedAmountAsOnLastFiscalYear = 9;
		public const int cStr_IO_InterestOnEmployeeContributionForCurrentFiscalYeare = 10;
		public const int cStr_IO_InterestOnEmployerContributionForCurrentFiscalYear = 11;
		public const int cStr_IO_InterestOnYearlyContributionForCurrentFiscalYear = 12;

		public const int cStr_IO_GratuityAmountWhenCoveredUnderGratuityAct = 13;
		public const int cStr_IO_GratuityAmountWhenNotCoveredUnderGratuityAct = 14;
		public const int cStr_IO_VestingValue = 15;
		public const int cStr_IO_WithdrawlAmount = 16;

		public const string cStr_Formula_InterestAccumulatedEarnedTillDate = "3";
		public const string cStr_Formula_SimpleInterestBasis = "9";
		public const string cStr_Formula_CompoundInterestAccumulatedEarnedTillDate = "1";
		public const string cStr_Formula_CurrentValueOrMaturityValue = "2";
	}
}
