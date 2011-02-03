using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using log4net;
using log4net.Config;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
/// <summary>
/// Summary description for DBConnection
/// </summary>
public class DBConnection
{
	ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	public DBConnection()
	{
		//
		// TODO: Add constructor logic here
		//
	}
	/// <summary>
	/// </summary>
	 /// <param name="sQuery"></param>
	/// <returns>Result DataSet</returns>
	public static DataSet ExecuteDataSet(string sQuery)
	{
		string connectionString = null;
		DataSet dsResult = null;
		string sCommandString = null;
		try
		{
			connectionString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;

			if (sQuery != null && sQuery.Length > 0)
				sCommandString = sQuery;
			dsResult = new DataSet();

			SqlDataAdapter adapter = new SqlDataAdapter(sCommandString, connectionString);
			adapter.Fill(dsResult);
		}
		catch(Exception ex)
		{
			ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			logger.Error(ex.Message);
			throw ex;
		}
		return dsResult;
	}
}
