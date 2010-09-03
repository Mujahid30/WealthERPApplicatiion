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
        /// <summary>
        /// Get RM Group Customer Names 
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get RM Group Customer Names 
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] GetParentCustomers(string prefixText, int count, string contextKey)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtCustomerName = new DataTable();
            int i = 0;
            List<string> names = new List<string>();

            dtCustomerName = customerBo.GetParentCustomers(prefixText, int.Parse(contextKey));
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
        /// <summary>
        /// Get RM Individual Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>
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


        /// <summary>
        /// No use
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Advisor Individual Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>
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
        /// Get Advisor Group Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] GetAdviserGroupCustomerName(string prefixText, int count, string contextKey)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtCustomerName = new DataTable();
            int i = 0;
            List<string> names = new List<string>();

            dtCustomerName = customerBo.GetAdviserGroupCustomerName(prefixText, int.Parse(contextKey));
            //string[] customerNameList = new string[dtCustomerName.Rows.Count];

            foreach (DataRow dr in dtCustomerName.Rows)
            {

                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(), dr["C_CustomerId"].ToString());
                names.Add(item);
            }
            return names.ToArray();
        }


    }

}
