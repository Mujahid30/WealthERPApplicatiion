using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace WERP_CORPORATE_ACTION
{
   public class CorporateActionDao
    {
       public DataSet GetCATransactionDetails()
       {
           Database db;
           DbCommand getCATransactionDetailsCmd;
           DataSet dsCorporateActionTransactionDetails;


           db = DatabaseFactory.CreateDatabase("wealtherp");
           getCATransactionDetailsCmd = db.GetStoredProcCommand("SPROC_GetCATransactionDetails");
           dsCorporateActionTransactionDetails = db.ExecuteDataSet(getCATransactionDetailsCmd);

           return dsCorporateActionTransactionDetails;

       }
       public void CreateAdviserEQSplittedTransactions(int adviserId, DataTable dtCustomerFullTransactionAfterSplit)
       {

           string conString;
           conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
           SqlConnection sqlCon = new SqlConnection(conString);

           try
           {
               SqlCommand cmd;               
               cmd = new SqlCommand("SPROC_BulkInsertEquitySplittedTransactionCA", sqlCon);             
               cmd.CommandType = CommandType.StoredProcedure;
               //SqlParameter param = new SqlParameter("@BalanceTable", SqlDbType.Structured);
               //cmd.Parameters.Add(param);
               //SqlDataAdapter da = new SqlDataAdapter(cmd);

               // Add the input parameter and set its properties.
               SqlParameter sqlParameterAdviserId = new SqlParameter();
               sqlParameterAdviserId.ParameterName = "@AdviserId";
               sqlParameterAdviserId.SqlDbType = SqlDbType.BigInt;
               sqlParameterAdviserId.Direction = ParameterDirection.Input;
               sqlParameterAdviserId.Value = adviserId;

               // Add the parameter to the Parameters collection. 
               cmd.Parameters.Add(sqlParameterAdviserId);

               // Add the input parameter and set its properties.
               SqlParameter sqlParameter = new SqlParameter();
               sqlParameter.ParameterName = "@CustomerEQSplittedTransactions";
               sqlParameter.SqlDbType = SqlDbType.Structured;
               //parameter.Direction = ParameterDirection.Input;
               sqlParameter.Value = dtCustomerFullTransactionAfterSplit;
    
               // Add the parameter to the Parameters collection. 
               cmd.Parameters.Add(sqlParameter);

               sqlCon.Open();
               cmd.ExecuteNonQuery();

           }

           catch (BaseApplicationException Ex)
           {
               throw Ex;
           }
           catch (Exception Ex)
           {
               BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
               NameValueCollection FunctionInfo = new NameValueCollection();

           }
           finally
           {
               sqlCon.Close();
           }
       }

       public double GetCorporateActionRatio()
       {
           Database db;
           DbCommand getCAFactorCmd;
           double caRatio=0;


           db = DatabaseFactory.CreateDatabase("wealtherp");
           getCAFactorCmd = db.GetStoredProcCommand("SPROC_GetCorporateActionRatio");
           db.AddOutParameter(getCAFactorCmd, "@caRatio", DbType.Int32, 4);
           if (db.ExecuteNonQuery(getCAFactorCmd) != 0)
           {
               caRatio = int.Parse(db.GetParameterValue(getCAFactorCmd, "caRatio").ToString());
           }
           return caRatio;

       }

       
    }
}
