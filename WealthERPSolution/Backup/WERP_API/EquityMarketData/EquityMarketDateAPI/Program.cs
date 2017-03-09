using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data;

using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Configuration;
namespace EquityMarketDateAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dsResult = new DataSet();
            EquityAPIBO EquityAPIBO = new EquityAPIBO();
            dsResult = TriggerAPIConvertToDT(ConfigurationSettings.AppSettings["EquityMarket"] + DateTime.Now.ToString("yyyy-MM-dd"), "EquityMaster");
            if (dsResult.Tables.Count > 1 && dsResult.Tables[1].Rows[0]["StatusCode"].ToString() == "01")
            {
                EquityAPIBO.CreateUpdateEquityMarketData(dsResult.Tables[0]);
            }
            dsResult.Reset();
            dsResult = TriggerAPIConvertToDT(ConfigurationSettings.AppSettings["EquityMaster"], "EquityScriptMasterList");
            if (dsResult.Tables.Count > 1 && dsResult.Tables[1].Rows[0]["StatusCode"].ToString() == "01")
            {
                EquityAPIBO.CreateUpdateEquityMasterData(dsResult.Tables[0]);
            }
        }
        public static DataSet TriggerAPIConvertToDT(string API, string rootName)
        {
            DataSet ds = new DataSet();
            try
            {
                WebResponse response;
                string result;
                WebRequest request1 = HttpWebRequest.Create(API);
                response = request1.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                    reader.Close();
                }
                StringReader theReader1 = new StringReader(result);
                string content1 = theReader1.ReadToEnd();
                XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(content1, rootName);
                XDocument xDoc = XDocument.Load(new XmlNodeReader(doc));
                StringReader theReader12 = new StringReader(xDoc.ToString());
                ds.ReadXml(theReader12);

            }
            catch (Exception ex)
            {

            }
            return ds;
        }

    }

}
