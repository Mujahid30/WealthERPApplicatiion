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
        /// Get RM Individual Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] GetMemberCustomerNamesForGrouping(string prefixText, int count, string contextKey)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtCustomerName = new DataTable();
            int i = 0;
            int selectedParentId = 0;
            int rmId = 0;
            string[] splitStr = contextKey.Split('|');
            rmId = int.Parse(splitStr[0].ToString());
            selectedParentId = int.Parse(splitStr[1].ToString());
            List<string> names = new List<string>();

            dtCustomerName = customerBo.GetMemberCustomerNamesForGrouping(prefixText, selectedParentId, rmId);
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
        /// <summary>
        /// Get BM Parent Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] GetBMParentCustomerNames(string prefixText, int count, string contextKey)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtCustomerName = new DataTable();
            int i = 0;
            List<string> names = new List<string>();

            dtCustomerName = customerBo.GetBMParentCustomerNames(prefixText, int.Parse(contextKey));
            foreach (DataRow dr in dtCustomerName.Rows)
            {

                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(), dr["C_CustomerId"].ToString());
                names.Add(item);
            }
            return names.ToArray();
        }
        /// <summary>
        /// Get BM Individual Customer Name
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] GetBMIndividualCustomerNames(string prefixText, int count, string contextKey)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtCustomerName = new DataTable();
            int i = 0;
            List<string> names = new List<string>();

            dtCustomerName = customerBo.GetBMIndividualCustomerNames(prefixText, int.Parse(contextKey));
             foreach (DataRow dr in dtCustomerName.Rows)
            {

                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(), dr["C_CustomerId"].ToString());
                names.Add(item);
            }
            return names.ToArray();
        }

        [WebMethod]
        public string[] GetAllBranchAndRMIndividualCustomers(string contextKey, string prefixText)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtAllRMBranchIndividualCustomersName = new DataTable();
            List<string> allRMBranchNames = new List<string>();

            dtAllRMBranchIndividualCustomersName = customerBo.GetRMBranchIndividualCustomerNames(contextKey, prefixText);
            foreach (DataRow dr in dtAllRMBranchIndividualCustomersName.Rows)
            {
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(), dr["C_FirstName"].ToString());
                allRMBranchNames.Add(item);
            }
            return allRMBranchNames.ToArray();
        }

        [WebMethod]
        public string[] GetAllBranchAndRMGroupCustomers(string contextKey, string prefixText)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtAllRMBranchGroupCustomersName = new DataTable();
            List<string> allRMBranchNames = new List<string>();

            dtAllRMBranchGroupCustomersName = customerBo.GetRMBranchGroupCustomerNames(contextKey, prefixText);
            foreach (DataRow dr in dtAllRMBranchGroupCustomersName.Rows)
            {
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(), dr["C_FirstName"].ToString());
                allRMBranchNames.Add(item);
            }
            return allRMBranchNames.ToArray();
        }

        [WebMethod]
        public string[] GetPerticularBranchsAllIndividualCustomers(string contextKey, string prefixText)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtAllRMBranchGroupCustomersName = new DataTable();
            List<string> allRMBranchNames = new List<string>();

            dtAllRMBranchGroupCustomersName = customerBo.GetPerticularBranchsAllIndividualCustomers(contextKey, prefixText);
            foreach (DataRow dr in dtAllRMBranchGroupCustomersName.Rows)
            {
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(), dr["C_FirstName"].ToString());
                allRMBranchNames.Add(item);
            }
            return allRMBranchNames.ToArray();
        }

        [WebMethod]
        public string[] GetPerticularBranchsAllGroupCustomers(string contextKey, string prefixText)
        {
            CustomerBo customerBo = new CustomerBo();
            DataTable dtAllRMBranchGroupCustomersName = new DataTable();
            List<string> allRMBranchNames = new List<string>();

            dtAllRMBranchGroupCustomersName = customerBo.GetPerticularBranchsAllGroupCustomers(contextKey, prefixText);
            foreach (DataRow dr in dtAllRMBranchGroupCustomersName.Rows)
            {
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["C_FirstName"].ToString(), dr["C_FirstName"].ToString());
                allRMBranchNames.Add(item);
            }
            return allRMBranchNames.ToArray();
        }
    }

}
