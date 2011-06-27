using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Collections.Specialized;
using System.Data;
using DaoReports;
using VoReports;
using System.Configuration;

using Microsoft.ApplicationBlocks.ExceptionManagement;


namespace BoReports
{
    public class WERPReports
    {
        /// <summary>
        /// To Get the State Name using State Code
        /// </summary>
        /// <param name="path"></param>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public string GetState(string path, string stateCode)
        {
            DataSet ds;
            DataRow[] dr;
            DataRow row;
            string category;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                if (ds.Tables["State"].Select("StateCode = '" + stateCode.ToString() + "'") != null)
                {
                    dr = ds.Tables["State"].Select("StateCode = '" + stateCode.ToString() + "'");
                    row = dr[0];
                    category = row["StateName"].ToString();
                }
                else
                    category = stateCode;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetCategory()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = stateCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return category;
        }

       
    }
}
