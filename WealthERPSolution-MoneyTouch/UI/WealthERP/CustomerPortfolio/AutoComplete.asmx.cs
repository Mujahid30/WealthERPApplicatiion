using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using VoUser;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using VoProductMaster;
using BoProductMaster;
using BoCustomerProfiling;
using WealthERP.Base;
using BoCustomerRiskProfiling;
using System.Xml.Linq;
using System.Web.Script.Services;
using System.Xml;

namespace WealthERP.CustomerPortfolio
{
    /// <summary>
    /// Summary description for AutoComplete1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AutoComplete1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string[] GetSchemeList(string prefixText)
        {

            ProductMFBo productMFBo = new ProductMFBo();
            DataTable dtSchemePlans;
            List<string> names = new List<string>();
            dtSchemePlans = productMFBo.GetSchemePlanPrefix(prefixText);


            foreach (DataRow dr in dtSchemePlans.Rows)
            {
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["PASP_SchemePlanName"].ToString(), dr["PASP_SchemePlanCode"].ToString());
                names.Add(item);

            }
            return names.ToArray();
        }

        [WebMethod]
        public string[] GetSwitchSchemeList(string prefixText, string contextKey)
        {

            ProductMFBo productMFBo = new ProductMFBo();
            DataTable dtSchemePlans;
            List<string> names = new List<string>();
            dtSchemePlans = productMFBo.GetSwitchSchemePlanPrefix(prefixText, int.Parse(contextKey));


            foreach (DataRow dr in dtSchemePlans.Rows)
            {
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["PASP_SchemePlanName"].ToString(), dr["PASP_SchemePlanCode"].ToString());
                names.Add(item);

            }
            return names.ToArray();
        }

        
        [WebMethod]
        public string[] GetScripList(string prefixText)
        {

            ProductEquityBo productEquityBo = new ProductEquityBo();
            DataTable dtEquityScrips = new DataTable();
            int i = 0;

            dtEquityScrips = productEquityBo.GetEquityScripPrefix(prefixText);
            string[] equityScripList = new string[dtEquityScrips.Rows.Count];

            foreach (DataRow dr in dtEquityScrips.Rows)
            {

                equityScripList[i] = dr["PEM_CompanyName"].ToString();
                i++;
            }
            return equityScripList;


        }

        [WebMethod]
        public string[] GetParentCustomerName(string prefixText,int count,string contextKey)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtCustomerName = new DataTable();
            int i = 0;
           List<string> names = new List<string>();

            dtCustomerName = customerBo.GetParentCustomerName(prefixText,int.Parse(contextKey));
            //string[] customerNameList = new string[dtCustomerName.Rows.Count];

            foreach (DataRow dr in dtCustomerName.Rows)
            {

                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(),dr["C_CustomerId"].ToString());
                names.Add(item);

                //customerNameList[i] = dr["C_FirstName"].ToString() + "|" + dr["C_PANNum"].ToString();
                //i++;
            }
            return names.ToArray();
        }

        [WebMethod]
        public string[] GetMemberCustomerName(string prefixText, int count, string contextKey)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtCustomerName = new DataTable();
            int i = 0;
            List<string> names = new List<string>();

            dtCustomerName = customerBo.GetMemberCustomerName(prefixText, int.Parse(contextKey));
            //string[] customerNameList = new string[dtCustomerName.Rows.Count];

            foreach (DataRow dr in dtCustomerName.Rows)
            {

                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(), dr["C_CustomerId"].ToString());
                names.Add(item);

                //customerNameList[i] = dr["C_FirstName"].ToString() + "|" + dr["C_PANNum"].ToString();
                //i++;
            }
            return names.ToArray();
        }
        [WebMethod]
        public string[] GetAdviserParentCustomerName(string prefixText,string contextKey)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtCustomerName = new DataTable();
            int i = 0;
            List<string> names = new List<string>();

            dtCustomerName = customerBo.GetAdviserParentCustomerName(prefixText, int.Parse(contextKey));
            //string[] customerNameList = new string[dtCustomerName.Rows.Count];

            foreach (DataRow dr in dtCustomerName.Rows)
            {

                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(), dr["C_CustomerId"].ToString());
                names.Add(item);
            }
            return names.ToArray();
        }
        [WebMethod]
        public string[] GetCustomerName(string prefixText, int count, string contextKey)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtCustomerName = new DataTable();
            int i = 0;
            List<string> names = new List<string>();

            dtCustomerName = customerBo.GetCustomerName(prefixText, int.Parse(contextKey));
            //string[] customerNameList = new string[dtCustomerName.Rows.Count];

            foreach (DataRow dr in dtCustomerName.Rows)
            {

                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(), dr["C_CustomerId"].ToString());
                names.Add(item);
            }
            return names.ToArray();
        }

        [WebMethod]
        public string[] GetAdviserCustomerName(string prefixText, int count, string contextKey)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtCustomerName = new DataTable();
            int i = 0;
            List<string> names = new List<string>();

            dtCustomerName = customerBo.GetAdviserCustomerName(prefixText, int.Parse(contextKey));
            //string[] customerNameList = new string[dtCustomerName.Rows.Count];

            foreach (DataRow dr in dtCustomerName.Rows)
            {

                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(), dr["C_CustomerId"].ToString());
                names.Add(item);
            }
            return names.ToArray();
        }
        /// <summary>
        /// For POC of FlexiGrid this Function is Created
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(UseHttpGet = false,XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public XmlDocument XmlData()
        {
            int page = 1;
            int total = 4;
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("rows",
                new XElement("page", page.ToString()),
                new XElement("total", total.ToString(),
                    new XElement("row", new XAttribute("id", "111"),
                        new XElement("cell", "111"),
                        new XElement("cell", "row1"),
                        new XElement("cell", "rowDescription1"),
                        new XElement("cell", "rowUnit1"),
                        new XElement("cell", "rowUnitPrice1"),
                        new XElement("cell", DateTime.Now.ToShortDateString())
        ),
        new XElement("row", new XAttribute("id", "222"),
                        new XElement("cell", "222"),
                        new XElement("cell", "row2"),
                        new XElement("cell", "rowDescription2"),
                        new XElement("cell", "rowUnit2"),
                        new XElement("cell", "rowUnitPrice2"),
                        new XElement("cell", DateTime.Now.ToShortDateString())
        ),
         new XElement("row", new XAttribute("id", "333"),
                        new XElement("cell", "333"),
                        new XElement("cell", "row3"),
                        new XElement("cell", "rowDescription3"),
                        new XElement("cell", "rowUnit3"),
                        new XElement("cell", "rowUnitPrice3"),
                        new XElement("cell", DateTime.Now.ToShortDateString())
        ),
           new XElement("row", new XAttribute("id", "444"),
                        new XElement("cell", "444"),
                        new XElement("cell", "row4"),
                        new XElement("cell", "rowDescription4"),
                        new XElement("cell", "rowUnit4"),
                        new XElement("cell", "rowUnitPrice4"),
                        new XElement("cell", DateTime.Now.ToShortDateString())
        )
                                           )
                                )
        );

            XmlDocument newDoc = new XmlDocument();
            newDoc.LoadXml(xmlDoc.ToString());
            return newDoc;
        }
    }

}
