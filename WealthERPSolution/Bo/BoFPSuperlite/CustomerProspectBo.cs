using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data.Common;
using VoFPSuperlite;
using DaoFPSuperlite;

namespace BoFPSuperlite
{
    public class CustomerProspectBo
    {

        public bool AddDetailsForCustomerProspect(int customerId, int userId, CustomerProspectVo customerprospectvo)
        {

            bool bTotalResult = true;
            try
            {
                CustomerProspectDao customerprospectdao = new CustomerProspectDao();

                customerprospectdao.AddDetailsForCustomerProspect(customerId, userId, customerprospectvo);

            }
            catch (Exception ex)
            {
                bTotalResult = false;
            }
            return bTotalResult;

        }


        public CustomerProspectVo GetDetailsForCustomerProspect(DataTable dtGetDetailsForCustomerProspect)
        {
            
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            CustomerProspectVo customerprospectvo = new CustomerProspectVo();

            try
            {
                if (dtGetDetailsForCustomerProspect != null && dtGetDetailsForCustomerProspect.Rows.Count > 0)
                {
                    customerprospectvo.TotalIncome = double.Parse(dtGetDetailsForCustomerProspect.Rows[0]["CFPS_Income"].ToString());
                    customerprospectvo.TotalExpense = double.Parse(dtGetDetailsForCustomerProspect.Rows[0]["CFPS_Expense"].ToString());
                    customerprospectvo.TotalLiabilities = double.Parse(dtGetDetailsForCustomerProspect.Rows[0]["CFPS_Liabilities"].ToString());
                    customerprospectvo.TotalAssets = double.Parse(dtGetDetailsForCustomerProspect.Rows[0]["CFPS_Asset"].ToString());
                    customerprospectvo.TotalGeneralInsurance = double.Parse(dtGetDetailsForCustomerProspect.Rows[0]["CFPS_GeneralInsurance"].ToString());
                    customerprospectvo.TotalLifeInsurance = double.Parse(dtGetDetailsForCustomerProspect.Rows[0]["CFPS_LifeInsurance"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerprospectvo;
        }

        /// <summary>
        /// Used to Add Income Details for Customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectIncomeDetailsVoList"></param>
        /// <returns></returns>
        public bool AddCustomerFPIncomeDetails(int customerId, int userId, List<CustomerProspectIncomeDetailsVo> customerProspectIncomeDetailsVoList, out double totalincome)
        {
            totalincome = 0.0;
            bool bIncomeResult = true;
            try
            {
                CustomerProspectDao customerprospectdao = new CustomerProspectDao();

                foreach (CustomerProspectIncomeDetailsVo cpid in customerProspectIncomeDetailsVoList)
                {
                        customerprospectdao.AddCustomerFPIncomeDetails(customerId, userId, cpid);
                        if(cpid.IncomeCategoryCode != 7)
                            totalincome += cpid.IncomeValue;
                }
            }
            catch (Exception ex)
            {
                bIncomeResult = false;
            }
            return bIncomeResult;

        }

        /// <summary>
        /// Used to get income details for Customer Prospect
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="totalIncome"></param>
        /// <returns></returns>
        public List<CustomerProspectIncomeDetailsVo> GetIncomeDetailsForCustomerProspect(DataTable dtCustomerProspectIncomeDetails)
        {           
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            CustomerProspectIncomeDetailsVo customerprospectincomedetailsvo;
            List<CustomerProspectIncomeDetailsVo> customerprospectincomedetailslist = new List<CustomerProspectIncomeDetailsVo>();
            try
            {
                if (dtCustomerProspectIncomeDetails != null && dtCustomerProspectIncomeDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCustomerProspectIncomeDetails.Rows.Count; i++)
                    {
                        customerprospectincomedetailsvo = new CustomerProspectIncomeDetailsVo();
                        customerprospectincomedetailsvo.IncomeDetailsId = int.Parse(dtCustomerProspectIncomeDetails.Rows[i]["CFPID_FPIncomeDetailsId"].ToString());
                        customerprospectincomedetailsvo.IncomeCategoryCode = int.Parse(dtCustomerProspectIncomeDetails.Rows[i]["XIC_IncomeCategoryCode"].ToString());
                        if (dtCustomerProspectIncomeDetails.Rows[i]["CFPID_Value"] != null && dtCustomerProspectIncomeDetails.Rows[i]["CFPID_Value"].ToString() != "")
                        {
                            customerprospectincomedetailsvo.IncomeValue = double.Parse(dtCustomerProspectIncomeDetails.Rows[i]["CFPID_Value"].ToString());
                        }
                        customerprospectincomedetailslist.Add(customerprospectincomedetailsvo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerprospectincomedetailslist;
        }

        /// <summary>
        /// Used to update details for Customer Prospect
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerProspectIncomeDetailsVoList"></param>
        /// <param name="totalIncome"></param>
        /// <returns></returns>
        public bool UpdateCustomerIncomeDetailsForCustomerProspect(int userId, int customerId, List<CustomerProspectIncomeDetailsVo> customerProspectIncomeDetailsVoList, out double totalIncome)
        {
            totalIncome = 0.0;
            bool bincomeresult = true;
            try
            {
                CustomerProspectDao customerprospectdao = new CustomerProspectDao();

                foreach (CustomerProspectIncomeDetailsVo cpid in customerProspectIncomeDetailsVoList)
                {
                    customerprospectdao.UpdateCustomerIncomeDetailsForCustomerProspect(customerId, userId, cpid);
                    totalIncome += cpid.IncomeValue;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bincomeresult;
        }

        /// <summary>
        /// Used to Add Customer FP Expense Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectExpenseeDetailsVoList"></param>
        /// <param name="totalExpense"></param>
        /// <returns></returns>
        public bool AddCustomerFPExpenseDetails(int customerId, int userId, List<CustomerProspectExpenseDetailsVo> customerProspectExpenseeDetailsVoList, out double totalExpense)
        {
            totalExpense = 0.0;
            bool bExpenseResult = true;
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            try
            {
                foreach (CustomerProspectExpenseDetailsVo cevo in customerProspectExpenseeDetailsVoList)
                {
                    customerprospectdao.AddCustomerFPExpenseDetails(customerId, userId, cevo);
                    totalExpense += cevo.ExpenseValue;
                }
            }
            catch (Exception ex)
            {
                bExpenseResult = false;
            }
            return bExpenseResult;
        }

        /// <summary>
        /// Used to Get Expense Details about customers
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="totalExpense"></param>
        /// <returns></returns>
        public List<CustomerProspectExpenseDetailsVo> GetExpenseDetailsForCustomerProspect(DataTable dtCustomerProspectExpenseDetails)
        {

            
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            CustomerProspectExpenseDetailsVo customerprospectexpensedetailsvo;
            List<CustomerProspectExpenseDetailsVo> customerprospectexpensedetailslist = new List<CustomerProspectExpenseDetailsVo>();
            try
            {
                if (dtCustomerProspectExpenseDetails != null && dtCustomerProspectExpenseDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCustomerProspectExpenseDetails.Rows.Count; i++)
                    {
                        customerprospectexpensedetailsvo = new CustomerProspectExpenseDetailsVo();
                        customerprospectexpensedetailsvo.ExpenseDetailsId = int.Parse(dtCustomerProspectExpenseDetails.Rows[i]["CFPED_FPExpenseDetailsId"].ToString());
                        customerprospectexpensedetailsvo.ExpenseCategoryCode = int.Parse(dtCustomerProspectExpenseDetails.Rows[i]["XEC_ExpenseCategoryCode"].ToString());
                        if (dtCustomerProspectExpenseDetails.Rows[i]["CFPED_Value"] != null && dtCustomerProspectExpenseDetails.Rows[i]["CFPED_Value"].ToString() != "")
                        {
                            customerprospectexpensedetailsvo.ExpenseValue = double.Parse(dtCustomerProspectExpenseDetails.Rows[i]["CFPED_Value"].ToString());
                        }
                        customerprospectexpensedetailslist.Add(customerprospectexpensedetailsvo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerprospectexpensedetailslist;
        }

        /// <summary>
        /// Used to Update Customer Expense for Customer Prospect
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerProspectExpenseDetailsVoList"></param>
        /// <param name="totalExpense"></param>
        /// <returns></returns>
        public bool UpdateCustomerExpenseDetailsForCustomerProspect(int userId, int customerId, List<CustomerProspectExpenseDetailsVo> customerProspectExpenseDetailsVoList, out double totalExpense)
        {
            totalExpense = 0.0;
            bool bexpenseresult = true;
            try
            {
                CustomerProspectDao customerprospectdao = new CustomerProspectDao();

                foreach (CustomerProspectExpenseDetailsVo cped in customerProspectExpenseDetailsVoList)
                {
                    customerprospectdao.UpdateCustomerExpenseDetailsForCustomerProspect(userId, customerId, cped);
                    totalExpense += cped.ExpenseValue;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bexpenseresult;
        }

        /// <summary>
        /// Used to Add Liabilities for Customer Prospect
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectLiabilitiesDetailsVoList"></param>
        /// <param name="totalLiabilities"></param>
        /// <param name="totalLoanOutstanding"></param>
        /// <returns></returns>
        public bool AddLiabilitiesDetailsForCustomerProspect(int customerId, int userId, List<CustomerProspectLiabilitiesDetailsVo> customerProspectLiabilitiesDetailsVoList, out double totalLiabilities, out double totalLoanOutstanding)
        {
            totalLiabilities = 0.0;
            totalLoanOutstanding = 0.0;
            bool bLiabilitiesResult = true;
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            try
            {
                foreach (CustomerProspectLiabilitiesDetailsVo cpld in customerProspectLiabilitiesDetailsVoList)
                {
                    customerprospectdao.AddLiabilitiesDetailsForCustomerProspect(customerId, userId, cpld);
                    totalLiabilities += cpld.LoanOutstanding;
                }
            }
            catch (Exception ex)
            {
                bLiabilitiesResult = false;
            }
            return bLiabilitiesResult;
        }

        /// <summary>
        /// Used to Get Liabilties for Customer Prospect        
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="totalLoanOutstanding"></param>
        /// <param name="totalLiabilities"></param>
        /// <returns></returns>
        public List<CustomerProspectLiabilitiesDetailsVo> GetLiabilitiesDetailsForCustomerProspect(DataTable dtCustomerLiabilitiesDetails)
        {           
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            CustomerProspectLiabilitiesDetailsVo customerprospectliabilitiesetailsvo;
            List<CustomerProspectLiabilitiesDetailsVo> customerprospectliabilitiesdetailslist = new List<CustomerProspectLiabilitiesDetailsVo>();
            try
            {
                if (dtCustomerLiabilitiesDetails != null && dtCustomerLiabilitiesDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCustomerLiabilitiesDetails.Rows.Count; i++)
                    {
                        customerprospectliabilitiesetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                        customerprospectliabilitiesetailsvo.LiabilitiesDetailsId = int.Parse(dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_FPLiabilitiesDetailsId"].ToString());

                        if (dtCustomerLiabilitiesDetails.Rows[i]["XLT_LoanTypeCode"].ToString() != null)
                        {

                            //Adjusted Loan
                            if (dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_AdjustedLoanOutstanding"].ToString() != string.Empty && dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_AdjustedLoanOutstanding"] != null)
                            {
                                customerprospectliabilitiesetailsvo.AdjustedLoan = double.Parse(dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_AdjustedLoanOutstanding"].ToString());
                            }
                            //Total Value
                            if (dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_TotalLoanOutstanding"].ToString() != string.Empty && dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_TotalLoanOutstanding"] != null)
                            {
                                customerprospectliabilitiesetailsvo.LoanOutstanding = double.Parse(dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_TotalLoanOutstanding"].ToString());
                            }

                            // EMI Value
                            if (dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_EMIAmount"] != null && dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_EMIAmount"].ToString() != "")
                            {
                                customerprospectliabilitiesetailsvo.EMIAmount = double.Parse(dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_EMIAmount"].ToString());
                            }
                            // Adjusted EMI 
                            if (dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_AdjustedEMIAmount"].ToString() != string.Empty && dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_AdjustedEMIAmount"] != null)
                            {
                                customerprospectliabilitiesetailsvo.AdjustedEMIAmount = double.Parse(dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_AdjustedEMIAmount"].ToString());
                            }

                            // Total EMI 
                            if (dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_TotalEMIAmount"].ToString() != string.Empty && dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_TotalEMIAmount"] != null)
                            {
                                customerprospectliabilitiesetailsvo.TotalEMIAmount = double.Parse(dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_TotalEMIAmount"].ToString());
                            }
                            
                            customerprospectliabilitiesetailsvo.LoanTypeCode = int.Parse(dtCustomerLiabilitiesDetails.Rows[i]["XLT_LoanTypeCode"].ToString());
                            if (dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_Tenure"] != null && dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_Tenure"].ToString() != "")
                            {
                                customerprospectliabilitiesetailsvo.Tenure = int.Parse(dtCustomerLiabilitiesDetails.Rows[i]["CFPLD_Tenure"].ToString());
                            }
                            
                            customerprospectliabilitiesdetailslist.Add(customerprospectliabilitiesetailsvo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerprospectliabilitiesdetailslist;
        }

        /// <summary>
        /// Update Customer Liabilites details for Customer Prospect
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerProspectLiabilitiesDetailsVoList"></param>
        /// <param name="totalLiabilities"></param>
        /// <param name="totalLoanOutstanding"></param>
        /// <returns></returns>
        public bool UpdateCustomerLiabilitiesDetailsForCustomerProspect(int userId, int customerId, List<CustomerProspectLiabilitiesDetailsVo> customerProspectLiabilitiesDetailsVoList, out double totalLiabilities, out double totalLoanOutstanding)
        {
            totalLiabilities = 0.0;
            totalLoanOutstanding = 0.0;
            bool bLiabilitiesResult = true;
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            try
            {
                foreach (CustomerProspectLiabilitiesDetailsVo cpld in customerProspectLiabilitiesDetailsVoList)
                {
                    customerprospectdao.UpdateCustomerLiabilitiesDetailsForCustomerProspect(userId, customerId, cpld);
                    totalLiabilities += cpld.LoanOutstanding;
                }
            }
            catch (Exception ex)
            {
                bLiabilitiesResult = false;
            }
            return bLiabilitiesResult;
        }

        /// <summary>
        /// Used to add customer FP Asset Sub Category Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectAssetSubDetailsVoList"></param>
        /// <param name="subInstrumentTotal"></param>
        /// <returns></returns>
        public bool AddCustomerFPAssetSubInstrumentDetails(int customerId, int userId, List<CustomerProspectAssetSubDetailsVo> customerProspectAssetSubDetailsVoList, out double subInstrumentTotal)
        {
            subInstrumentTotal = 0.0;

            bool bSubInstrumentResult = true;
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            try
            {
                foreach (CustomerProspectAssetSubDetailsVo cpasd in customerProspectAssetSubDetailsVoList)
                {
                    customerprospectdao.AddCustomerFPAssetSubInstrumentDetails(customerId, userId, cpasd);
                    subInstrumentTotal += cpasd.Value;
                }
            }
            catch (Exception ex)
            {
                bSubInstrumentResult = false;
            }
            return bSubInstrumentResult;
        }

        /// <summary>
        /// Used to Get Customer FP Asset Sub Instrument Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="totalAssetSubDetails"></param>
        /// <returns></returns>
        public List<CustomerProspectAssetSubDetailsVo> GetCustomerFPAssetSubInstrumentDetails(DataTable dtCustomerAssetSubInstrumentDetails)
        {
            
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            CustomerProspectAssetSubDetailsVo customerprospectassetsubdetailsvo;
            List<CustomerProspectAssetSubDetailsVo> customerprospectassetsubdetailsvolist = new List<CustomerProspectAssetSubDetailsVo>();
            try
            {
                if (dtCustomerAssetSubInstrumentDetails != null && dtCustomerAssetSubInstrumentDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCustomerAssetSubInstrumentDetails.Rows.Count; i++)
                    {
                        customerprospectassetsubdetailsvo = new CustomerProspectAssetSubDetailsVo();
                        customerprospectassetsubdetailsvo.SubInstrumentDetailsId = int.Parse(dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_FPSubInstrumentDetailsId"].ToString());
                        customerprospectassetsubdetailsvo.AssetGroupCode = dtCustomerAssetSubInstrumentDetails.Rows[i]["PAG_AssetGroupCode"].ToString();
                        customerprospectassetsubdetailsvo.AssetInstrumentCategoryCode = dtCustomerAssetSubInstrumentDetails.Rows[i]["PAIC_AssetInstrumentCategoryCode"].ToString();
                        customerprospectassetsubdetailsvo.AssetInstrumentSubCategoryCode = dtCustomerAssetSubInstrumentDetails.Rows[i]["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                        if (dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_AdjustedValue"] != null && dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_AdjustedValue"].ToString() != "")
                        {
                            customerprospectassetsubdetailsvo.AdjustedValue = double.Parse(dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_AdjustedValue"].ToString());
                        }
                        if (dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_TotalValue"] != null && dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_TotalValue"].ToString() != "")
                        {
                            customerprospectassetsubdetailsvo.Value = double.Parse(dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_TotalValue"].ToString());
                        }
                        if (dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_MaturityDate"] != null && dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_MaturityDate"].ToString() != "")
                        {
                            customerprospectassetsubdetailsvo.MaturityDate = DateTime.Parse(dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_MaturityDate"].ToString());
                        }
                        if (dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_Premium"] != null && dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_Premium"].ToString() != "")
                        {
                            customerprospectassetsubdetailsvo.Premium = double.Parse(dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_Premium"].ToString());
                        }
                        if (dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_AdjustedPremium"] != null && dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_AdjustedPremium"].ToString() != "")
                        {
                            customerprospectassetsubdetailsvo.AdjustedPremium = double.Parse(dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_AdjustedPremium"].ToString());
                        }
                        if (dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_TotalPremiumValue"] != null && dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_TotalPremiumValue"].ToString() != "")
                        {
                            customerprospectassetsubdetailsvo.TotalPremiumValue = double.Parse(dtCustomerAssetSubInstrumentDetails.Rows[i]["CFPASID_TotalPremiumValue"].ToString());
                        }
                        customerprospectassetsubdetailsvolist.Add(customerprospectassetsubdetailsvo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerprospectassetsubdetailsvolist;
        }

        /// <summary>
        /// Used to Update FP Asset Sub Instrument Category Details
        /// </summary>
        /// 
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectAssetSubDetailsVoList"></param>
        /// <param name="subInstrumentTotal"></param>
        /// <returns></returns>
        public bool UpdateCustomerFPAssetSubInstrumentDetails(int customerId, int userId, List<CustomerProspectAssetSubDetailsVo> customerProspectAssetSubDetailsVoList, out double subInstrumentTotal)
        {
            subInstrumentTotal = 0.0;

            bool bSubInstrumentResult = true;
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            try
            {
                foreach (CustomerProspectAssetSubDetailsVo cpasd in customerProspectAssetSubDetailsVoList)
                {
                    customerprospectdao.UpdateCustomerFPAssetSubInstrumentDetails(customerId, userId, cpasd);
                    subInstrumentTotal += cpasd.Value;
                }
            }
            catch (Exception ex)
            {
                bSubInstrumentResult = false;
            }
            return bSubInstrumentResult;
        }

        /// <summary>
        /// Used to Add Customer FP Asset Instrument Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectAssetDetailsVoList"></param>
        /// <param name="instrumentTotal"></param>
        /// <returns></returns>
        public bool AddCustomerFPAssetInstrumentDetails(int customerId, int userId, List<CustomerProspectAssetDetailsVo> customerProspectAssetDetailsVoList, out double instrumentTotal)
        {
            instrumentTotal = 0.0;

            bool bInstrumentResult = true;
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            try
            {
                foreach (CustomerProspectAssetDetailsVo cpad in customerProspectAssetDetailsVoList)
                {
                    customerprospectdao.AddCustomerFPAssetInstrumentDetails(customerId, userId, cpad);
                    instrumentTotal += cpad.Value;
                }
            }
            catch (Exception ex)
            {
                bInstrumentResult = false;
            }
            return bInstrumentResult;
        }

        /// <summary>
        /// Used to Get Customer FP Asset Instrument Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="totalAssetDetails"></param>
        /// <returns></returns>
        public List<CustomerProspectAssetDetailsVo> GetCustomerFPAssetInstrumentDetails(DataTable dtCustomerAssetInstrumentDetails)
        {            
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            CustomerProspectAssetDetailsVo customerprospectassetdetailsvo;
            List<CustomerProspectAssetDetailsVo> customerprospectassetdetailsvolist = new List<CustomerProspectAssetDetailsVo>();
            try
            {
                for (int i = 0; i < dtCustomerAssetInstrumentDetails.Rows.Count; i++)
                {
                    customerprospectassetdetailsvo = new CustomerProspectAssetDetailsVo();
                    customerprospectassetdetailsvo.InstrumentDetailsId = int.Parse(dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_FPInstrumentDetailsId"].ToString());
                    customerprospectassetdetailsvo.AssetGroupCode = dtCustomerAssetInstrumentDetails.Rows[i]["PAG_AssetGroupCode"].ToString();
                    customerprospectassetdetailsvo.AssetInstrumentCategoryCode = dtCustomerAssetInstrumentDetails.Rows[i]["PAIC_AssetInstrumentCategoryCode"].ToString();
                    if (dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_AdjustedValue"] != null && dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_AdjustedValue"].ToString() != "")
                    {
                        customerprospectassetdetailsvo.AdjustedValue = double.Parse(dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_AdjustedValue"].ToString());
                    }
                    if (dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_TotalValue"] != null && dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_TotalValue"].ToString() != "")
                    {
                        customerprospectassetdetailsvo.Value = double.Parse(dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_TotalValue"].ToString());
                    }
                    if (dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_MaturityDate"] != null && dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_MaturityDate"].ToString() != "")
                    {
                        customerprospectassetdetailsvo.MaturityDate = DateTime.Parse(dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_MaturityDate"].ToString());
                    }
                    if (dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_Premium"] != null && dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_Premium"].ToString() != "")
                    {
                        customerprospectassetdetailsvo.Premium = double.Parse(dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_Premium"].ToString());
                    }
                    if (dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_AdjustedPremium"] != null && dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_AdjustedPremium"].ToString() != "")
                    {
                        customerprospectassetdetailsvo.AdjustedPremium = double.Parse(dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_AdjustedPremium"].ToString());
                    }
                    if (dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_TotalPremiumValue"] != null && dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_TotalPremiumValue"].ToString() != "")
                    {
                        customerprospectassetdetailsvo.TotalPremiumValue = double.Parse(dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_TotalPremiumValue"].ToString());
                    }
                    if (dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_SurrenderMarketValue"] != null && dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_SurrenderMarketValue"].ToString() != "")
                    {
                        customerprospectassetdetailsvo.SurrMktVal = double.Parse(dtCustomerAssetInstrumentDetails.Rows[i]["CFPAID_SurrenderMarketValue"].ToString());
                    }
                    customerprospectassetdetailsvolist.Add(customerprospectassetdetailsvo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerprospectassetdetailsvolist;
        }

        /// <summary>
        /// Used to Update Custoemr FP Asset Instrument Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectAssetDetailsVoList"></param>
        /// <param name="instrumentTotal"></param>
        /// <returns></returns>
        public bool UpdateCustomerFPAssetInstrumentDetails(int customerId, int userId, List<CustomerProspectAssetDetailsVo> customerProspectAssetDetailsVoList, out double instrumentTotal)
        {
            instrumentTotal = 0.0;

            bool bInstrumentResult = true;
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            try
            {
                foreach (CustomerProspectAssetDetailsVo cpad in customerProspectAssetDetailsVoList)
                {
                    customerprospectdao.UpdateCustomerFPAssetInstrumentDetails(customerId, userId, cpad);
                    instrumentTotal += cpad.Value;
                }
            }
            catch (Exception ex)
            {
                bInstrumentResult = false;
            }
            return bInstrumentResult;
        }


        /// <summary>
        /// Used to Add Customer FP Asset Group Details. First level of Category
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectAssetDetailsVoList"></param>
        /// <param name="instrumentTotal"></param>
        /// <returns></returns>
        public bool AddCustomerFPAssetGroupDetails(int customerId, int userId, List<CustomerProspectAssetGroupDetails> customerProspectGroupDetailsList, out double grouptotal)
        {
            grouptotal = 0.0;

            bool bGroupResult = true;
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            try
            {
                foreach (CustomerProspectAssetGroupDetails cpagd in customerProspectGroupDetailsList)
                {

                    customerprospectdao.AddCustomerFPAssetGroupDetails(customerId, userId, cpagd);
                    if (cpagd.AssetGroupCode != "MF")
                    {
                        grouptotal += cpagd.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                bGroupResult = false;
            }
            return bGroupResult;
        }

        /// <summary>
        /// Used to Get Customer FP Asset Instrument Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="totalAssetDetails"></param>
        /// <returns></returns>
        public List<CustomerProspectAssetGroupDetails> GetCustomerFPAssetGroupDetails(DataTable dtCustomerAssetGroupDetails)
        {

            
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            CustomerProspectAssetGroupDetails customerprospectassetgroupdetails;
            List<CustomerProspectAssetGroupDetails> customerprospectassetgroupdetailslist = new List<CustomerProspectAssetGroupDetails>();
            try
            {
                if (dtCustomerAssetGroupDetails != null && dtCustomerAssetGroupDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCustomerAssetGroupDetails.Rows.Count; i++)
                    {
                        customerprospectassetgroupdetails = new CustomerProspectAssetGroupDetails();
                        customerprospectassetgroupdetails.AssetGroupId = int.Parse(dtCustomerAssetGroupDetails.Rows[i]["CFPAGD_FPAssetGroupDetailsId"].ToString());
                        customerprospectassetgroupdetails.AssetGroupCode = dtCustomerAssetGroupDetails.Rows[i]["PAG_AssetGroupCode"].ToString();
                        if (dtCustomerAssetGroupDetails.Rows[i]["CFPAGD_AdjustedValue"] != null && dtCustomerAssetGroupDetails.Rows[i]["CFPAGD_AdjustedValue"].ToString() != "")
                        {
                            customerprospectassetgroupdetails.AdjustedValue = double.Parse(dtCustomerAssetGroupDetails.Rows[i]["CFPAGD_AdjustedValue"].ToString());
                        }
                        if (dtCustomerAssetGroupDetails.Rows[i]["CFPAGD_TotalValue"] != null && dtCustomerAssetGroupDetails.Rows[i]["CFPAGD_TotalValue"].ToString() != "")
                        {
                            customerprospectassetgroupdetails.Value = double.Parse(dtCustomerAssetGroupDetails.Rows[i]["CFPAGD_TotalValue"].ToString());
                        }
                        if (dtCustomerAssetGroupDetails.Rows[i]["CFPAGD_PremiumTotalValue"] != null && dtCustomerAssetGroupDetails.Rows[i]["CFPAGD_PremiumTotalValue"].ToString() != "")
                        {
                            customerprospectassetgroupdetails.TotalPremiumValue = double.Parse(dtCustomerAssetGroupDetails.Rows[i]["CFPAGD_PremiumTotalValue"].ToString());
                        }
                        customerprospectassetgroupdetailslist.Add(customerprospectassetgroupdetails);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerprospectassetgroupdetailslist;
        }

        public void GetAllDetailsForCustomerProspect(int CustomerId,
            out List<CustomerProspectIncomeDetailsVo> IncomeDetailsForCustomerProspectList,
            out List<CustomerProspectExpenseDetailsVo> ExpenseDetailsForCustomerProspectList,
           out List<CustomerProspectLiabilitiesDetailsVo> LiabilitiesDetailsForCustomerProspectList,
            out List<CustomerProspectAssetGroupDetails> CustomerFPAssetGroupDetailsList,
            out List<CustomerProspectAssetDetailsVo> CustomerFPAssetInstrumentDetailsList,
            out List<CustomerProspectAssetSubDetailsVo> CustomerFPAssetSubInstrumentDetailsList)
        {
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            DataSet dsGetAllDetailsForCustomerProspect = customerprospectdao.GetAllDetailsForCustomerProspect(CustomerId);
            IncomeDetailsForCustomerProspectList = GetIncomeDetailsForCustomerProspect(dsGetAllDetailsForCustomerProspect.Tables[0]);
            ExpenseDetailsForCustomerProspectList = GetExpenseDetailsForCustomerProspect(dsGetAllDetailsForCustomerProspect.Tables[1]);
            LiabilitiesDetailsForCustomerProspectList = GetLiabilitiesDetailsForCustomerProspect(dsGetAllDetailsForCustomerProspect.Tables[2]);
            CustomerFPAssetGroupDetailsList = GetCustomerFPAssetGroupDetails(dsGetAllDetailsForCustomerProspect.Tables[3]);
            CustomerFPAssetInstrumentDetailsList = GetCustomerFPAssetInstrumentDetails(dsGetAllDetailsForCustomerProspect.Tables[4]);
            CustomerFPAssetSubInstrumentDetailsList = GetCustomerFPAssetSubInstrumentDetails(dsGetAllDetailsForCustomerProspect.Tables[5]);
            GetDetailsForCustomerProspect(dsGetAllDetailsForCustomerProspect.Tables[6]);
        }

        /// <summary>
        /// It will seperate Code from the Dictionary and send it to the Business object for Insertion
        /// </summary>
        /// <param name="DataCapture"></param>
        /// <param name="customerId"></param>
        /// <param name="createdById"></param>
        /// <param name="totalincome"></param>
        /// <param name="totalExpense"></param>
        /// <param name="totalLiabilities"></param>
        /// <param name="totalLoanOutstanding"></param>
        /// <param name="instrumentTotal"></param>
        /// <param name="subInstrumentTotal"></param>
        /// <returns></returns>
        public bool DataManipulationInput(Dictionary<string, object> DataCapture, int customerId, int createdById, out double totalincome, out double totalExpense, out double totalLiabilities, out double totalLoanOutstanding
            , out double instrumentTotal, out double subInstrumentTotal, out double groupTotal)
        {
            bool statusMessage = true;
            totalincome = 0.0;
            totalExpense = 0.0;
            totalLiabilities = 0.0;
            totalLoanOutstanding = 0.0;
            instrumentTotal = 0.0;
            subInstrumentTotal = 0.0;
            groupTotal = 0.0;
            CustomerProspectVo customerprospectVo = new CustomerProspectVo();
            try
            {
                List<CustomerProspectIncomeDetailsVo> incomedetailsvolist = new List<CustomerProspectIncomeDetailsVo>();
                List<CustomerProspectExpenseDetailsVo> expensedetailsvolist = new List<CustomerProspectExpenseDetailsVo>();
                List<CustomerProspectLiabilitiesDetailsVo> liabilitiesdetailsvolist = new List<CustomerProspectLiabilitiesDetailsVo>();
                List<CustomerProspectAssetDetailsVo> assetdetailsvolist = new List<CustomerProspectAssetDetailsVo>();
                List<CustomerProspectAssetSubDetailsVo> assetsubdetailsvolist = new List<CustomerProspectAssetSubDetailsVo>();
                List<CustomerProspectAssetGroupDetails> assetgroupdetailslist = new List<CustomerProspectAssetGroupDetails>();
                CustomerProspectVo customerprospectvo = new CustomerProspectVo();
                if (((List<CustomerProspectIncomeDetailsVo>)DataCapture["IncomeList"]).Count != 0)
                {
                    incomedetailsvolist = DataCapture["IncomeList"] as List<CustomerProspectIncomeDetailsVo>;
                }
                if (((List<CustomerProspectExpenseDetailsVo>)DataCapture["ExpenseList"]).Count != 0)
                {
                    expensedetailsvolist = DataCapture["ExpenseList"] as List<CustomerProspectExpenseDetailsVo>;
                }
                if (((List<CustomerProspectLiabilitiesDetailsVo>)DataCapture["Liabilities"]).Count != 0)
                {
                    liabilitiesdetailsvolist = DataCapture["Liabilities"] as List<CustomerProspectLiabilitiesDetailsVo>;
                }
                if (((List<CustomerProspectAssetDetailsVo>)DataCapture["AssetDetails"]).Count != 0)
                {
                    assetdetailsvolist = DataCapture["AssetDetails"] as List<CustomerProspectAssetDetailsVo>;
                }
                if (((List<CustomerProspectAssetSubDetailsVo>)DataCapture["AssetSubDetails"]).Count != 0)
                {
                    assetsubdetailsvolist = DataCapture["AssetSubDetails"] as List<CustomerProspectAssetSubDetailsVo>;
                }
                if (((List<CustomerProspectAssetGroupDetails>)DataCapture["AssetGroupDetails"]).Count != 0)
                {
                    assetgroupdetailslist = DataCapture["AssetGroupDetails"] as List<CustomerProspectAssetGroupDetails>;
                }
                if (((CustomerProspectVo)DataCapture["TotalAssetDetails"]) != null)
                {
                    customerprospectvo = DataCapture["TotalAssetDetails"] as CustomerProspectVo;
                }
                //Deleting before insertion
                //DeleteDetailsForCustomerProspect(customerId);
                //Inserting data
                bool incomestatusmessage = true;
                bool expensestatusmessage = true;
                bool liabilitiesstatusmessage = true;
                bool assetstatusmessage = true;
                bool assetsubstatusmeessage = true;
                bool assetgroupstatusmeessage = true;
                bool detailsstatusmessage = true;

                if (incomedetailsvolist.Count != 0)
                {
                    incomestatusmessage = AddCustomerFPIncomeDetails(customerId, createdById, incomedetailsvolist, out totalincome);
                }
                if (expensedetailsvolist.Count != 0)
                {
                    expensestatusmessage = AddCustomerFPExpenseDetails(customerId, createdById, expensedetailsvolist, out totalExpense);
                }
                if (liabilitiesdetailsvolist.Count != 0)
                {
                    liabilitiesstatusmessage = AddLiabilitiesDetailsForCustomerProspect(customerId, createdById, liabilitiesdetailsvolist, out totalLiabilities, out totalLoanOutstanding);
                }
                if (assetdetailsvolist.Count != 0)
                {
                    assetstatusmessage = AddCustomerFPAssetInstrumentDetails(customerId, createdById, assetdetailsvolist, out instrumentTotal);
                }
                if (assetsubdetailsvolist.Count != 0)
                {
                    assetsubstatusmeessage = AddCustomerFPAssetSubInstrumentDetails(customerId, createdById, assetsubdetailsvolist, out subInstrumentTotal);
                }
                if (assetgroupdetailslist.Count != 0)
                {
                    assetgroupstatusmeessage = AddCustomerFPAssetGroupDetails(customerId, createdById, assetgroupdetailslist, out groupTotal);
                }
                if (customerprospectvo != null)
                {
                    detailsstatusmessage = AddDetailsForCustomerProspect(customerId, createdById, customerprospectvo);
                }
                //bool incomestatusmessage = UpdateCustomerIncomeDetailsForCustomerProspect(customerId, createdById, incomedetailsvolist, out totalincome);
                //bool expensestatusmessage = UpdateCustomerExpenseDetailsForCustomerProspect(customerId, createdById, expensedetailsvolist, out totalExpense);
                //bool liabilitiesstatusmessage = UpdateCustomerLiabilitiesDetailsForCustomerProspect(customerId, createdById, liabilitiesdetailsvolist, out totalLiabilities, out totalLoanOutstanding);
                //bool assetstatusmessage = UpdateCustomerFPAssetInstrumentDetails(customerId, createdById, assetdetailsvolist, out instrumentTotal);
                //bool assetsubstatusmeessage = UpdateCustomerFPAssetSubInstrumentDetails(customerId, createdById, assetsubdetailsvolist, out subInstrumentTotal);
                if (incomestatusmessage == true && expensestatusmessage == true && liabilitiesstatusmessage == true && assetstatusmessage == true && assetsubstatusmeessage == true && assetgroupstatusmeessage == true && detailsstatusmessage == true)
                {
                    statusMessage = true;
                }
                else
                {
                    statusMessage = false;
                }
            }
            catch (Exception ex)
            {
                statusMessage = false;
            }
            return statusMessage;
        }

        public Dictionary<string, object> Databuffer(int customerId)
        {
            Dictionary<string, object> dataCatch = new Dictionary<string, object>();
            List<CustomerProspectIncomeDetailsVo> IncomeDetailsForCustomerProspectList;
            List<CustomerProspectExpenseDetailsVo> ExpenseDetailsForCustomerProspectList;
            List<CustomerProspectLiabilitiesDetailsVo> LiabilitiesDetailsForCustomerProspectList;
            List<CustomerProspectAssetSubDetailsVo> CustomerFPAssetSubInstrumentDetailsList;
            List<CustomerProspectAssetDetailsVo> CustomerFPAssetInstrumentDetailsList;
            List<CustomerProspectAssetGroupDetails> CustomerFPAssetGroupDetailsList;
            GetAllDetailsForCustomerProspect(customerId,
            out IncomeDetailsForCustomerProspectList,
            out ExpenseDetailsForCustomerProspectList,
           out LiabilitiesDetailsForCustomerProspectList,
            out CustomerFPAssetGroupDetailsList,
            out CustomerFPAssetInstrumentDetailsList,
            out CustomerFPAssetSubInstrumentDetailsList);
            dataCatch.Add("IncomeDetailsList", IncomeDetailsForCustomerProspectList);
            dataCatch.Add("ExpenseDetailsList", ExpenseDetailsForCustomerProspectList);
            dataCatch.Add("LiabilitiesDetailsList", LiabilitiesDetailsForCustomerProspectList);
            dataCatch.Add("AssetInstrumentDetailsList", CustomerFPAssetInstrumentDetailsList);
            dataCatch.Add("AssetInstrumentSubDetailsList", CustomerFPAssetSubInstrumentDetailsList);
            dataCatch.Add("AssetGroupDetailsList", CustomerFPAssetGroupDetailsList);
            return dataCatch;
        }

       

        /// <summary>
        /// Used to Show FP Dashboard and Show Current and Recomonded Asset allocation.
        /// </summary>
        /// Added by: Vinayak Patil.
        /// <param name="CustomerId"></param>
        /// <returns></returns>

        public DataSet GetFPDashBoardAsstesBreakUp(int CustomerId)
        {


            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            DataSet dsFPDashBoard = new DataSet();
            try
            {
                dsFPDashBoard = customerprospectdao.GetFPDashBoardAsstesBreakUp(CustomerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectBo.cs:GetFPDashBoardAsstesBreakUp()");
                object[] objects = new object[3];
                objects[0] = CustomerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsFPDashBoard;

        }

        /* End For FPDashBoard Asset BreakUp */

        public DataSet GetFPCurrentAndRecomondedAssets(int CustomerId)
        {


            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            DataSet dsFPCurrentAndRecomondedAssets = new DataSet();
            try
            {
                dsFPCurrentAndRecomondedAssets = customerprospectdao.GetFPCurrentAndRecomondedAssets(CustomerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectBo.cs:GetFPCurrentAndRecomondedAssets()");
                object[] objects = new object[3];
                objects[0] = CustomerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsFPCurrentAndRecomondedAssets;

        }

        ///Sumary
        ///Used to get unmanaged and managed code for WERP 

        public DataSet GetUnmanagedManagedDetailsForFP(int CustomerId, int AdvisorId, int Switch)
        {
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            DataSet dsGetUnmanagedManagedDetailsForFP;
            dsGetUnmanagedManagedDetailsForFP = customerprospectdao.GetUnmanagedManagedDetailsForFP(CustomerId, AdvisorId, Switch);
            return dsGetUnmanagedManagedDetailsForFP;
        }


        /// <summary>
        /// To get All the Prospect custoomers for perticular RMId.
        /// </summary>
        /// Vinayak Patil
        /// <param name="RmId"></param>
        /// <returns></returns>
        public DataSet GetAllProspectCustomersForRM(int RmId)
        {
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            DataSet dsGetAllProspectCustomersForRM = new DataSet();
            try
            {
                dsGetAllProspectCustomersForRM = customerprospectdao.GetAllProspectCustomersForRM(RmId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectBo.cs:GetFPCurrentAndRecomondedAssets()");
                object[] objects = new object[3];
                objects[0] = RmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAllProspectCustomersForRM;

        }

        /// <summary>
        /// To get the FP analytic Standard data .. 
        /// </summary>
        /// Created by   ** Bhoopendra Sahoo **
        /// <param name="customerId"></param>
        /// <returns></returns>
        
        public DataSet GetCustomerFPAnalyticsStandard(int customerId)
        {
            DataSet dsGetCustomerFPAnalyticsStandard;
            CustomerProspectDao customerprospectdao = new CustomerProspectDao();
            try
            {
                dsGetCustomerFPAnalyticsStandard = customerprospectdao.GetCustomerFPAnalyticsStandard(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetCustomerFPAnalyticsStandard;
        }
        // *** End ***

    }
}
