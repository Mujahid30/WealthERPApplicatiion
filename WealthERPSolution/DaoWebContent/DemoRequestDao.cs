using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoWebContent;
using System.Data.Sql;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoWebContent
{
    public class DemoRequestDao
    {
        public bool Add(DemoRequestVo demoRequestVo)
        {

            Database db;
            DbCommand insertCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                insertCmd = db.GetStoredProcCommand("SP_DemoRequestsInsert");
                db.AddInParameter(insertCmd, "@DR_Name", DbType.String, demoRequestVo.Name);
                db.AddInParameter(insertCmd, "@DR_Designation", DbType.String, demoRequestVo.Designation);
                db.AddInParameter(insertCmd, "@DR_CompanyName", DbType.String, demoRequestVo.CompanyName);
                db.AddInParameter(insertCmd, "@DR_EmailId", DbType.String, demoRequestVo.EmailId);
                db.AddInParameter(insertCmd, "@DR_MobileNumber", DbType.String, demoRequestVo.MobileNumber);
                db.AddInParameter(insertCmd, "@DR_Location", DbType.String, demoRequestVo.Location);
                db.AddInParameter(insertCmd, "@DR_Message", DbType.String, demoRequestVo.Message);

                affectedRows = db.ExecuteNonQuery(insertCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "DemoRequestDao.cs:Add()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            if (affectedRows > 0)
                return true;
            else
                return false;
        }
    }
}
