using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Configuration;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Xml;


namespace CMOTJOB
{
    class CMOTJOBDao
    {

        public DataSet GetDataFromAPI()
        {
            int i = 0;
            DataSet ds = new DataSet();
            DataTable dtRatingScheme = new DataTable();
            DataTable dtManagerRating = new DataTable();
            DataTable dtSchemeCode = new DataTable();
            DataTable dtSchemeDetailsful = new DataTable();
            dtSchemeCode = GetCMOTCodeForAllSchemes();
            DataTable dtSchemeDetails = new DataTable();
            //string SchemeDetailsAPI = ConfigurationSettings.AppSettings["AllSchemeInformation"];
            //dtSchemeDetails = GetIndividualSchemeDataFromAPI(SchemeDetailsAPI);
            foreach (DataRow dr in dtSchemeCode.Rows)
            {
             
                try
                {

                    string strCMOTcode = dr["PASC_AMC_ExternalCode"].ToString();
                    ////string ratingAPI = ConfigurationSettings.AppSettings["Rating"] + strCMOTcode;
                    ////string managersAPI = ConfigurationSettings.AppSettings["FundManager"] + strCMOTcode + "/Pref";
                    //string SchemeDetailsAPI = ConfigurationSettings.AppSettings["AllSchemeInformation"] ;
                    ////DataTable dtIndividualSchemeRating = GetIndividualSchemeDataFromAPI(ratingAPI);
                    ////DataTable dtIndividualManagersRating = GetIndividualSchemeDataFromAPI(managersAPI);
                    ////dtRatingScheme.Merge(dtIndividualSchemeRating);
                    ////dtManagerRating.Merge(dtIndividualManagersRating);
                    string SchemeDetailsAPI = ConfigurationSettings.AppSettings["AllSchemeInformation"] + strCMOTcode;
                    dtSchemeDetails = GetIndividualSchemeDataFromAPI(SchemeDetailsAPI);
                    dtSchemeDetailsful.Merge(dtSchemeDetails);
                    i++;
                    Console.WriteLine("SchemeCode :{0}, Interation No:{1}", strCMOTcode, i);
                    if (i == 10000)
                    {

                    }

                }
                catch (Exception ex)
                {

                }

            }
            //ds.Tables.Add(dtRatingScheme);
            //ds.Tables.Add(dtManagerRating);
            ds.Tables.Add(dtSchemeDetailsful);
            return ds;
        }

        public DataTable GetCMOTCodeForAllSchemes()
        {
            DataTable dtCMOTCode = new DataTable();
            Database db;
            DbCommand GetCMOTCodeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetCMOTCodeCmd = db.GetStoredProcCommand("SPROC_ONL_GetProductMFSchemeExternalCode");
                dtCMOTCode = db.ExecuteDataSet(GetCMOTCodeCmd).Tables[0];
            }

            catch (Exception Ex)
            {

            }
            return dtCMOTCode;
        }
        public DataTable GetIndividualSchemeDataFromAPI(string APIUrl)
        {

            WebResponse response;
            String result, convertedXML ;
            WebRequest request = HttpWebRequest.Create(APIUrl);
            DataTable dtResult = new DataTable();
            response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
                reader.Close();
            }
            if (result.StartsWith("{"))
            {
                XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(result);
                convertedXML = GetXmlString(doc);
                result = convertedXML;
                
            }
            else if (result.StartsWith("<"))
            {
              dtResult =GetDataTableXML(result);
            }
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            StringReader theReader = new StringReader(result);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);
            return theDataSet.Tables[3];
        }
        private DataTable GetDataTableXML(string xmlstring)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            StringReader theReader = new StringReader(xmlstring);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);
            return theDataSet.Tables[3];
        }
        public int UpdateMarketResearchData(DataTable dtSchemeDetails)
        {
            int result = 0;
            try
            {

                string conString = ConfigurationSettings.AppSettings["wealtherp"];
                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();
                SqlCommand cmdProc = new SqlCommand("SPROC_ONL_ProductCMOTSchemeDetailsUpdate", sqlCon);
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@Details", dtSchemeDetails);
                result = cmdProc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            return result;

        }
        public int updateSchemeDetails(DataTable dtschemeDetails)
        {
            int result = 0;
            try
            {

                string conString = ConfigurationSettings.AppSettings["wealtherp"];
                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();
                SqlCommand cmdProc = new SqlCommand("SPROC_UpdateSchemeDetailsFromCMOT", sqlCon);
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@TTSchemeDetails", dtschemeDetails);
                result = cmdProc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
           // C# Function to Convert Json string to C# Datatable
    protected DataTable ConvertJSONToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        //strip out bad characters
        string[] jsonParts = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");

        //hold column names
        List<string> dtColumns = new List<string>();

        //get columns
        foreach (string jp in jsonParts)
        {
            //only loop thru once to get column names
            string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1);
                    string v = rowData.Substring(idx + 1);
                    if (!dtColumns.Contains(n))
                    {
                        dtColumns.Add(n.Replace("\"", ""));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error Parsing Column Name : {0}", rowData));
                }

            }
            break; // TODO: might not be correct. Was : Exit For
        }

        //build dt
        foreach (string c in dtColumns)
        {
            dt.Columns.Add(c);
        }
        //get table data
        foreach (string jp in jsonParts)
        {
            string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string v = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[n] = v;
                }
                catch (Exception ex)
                {
                    continue;
                }

            }
            dt.Rows.Add(nr);
        }
        return dt;
    }
  
        private string GetXmlString(XmlDocument xmlDoc)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xw.Formatting = System.Xml.Formatting.Indented;
            xmlDoc.WriteTo(xw);
            return sw.ToString();
        }

}
    }

