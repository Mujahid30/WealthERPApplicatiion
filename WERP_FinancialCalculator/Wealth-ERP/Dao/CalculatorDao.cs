using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoCalculator
{
	/// <summary>
	/// Summary description for CalculatorDao
	/// </summary>
	public class CalculatorDao
	{
		public DataSet GetInstrumentType()
		{
			Database db;
			DbCommand GetInstrumentTypeCmd;
			DataSet dsInstrumentType;

			try
			{
				db = DatabaseFactory.CreateDatabase("wealtherp");
				GetInstrumentTypeCmd = db.GetStoredProcCommand("SP_GetInstrumentTypeDetails");
				dsInstrumentType = db.ExecuteDataSet(GetInstrumentTypeCmd);
			}
			catch (BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();
				FunctionInfo.Add("Method", "CalculatorDao.cs:GetInstrumentType()");
				FunctionInfo = exBase.AddObject(FunctionInfo, null);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;
			}
			return dsInstrumentType;
		}

		public DataSet GetOutputType()
		{
			Database db;
			DbCommand GetOutputTypeCmd;
			DataSet dsOutputType;

			try
			{
				db = DatabaseFactory.CreateDatabase("wealtherp");
				GetOutputTypeCmd = db.GetStoredProcCommand("SP_GetOutputType");
				dsOutputType = db.ExecuteDataSet(GetOutputTypeCmd);
			}
			catch (BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();
				FunctionInfo.Add("Method", "CalculatorDao.cs:GetOutputType()");
				FunctionInfo = exBase.AddObject(FunctionInfo, null);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;
			}
			return dsOutputType;
		}

		public DataSet GetInputForSelOutput()
		{
			Database db;
			DbCommand GetInputForSelOutputCmd;
			DataSet dsInputFields;

			try
			{
				db = DatabaseFactory.CreateDatabase("wealtherp");
				GetInputForSelOutputCmd = db.GetStoredProcCommand("SP_GetIuputForSelectedOutput");
				dsInputFields = db.ExecuteDataSet(GetInputForSelOutputCmd);
			}
			catch (BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();
				FunctionInfo.Add("Method", "CalculatorDao.cs:GetOutputType()");
				FunctionInfo = exBase.AddObject(FunctionInfo, null);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;
			}
			return dsInputFields;
		}

		public DataSet GetFormula()
		{
			Database db;
			DbCommand GetFormulaCmd;
			DataSet dsFormula;

			try
			{
				db = DatabaseFactory.CreateDatabase("wealtherp");
				GetFormulaCmd = db.GetStoredProcCommand("SP_GetFormula");
				dsFormula = db.ExecuteDataSet(GetFormulaCmd);
			}
			catch (BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();
				FunctionInfo.Add("Method", "CalculatorDao.cs:GetOutputType()");
				FunctionInfo = exBase.AddObject(FunctionInfo, null);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;
			}
			return dsFormula;
		}

		public DataSet GetCurrentMapping(string sMappingTable)
		{
			Database db;
			DbCommand GetMappingTypeCmd;
			DataSet dsMappingDetails;

			try
			{
				db = DatabaseFactory.CreateDatabase("wealtherp");
				GetMappingTypeCmd = db.GetStoredProcCommand("SP_GetMappingDetails");
				dsMappingDetails = db.ExecuteDataSet(GetMappingTypeCmd);
			}
			catch (BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();
				FunctionInfo.Add("Method", "CalculatorDao.cs:GetCurrentMapping()");
				FunctionInfo = exBase.AddObject(FunctionInfo, null);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;
			}
			return dsMappingDetails;
		}

		/// <summary>
		/// Summary description for CalculatorDao
		/// </summary>
		public DataSet GetInterestFrequency()
		{
			Database db;
			DbCommand GetInterestFrequencyCmd;
			DataSet dsInterestFrequency;

			try
			{
				db = DatabaseFactory.CreateDatabase("wealtherp");
				GetInterestFrequencyCmd = db.GetStoredProcCommand("SP_GetInterestFrequencyDetails");
				dsInterestFrequency = db.ExecuteDataSet(GetInterestFrequencyCmd);
			}
			catch (BaseApplicationException Ex)
			{
				throw Ex;
			}
			catch (Exception Ex)
			{
				BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
				NameValueCollection FunctionInfo = new NameValueCollection();
				FunctionInfo.Add("Method", "CalculatorDao.cs:GetInterestFrequency()");
				FunctionInfo = exBase.AddObject(FunctionInfo, null);
				exBase.AdditionalInformation = FunctionInfo;
				ExceptionManager.Publish(exBase);
				throw exBase;
			}
			return dsInterestFrequency;
		}
	}
}
