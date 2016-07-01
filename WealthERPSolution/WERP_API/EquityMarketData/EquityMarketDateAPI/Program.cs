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
            EquityAPIBO EquityAPIBO = new EquityAPIBO();
            WebResponse response;
            string result;
            WebRequest request = HttpWebRequest.Create(ConfigurationSettings.AppSettings["EquityMarket"]+ DateTime.Now.ToString("yyyy-MM-dd"));
            response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
                reader.Close();
            }
            StringReader theReader = new StringReader(result);
            string content = theReader.ReadToEnd();
            XmlDocument doc1 = (XmlDocument)JsonConvert.DeserializeXmlNode(content, "EquityMarketPriceList");
            XDocument xDoc1 = XDocument.Load(new XmlNodeReader(doc1));
            StringReader theReader123 = new StringReader(xDoc1.ToString());
            DataSet ds1 = new DataSet();
            ds1.ReadXml(theReader123);

            EquityAPIBO.CreateUpdateEquityMarketData(ds1.Tables[0]);

            string result1;
            WebRequest request1 = HttpWebRequest.Create(ConfigurationSettings.AppSettings["EquityMaster"]);
            response = request1.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result1 = reader.ReadToEnd();
                reader.Close();
            }
            StringReader theReader1 = new StringReader(result1);
            string content1 = theReader1.ReadToEnd();
            XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(content1, "EquityScriptMasterList");
            XDocument xDoc = XDocument.Load(new XmlNodeReader(doc));
            StringReader theReader12 = new StringReader(xDoc.ToString());
            DataSet ds = new DataSet();
            ds.ReadXml(theReader12);
            EquityAPIBO.CreateUpdateEquityMasterData(ds.Tables[0]);

        }

    }

}
