using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoCommon
{

    public static class XMLDao
    {
        public static DataTable GetFiscalYearCode(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["FiscalYear"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetFiscalYearCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetBusinessType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["AdvisorBusinessType"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetBusinessType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetLOBCategory(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["AdvisorLOBCategory"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLOBCategory()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetLOBEquitySegment(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["AdvisorLOBEquitySegment"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLOBEquitySegment()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static string GetLOBClassification(string path, string assetGroupCode, string categoryCode, string segmentCode) // pass assetgroupcode, categorycode, segmentcode to get classification.
        {
            DataSet ds;
            // DataTable dt;
            DataRow[] dr;
            DataRow row;
            string LOBClassificationCode;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);                
                dr = ds.Tables["AdvisorLOBClassification"].Select("LOBAssetGroupCode = '" + assetGroupCode + "' and LOBCategoryCode = '" + categoryCode + "' and SegmentCode = '" + segmentCode + "'");               
                row = dr[0];
                LOBClassificationCode = row["LOBClassificationCode"].ToString();
                //foreach (DataRow row in dr)
                //{
                //    dt.Rows.Add(row);
                //}

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLOBClassification()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return LOBClassificationCode;

        }

        public static string GetLOBType(string path, string LOBClassificationCode)
        {
            DataSet ds;
            DataRow[] dr;
            DataRow row;
            string LOBType;
            string assetGroup;
            string category;
            string segment;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dr = ds.Tables["AdvisorLOBClassification"].Select("LOBClassificationCode = '" + LOBClassificationCode.ToString() + "'");
                row = dr[0];
                assetGroup = XMLDao.GetAssetGroup(path, row["LOBAssetGroupCode"].ToString());
                category = XMLDao.GetCategory(path, row["LOBCategoryCode"].ToString());
                if ((XMLDao.GetSegment(path, row["SegmentCode"].ToString())) != "")
                {
                    segment = XMLDao.GetSegment(path, row["SegmentCode"].ToString());
                }
                else{
                    segment="";
                }
                LOBType = assetGroup + " " + category + " "+ segment;
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLOBType()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = LOBClassificationCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return LOBType;
        }

        public static string GetSegment(string path, string segmentCode)
        {
            DataSet ds;
            DataRow[] dr;
            DataRow row;
            string segment;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dr = ds.Tables["AdvisorLOBEquitySegment"].Select("SegmentCode = '" + segmentCode.ToString() + "'");
                row = dr[0];
                segment = row["SegmentName"].ToString();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetSegment()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = segmentCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return segment;
        }

        public static string GetCategory(string path, string categoryCode)
        {
            DataSet ds;
            DataRow[] dr;
            DataRow row;
            string category;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dr = ds.Tables["AdvisorLOBCategory"].Select("LOBCategoryCode = '" + categoryCode.ToString() + "'");
                row = dr[0];
                category = row["LOBCategory"].ToString();

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
                objects[1] = categoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return category;
        }

        public static string GetAssetGroup(string path, string assetGroupCode)
        {
            DataSet ds;
            DataRow[] dr;
            DataRow row;
            string assetGroup;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dr = ds.Tables["AdvisorLOBAssetGroups"].Select("LOBAssetGroupCode = '" + assetGroupCode.ToString() + "'");
                row = dr[0];
                assetGroup = row["LOBAssetGroup"].ToString();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetAssetGroup()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = assetGroupCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return assetGroup;
        }

        public static string GetLOBIdentifer(string path,string identifierType)
        {
            DataSet ds;
            DataRow[] dr;
            DataRow row;
            string identifier;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dr = ds.Tables["AdvisorLOBIdentifierType"].Select("IdentifierTypeCode = '" + identifierType.ToString() + "'");
                row = dr[0];
                identifier = row["IdentifierTypeName"].ToString();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLOBIdentifer()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = identifierType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return identifier;
        }

        public static DataTable GetLOBIdentifierType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["AdvisorLOBIdentifierType"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLOBIdentifierType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetPortfolioType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["PortfolioTypes"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetPortfolioType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetBankAccountTypes(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["BankAccountTypes"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetBankAccountTypes()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetCustomerSubType(string path, string customerTypeCode)    //pass customer type and get subtypes only for that customertype
        {
            DataSet ds;
            DataTable dt=new DataTable();
            DataRow[] dr;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dr = ds.Tables["CustomerSubType"].Select("CustomerTypeCode = '" + customerTypeCode + "'");
                dt = ds.Tables["CustomerSubType"].Clone();
                foreach (DataRow row in dr)
                {
                    dt.ImportRow(row);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetCustomerSubType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetCustomerType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["CustomerType"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetCustomerType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetDebtIssuer(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["DebtIssuer"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetDebtIssuer()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetExternalSource(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["ExternalSource"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetExternalSource()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetFrequency(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Frequency"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetFrequency()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetInterestBasis(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["InterestBasis"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetInterestBasis()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetMaritalStatus(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["MaritalStatus"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetMaritalStatus()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetMeasureCode(string path, string assetGroup) //pass assetclass and get measure code for only that asset class
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["MeasureCode"].Clone();
                dr = ds.Tables["MeasureCode"].Select("AssetGroup = '" + assetGroup + "'");

                foreach (DataRow row in dr)
                {
                    dt.ImportRow(row);
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetMeasureCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetModeOfHolding(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["ModeOfHolding"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetModeOfHolding()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetNationality(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Nationality"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetNationality()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetOccupation(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Occupation"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetOccupation()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetPaymentMode(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["PaymentMode"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetPaymentMode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetQualification(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Qualification"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetQualification()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetSystematicTransactionType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["SystematicTransactionType"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetSystematicTransactionType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetProof(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Proof"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetProof()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetInsuranceIssuer(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["InsuranceIssuer"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetInsuranceIssuer()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetBroker(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Broker"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetBroker()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public static DataTable GetTransactionType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Transaction"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetTransactionType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;
        }

        public static DataTable GetExchangeType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Exchange"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetExchangeType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;
        }

        public static DataTable GetRelationship(string path,string type)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
               

                dr = ds.Tables["Relationship"].Select("Type = '" + type + "'");
                dt = ds.Tables["Relationship"].Clone();
                foreach (DataRow row in dr)
                {
                    dt.ImportRow(row);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetRelationship()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;
        }

        public static DataTable GetStates(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["State"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetStates()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public static DataTable GetDatePeriod(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["DatePeriod"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetDatePeriod()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public static DataTable GetAdviserBranchType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["AdviserBranchType"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetAdviserBranchType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public static DataTable GetLoanType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["LoanType"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLoanType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public static DataTable GetLoanAssociateType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["LoanAssociateType"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLoanAssociateType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public static DataTable GetEditType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["EditType"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetEditType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public static DataTable GetLoanPartner(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["LoanPartner"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLoanPartner()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public static DataTable GetRepaymentType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["RepaymentType"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetRepaymentType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public static DataTable GetCustomerCategory(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["CustomerCategory"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetCustomerCategory()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public static DataTable GetDeclineReason(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["DeclineReason"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetDeclineReason()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public static DataTable GetCopyType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["CopyType"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetCopyType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public static DataTable GetProofType(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["ProofType"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetProofType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        //start of functions for selecting the Name for the code passed

        public static string GetFiscalYearName(string path, string fiscalYearCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string fiscalYear;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["FiscalYear"];
                dr = dt.Select("FiscalYearCode = '" + fiscalYearCode + "'");
                fiscalYear = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetFiscalYearCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return fiscalYear;

        }

        public static string GetBusinessTypeName(string path, string businessTypeCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string businessTypeName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["AdvisorBusinessType"];
                dr = dt.Select("BusinessTypeCode = '" + businessTypeCode + "'");
                businessTypeName = dr[0][1].ToString();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetBusinessTypeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return businessTypeName;

        }

        public static string GetAdviserBranchTypeName(string path, string branchTypeCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string branchTypeName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["AdviserBranchType"];
                dr = dt.Select("XABRT_BranchTypeCode = '" + branchTypeCode + "'");
                branchTypeName = dr[0][1].ToString();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetAdviserBranchTypeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return branchTypeName;

        }

        public static string GetLOBCategoryName(string path, string lobCategoryCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string categoryName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["AdvisorLOBCategory"];
                dr = dt.Select("LOBCategoryCode = '" + lobCategoryCode + "'");
                categoryName = dr[0][1].ToString();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLOBCategoryName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return categoryName;

        }

        public static string GetLOBEquitySegmentName(string path, string segmentCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string lobSegmentName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["AdvisorLOBEquitySegment"];
                dr = dt.Select("SegmentCode = '" + segmentCode + "'");
                lobSegmentName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLOBEquitySegmentName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return lobSegmentName;

        }

        public static string GetLOBIdentifierName(string path, string identifierCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string lobIdentifierName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["AdvisorLOBIdentifierType"];
                dr = dt.Select("IdentifierTypeCode = '" + identifierCode + "'");
                lobIdentifierName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLOBIdentifierName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return lobIdentifierName;

        }

        public static string GetBankAccountName(string path, string bankAccountTypeCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string bankAccountTypeName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["BankAccountTypes"];
                dr = dt.Select("BankAccountTypeCode = '" + bankAccountTypeCode + "'");
                bankAccountTypeName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetBankAccountName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bankAccountTypeName;

        }

        public static string GetCustomerSubTypeName(string path, string customerSubTypeCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string customerSubTypeName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["CustomerSubType"];
                dr = dt.Select("CustomerSubTypeCode = '" + customerSubTypeCode + "'");
                if (dr.Length != 0)
                    customerSubTypeName = dr[0][2].ToString();
                else
                    customerSubTypeName = "";
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetCustomerSubTypeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerSubTypeName;

        }

        public static string GetCustomerTypeName(string path, string customerTypeCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string customerTypeName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["CustomerType"];
                
                    dr = dt.Select("CustomerTypeCode = '" + customerTypeCode + "'");
                    if (dr.Length != 0)
                    {
                        customerTypeName = dr[0][1].ToString();
                    }

                    else
                    {
                        customerTypeName = "";
                    }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetCustomerTypeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerTypeName;

        }

        public static string GetDebtIssuerName(string path, string debtIssuerCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string debtIssuerName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["DebtIssuer"];
                dr = dt.Select("DebtIssuerCode = '" + debtIssuerCode + "'");
                debtIssuerName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetDebtIssuerName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return debtIssuerName;

        }

        public static string GetExtSourceName(string path, string extSourceCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string extSourceName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["ExternalSource"];
                dr = dt.Select("SourceCode = '" + extSourceCode + "'");
                extSourceName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetExtSourceName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return extSourceName;

        }

        public static string GetFrequencyName(string path, string freqCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string freqName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Frequency"];
                dr = dt.Select("FrequencyCode = '" + freqCode + "'");
                freqName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetFrequencyName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return freqName;

        }

        public static string GetInterestBasisName(string path, string interestBasisCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string interestBasisName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["InterestBasis"];
                dr = dt.Select("InterestBasisCode = '" + interestBasisCode + "'");
                interestBasisName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetInterestBasisName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return interestBasisName;

        }

        public static string GetMaritalStatusName(string path, string maritalStatusCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string maritalStatusName;
            try
            {
                if (maritalStatusCode != "")
                {
                    ds = new DataSet();
                    ds.ReadXml(path);
                    dt = ds.Tables["MaritalStatus"];
                    dr = dt.Select("MaritalStatusCode = '" + maritalStatusCode + "'");
                    if (dr != null)
                    {
                        maritalStatusName = dr[0][1].ToString();
                    }
                    else
                    {
                        maritalStatusName = "";
                    }
                }
                else
                    maritalStatusName = "";
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetMaritalStatusName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return maritalStatusName;

        }

        public static string GetMeasureCodeName(string path, string measureCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string measureName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["MeasureCode"];
                dr = dt.Select("MeasureCode = '" + measureCode + "'");
                measureName = dr[0]["Measure"].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetMeasureCodeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return measureName;

        }

        public static string GetModeOfHoldingName(string path, string modeOfHoldingCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string modeOfHoldingName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["ModeOfHolding"];
                dr = dt.Select("ModeOfHoldingCode = '" + modeOfHoldingCode + "'");
                modeOfHoldingName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetModeOfHoldingName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return modeOfHoldingName;

        }

        public static string GetNationalityName(string path, string nationalityCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string nationalityName;
            try
            {
                if (nationalityCode != "")
                {
                    ds = new DataSet();
                    ds.ReadXml(path);
                    dt = ds.Tables["Nationality"];
                    dr = dt.Select("NationalityCode = '" + nationalityCode + "'");
                    if (dr != null)
                    {
                        nationalityName = dr[0][1].ToString();
                    }
                    else
                        nationalityName = "";
                }
                else
                {
                    nationalityName = "";
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetNationalityName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return nationalityName;

        }

        public static string GetOccupationName(string path, string occupationCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string occupationName;
            try
            {
                if (occupationCode != "")
                {
                    ds = new DataSet();
                    ds.ReadXml(path);
                    dt = ds.Tables["Occupation"];
                    dr = dt.Select("OccupationCode = '" + occupationCode + "'");

                    if (dr == null)
                    {
                        occupationName = "";
                    }
                    else
                    {
                        occupationName = dr[0][1].ToString();
                    }
                }
                else
                    occupationName = "";
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetOccupationName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return occupationName;

        }

        public static string GetPaymentModeName(string path, string paymentModeCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string paymentModeName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["PaymentMode"];
                dr = dt.Select("PaymentModeCode = '" + paymentModeCode + "'");
                paymentModeName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetPaymentModeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return paymentModeName;

        }

        public static string GetQualificationName(string path, string qualificationCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string qualificationName;
            try
            {
                if (qualificationCode != "")
                {
                    ds = new DataSet();
                    ds.ReadXml(path);
                    dt = ds.Tables["Qualification"];
                    dr = dt.Select("QualificationCode = '" + qualificationCode + "'");
                    if (dr != null)
                    {
                        qualificationName = dr[0][1].ToString();
                    }
                    else
                    {
                        qualificationName = "";
                    }
                }
                else
                    qualificationName = "";
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetQualificationName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return qualificationName;

        }

        public static string GetSystematicTypeName(string path, string systematicTypeCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string systematicTypeName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["SystematicTransactionType"];
                dr = dt.Select("SystemationTypeCode = '" + systematicTypeCode + "'");
                systematicTypeName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetSystematicTypeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return systematicTypeName;

        }

        public static string GetInsuranceIssuerName(string path, string insuranceIssuerCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string insuranceIssuerName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["InsuranceIssuer"];
                dr = dt.Select("InsuranceIssuerCode = '" + insuranceIssuerCode + "'");
                insuranceIssuerName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetInsuranceIssuerName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return insuranceIssuerName;

        }

        public static string GetBrokerName(string path, string brokerCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string brokerName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Broker"];
                dr = dt.Select("BrokerCode = '" + brokerCode + "'");
                brokerName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetBrokerName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return brokerName;

        }

        public static string GetRelationshipName(string path, string relationshipCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string relationshipName;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Relationship"];
                dr = dt.Select("RelationshipCode = '" + relationshipCode + "'");
                relationshipName = dr[0][1].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetRelationshipName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return relationshipName;

        }

        public static int getUploadFiletypeCode(string path, string UploadAssetType, string UploadSourceType, string UploadFileType)
        {
            DataSet ds;
            DataRow[] dr;
            DataRow row;
            int filetypeid;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dr = ds.Tables["XMLExternalSourceFileType"].Select("XESFT_AssetGroup = '" + UploadAssetType + "' and XES_SourceCode = '" + UploadSourceType + "' and XESFT_FileType = '" + UploadFileType + "' ");
                row = dr[0];
                filetypeid = Convert.ToInt32(row["WUXFT_XMLFileTypeId"].ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:getUploadFiletypeCode()");
                object[] objects = new object[4];
                objects[0] = path;
                objects[1] = UploadAssetType;
                objects[2] = UploadSourceType;
                objects[3] = UploadFileType;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return filetypeid;
        }

        public static string GetStateName(string path, string stateCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string stateName="";
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["State"];
               
                    dr = dt.Select("StateCode = '" + stateCode + "'");
                    if (dr.Length > 0)
                    {
                    stateName = dr[0][1].ToString();
                    }
                    
                }
            
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetStateName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return stateName;
        }

        public static string GetStateCode(string path, string stateName)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string stateCode = "";
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["State"];

                dr = dt.Select("StateName = '" + stateName + "'");
                if (dr.Length>0)
                {
                    stateCode = dr[0][1].ToString();
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetStateCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return stateName;
        }

        public static DataTable GetCurrency(string path)
        {
            DataSet ds;
            DataTable dt;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Currency"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetCurrency()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;
        }

        public static string GetLoanTypeName(string path, string LoanType)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string loanName = "";
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["LoanType"];

                dr = dt.Select("XLT_LoanTypeCode = '" + LoanType + "'");
                if (dr.Length > 0)
                {
                    loanName = dr[0][1].ToString();
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLoanTypeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return loanName;
        }

        public static string GetAssociateDescription(string path, string AssociateCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string AssociateDesc = "";
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["LoanAssociateType"];

                dr = dt.Select("XLAT_LoanAssociateCode = '" + AssociateCode + "'");
                if (dr.Length > 0)
                {
                    AssociateDesc = dr[0][1].ToString();
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetAssociateDescription()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return AssociateDesc;
        }

        public static string GetEditTypeName(string path, string EditTypeCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string EditTypeDesc = "";
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["EditType"];

                dr = dt.Select("XLET_EditTypeCode = '" + EditTypeCode + "'");
                if (dr.Length > 0)
                {
                    EditTypeDesc = dr[0][1].ToString();
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetEditTypeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return EditTypeDesc;
        }

        public static string GetLoanPartnerName(string path, string LoanPertnerCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string loanPartnerName = "";
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["LoanPartner"];

                dr = dt.Select("XLP_LoanPartnerCode = '" + LoanPertnerCode + "'");
                if (dr.Length > 0)
                {
                    loanPartnerName = dr[0][1].ToString();
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetLoanPartnerName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return loanPartnerName;
        }

        public static string GetRepaymentTypeName(string path, string RepaymentTypeCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string repaymentTypeName = "";
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["RepaymentType"];

                dr = dt.Select("XRT_RepaymentTypeCode = '" + RepaymentTypeCode + "'");
                if (dr.Length > 0)
                {
                    repaymentTypeName = dr[0][1].ToString();
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetRepaymentTypeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return repaymentTypeName;
        }

        public static string GetCustomerCategoryName(string path, string customerCategoryCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string customerCategoryName = "";
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["CustomerCategory"];

                dr = dt.Select("XCC_CustomerCategoryCode = '" + customerCategoryCode + "'");
                if (dr.Length > 0)
                {
                    customerCategoryName = dr[0][1].ToString();
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetRepaymentTypeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerCategoryName;
        }

        public static string GetDeclineReasonName(string path, string declineReasonCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string declineReasonName = "";
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["DeclineReason"];

                dr = dt.Select("XDR_DeclineReasonCode = '" + declineReasonCode + "'");
                if (dr.Length > 0)
                {
                    declineReasonName = dr[0][1].ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetRepaymentTypeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return declineReasonName;
        }

        public static string GetCopyTypeName(string path, string copyTypeCode)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string typeName = "";
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["CopyType"];

                dr = dt.Select("XPCT_ProofCopyTypeCode = '" + copyTypeCode + "'");
                if (dr.Length > 0)
                {
                    typeName = dr[0][1].ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetCopyTypeName()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = copyTypeCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return typeName;
        }

        public static string GetProofTypeName(string path, string proofTypeName)
        {
            DataSet ds;
            DataTable dt;
            DataRow[] dr;
            string typeName = "";
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["ProofType"];

                dr = dt.Select("XPRT_ProofTypeCode = '" + proofTypeName + "'");
                if (dr.Length > 0)
                {
                    typeName = dr[0][1].ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetProofTypeName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return typeName;
        }

        public static DataTable GetUploadInpuValidationColumns(string Uploadtype, string Extracttype, string path)
        {
            DataSet ds;
            DataTable dt = new DataTable();
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                
                if(Uploadtype == "CA" && Extracttype == "MFT" )
                    dt = ds.Tables["CAMSValidationColumn"];
                else if (Uploadtype == "KA" && Extracttype == "MFT")
                    dt = ds.Tables["KarvyValidationColumn"];
                else if (Uploadtype == "DT" && Extracttype == "MFT")
                    dt = ds.Tables["DeutscheValidationColumn"];
                else if (Uploadtype == "TN" && Extracttype == "MFT")
                    dt = ds.Tables["TempletonValidationColumn"];


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "XMLDao.cs:GetUploadInpuValidationColumns()");
                object[] objects = new object[3];
                objects[0] = path;
                objects[1] = Uploadtype;
                objects[2] = Extracttype;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;
        }

    }
}
