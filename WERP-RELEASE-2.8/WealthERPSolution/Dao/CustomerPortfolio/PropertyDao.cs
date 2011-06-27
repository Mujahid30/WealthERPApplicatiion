using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using VoCustomerPortfolio;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;


namespace DaoCustomerPortfolio
{
    public class PropertyDao
    {
        public int CreatePropertyPortfolio(PropertyVo propertyVo, int userId)
        {
            int PropertyId = 0;
            Database db;
            DbCommand createPropertyPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createPropertyPortfolioCmd = db.GetStoredProcCommand("SP_CreatePropertyNetPosition");
                db.AddInParameter(createPropertyPortfolioCmd, "@CPA_AccountId", DbType.Int32, propertyVo.AccountId);
                db.AddInParameter(createPropertyPortfolioCmd, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, propertyVo.AssetSubCategoryCode);
                db.AddInParameter(createPropertyPortfolioCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, propertyVo.AssetCategoryCode);
                db.AddInParameter(createPropertyPortfolioCmd, "@PAG_AssetGroupCode", DbType.String, propertyVo.AssetGroupCode);
                db.AddInParameter(createPropertyPortfolioCmd, "@XMC_MeasureCode", DbType.String, propertyVo.MeasureCode);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_Name", DbType.String, propertyVo.Name);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_PropertyAdrLine1", DbType.String, propertyVo.PropertyAdrLine1);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_PropertyAdrLine2", DbType.String, propertyVo.PropertyAdrLine2);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_PropertyAdrLine3", DbType.String, propertyVo.PropertyAdrLine3);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_PropertyCity", DbType.String, propertyVo.PropertyCity);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_PropertyState", DbType.String, propertyVo.PropertyState);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_PropertyCountry", DbType.String, propertyVo.PropertyCountry);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_PropertyPinCode", DbType.Int32, propertyVo.PropertyPinCode);
                if(propertyVo.PurchaseDate != DateTime.MinValue)
                    db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_PurchaseDate", DbType.DateTime, propertyVo.PurchaseDate);
                else
                    db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_PurchaseDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_PurchasePrice", DbType.Decimal, propertyVo.PurchasePrice);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_Quantity", DbType.Decimal, propertyVo.Quantity);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_CurrentPrice", DbType.Decimal, propertyVo.CurrentPrice);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_CurrentValue", DbType.Decimal, propertyVo.CurrentValue);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_PurchaseValue", DbType.Decimal, propertyVo.PurchaseValue);
                if (propertyVo.SellDate != DateTime.MinValue)
                    db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_SellDate", DbType.DateTime, propertyVo.SellDate);
                else
                    db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_SellDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_SellPrice", DbType.Decimal, propertyVo.SellPrice);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_SellValue", DbType.Decimal, propertyVo.SellValue);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_Remark", DbType.String, propertyVo.Remark);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_CreatedBy", DbType.Decimal, userId);
                db.AddInParameter(createPropertyPortfolioCmd, "@CPNP_ModifiedBy", DbType.Decimal, userId);

                db.AddOutParameter(createPropertyPortfolioCmd, "@PropertyId", DbType.Int32, 500);

                if (db.ExecuteNonQuery(createPropertyPortfolioCmd) != 0)
                    PropertyId = int.Parse(db.GetParameterValue(createPropertyPortfolioCmd, "PropertyId").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PropertyDao.cs:CreatePropertyPortfolio()");

                object[] objects = new object[2];
                objects[0] = propertyVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return PropertyId;
        }

        public List<PropertyVo> GetPropertyPortfolio(int portfolioId, int CurrentPage, string SortOrder, out int Count)
        {
            List<PropertyVo> propertyList = null;
            PropertyVo propertyVo = new PropertyVo();
            Database db;
            DbCommand getPropertyPortfolioCmd;
            DataSet dsGetPropertyPortfolio;
            DataTable dtGetPropertyPortfolio;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPropertyPortfolioCmd = db.GetStoredProcCommand("SP_GetPropertyNetPositionList");
                db.AddInParameter(getPropertyPortfolioCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getPropertyPortfolioCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getPropertyPortfolioCmd, "@SortOrder", DbType.String, SortOrder);
                dsGetPropertyPortfolio = db.ExecuteDataSet(getPropertyPortfolioCmd);
                dtGetPropertyPortfolio = dsGetPropertyPortfolio.Tables[0];

                if (dsGetPropertyPortfolio.Tables[1] != null && dsGetPropertyPortfolio.Tables[1].Rows.Count > 0)
                    Count = Int32.Parse(dsGetPropertyPortfolio.Tables[1].Rows[0][0].ToString());
                else
                    Count = 0;
                if (dsGetPropertyPortfolio.Tables[0].Rows.Count > 0)
                {
                    propertyList = new List<PropertyVo>();

                    foreach (DataRow dr in dtGetPropertyPortfolio.Rows)
                    {
                        propertyVo = new PropertyVo();

                        propertyVo.AccountId = int.Parse(dr["CPA_AccountId"].ToString());
                        propertyVo.PropertyId = int.Parse(dr["CPNP_PropertyNPId"].ToString());
                        propertyVo.AssetSubCategoryCode = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                        propertyVo.AssetCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        propertyVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                        propertyVo.MeasureCode = dr["XMC_MeasureCode"].ToString();
                        propertyVo.Name = dr["CPNP_Name"].ToString();
                        propertyVo.PropertyAdrLine1 = dr["CPNP_PropertyAdrLine1"].ToString();
                        propertyVo.PropertyAdrLine2 = dr["CPNP_PropertyAdrLine2"].ToString();
                        propertyVo.PropertyAdrLine3 = dr["CPNP_PropertyAdrLine3"].ToString();
                        propertyVo.PropertyCity = dr["CPNP_PropertyCity"].ToString();
                        propertyVo.PropertyState = dr["CPNP_PropertyState"].ToString();
                        propertyVo.PropertyCountry = dr["CPNP_PropertyCountry"].ToString();
                        propertyVo.PropertyPinCode = int.Parse(dr["CPNP_PropertyPinCode"].ToString());
                        if(dr["CPNP_PurchaseDate"].ToString() != "")
                            propertyVo.PurchaseDate = DateTime.Parse(dr["CPNP_PurchaseDate"].ToString());
                        propertyVo.PurchasePrice = float.Parse(dr["CPNP_PurchasePrice"].ToString());
                        propertyVo.Quantity = float.Parse(dr["CPNP_Quantity"].ToString());
                        propertyVo.CurrentPrice = float.Parse(dr["CPNP_CurrentPrice"].ToString());
                        propertyVo.CurrentValue = float.Parse(dr["CPNP_CurrentValue"].ToString());
                        propertyVo.PurchaseValue = float.Parse(dr["CPNP_PurchaseValue"].ToString());
                        if (dr["CPNP_SellDate"].ToString() != "")
                            propertyVo.SellDate = DateTime.Parse(dr["CPNP_SellDate"].ToString());
                        if (dr["CPNP_SellPrice"].ToString() != "")
                            propertyVo.SellPrice = float.Parse(dr["CPNP_SellPrice"].ToString());
                        if (dr["CPNP_SellValue"].ToString() != "")
                            propertyVo.SellValue = float.Parse(dr["CPNP_SellValue"].ToString());
                        propertyVo.Remark = dr["CPNP_Remark"].ToString();
                        propertyVo.AssetSubCategoryName = dr["PAISC_AssetInstrumentSubCategoryName"].ToString();

                        propertyList.Add(propertyVo);
                    }
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
                FunctionInfo.Add("Method", "PropertyDao.cs:GetPropertyPortfolio()");

                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return propertyList;
        }

        public PropertyVo GetPropertyAsset(int propertyId)
        {

            PropertyVo propertyVo = null;
            Database db;
            DbCommand getPropertyPortfolioCmd;
            DataSet dsGetPropertyPortfolio;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPropertyPortfolioCmd = db.GetStoredProcCommand("SP_GetPropertyNetPosition");
                db.AddInParameter(getPropertyPortfolioCmd, "@CPNP_PropertyNPId", DbType.Int32, propertyId);

                dsGetPropertyPortfolio = db.ExecuteDataSet(getPropertyPortfolioCmd);
                if (dsGetPropertyPortfolio.Tables[0].Rows.Count > 0)
                {
                    propertyVo = new PropertyVo();
                    dr = dsGetPropertyPortfolio.Tables[0].Rows[0];

                    propertyVo.AccountId = int.Parse(dr["CPA_AccountId"].ToString());
                    propertyVo.PropertyId = int.Parse(dr["CPNP_PropertyNPId"].ToString());
                    propertyVo.AssetSubCategoryCode = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                    propertyVo.AssetCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    propertyVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                    propertyVo.MeasureCode = dr["XMC_MeasureCode"].ToString();
                    propertyVo.Name = dr["CPNP_Name"].ToString();
                    propertyVo.PropertyAdrLine1 = dr["CPNP_PropertyAdrLine1"].ToString();
                    propertyVo.PropertyAdrLine2 = dr["CPNP_PropertyAdrLine2"].ToString();
                    propertyVo.PropertyAdrLine3 = dr["CPNP_PropertyAdrLine3"].ToString();
                    propertyVo.PropertyCity = dr["CPNP_PropertyCity"].ToString();
                    propertyVo.PropertyState = dr["CPNP_PropertyState"].ToString();
                    propertyVo.PropertyCountry = dr["CPNP_PropertyCountry"].ToString();
                    propertyVo.PropertyPinCode = int.Parse(dr["CPNP_PropertyPinCode"].ToString());
                    if(dr["CPNP_PurchaseDate"].ToString() != "")
                        propertyVo.PurchaseDate = DateTime.Parse(dr["CPNP_PurchaseDate"].ToString());
                    propertyVo.PurchasePrice = float.Parse(dr["CPNP_PurchasePrice"].ToString());
                    propertyVo.Quantity = float.Parse(dr["CPNP_Quantity"].ToString());
                    propertyVo.CurrentPrice = float.Parse(dr["CPNP_CurrentPrice"].ToString());
                    propertyVo.CurrentValue = float.Parse(dr["CPNP_CurrentValue"].ToString());
                    propertyVo.PurchaseValue = float.Parse(dr["CPNP_PurchaseValue"].ToString());
                    if (dr["CPNP_SellDate"].ToString() != "")
                        propertyVo.SellDate = DateTime.Parse(dr["CPNP_SellDate"].ToString());
                    if (dr["CPNP_SellPrice"].ToString() != "")
                        propertyVo.SellPrice = float.Parse(dr["CPNP_SellPrice"].ToString());
                    if (dr["CPNP_SellValue"].ToString() != "")
                        propertyVo.SellValue = float.Parse(dr["CPNP_SellValue"].ToString());
                    propertyVo.Remark = dr["CPNP_Remark"].ToString();
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

                FunctionInfo.Add("Method", "PropertyDao.cs:GetPropertyAsset()");


                object[] objects = new object[1];
                objects[0] = propertyId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return propertyVo;

        }

        public bool UpdatePropertyPortfolio(PropertyVo propertyVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updatePropertyPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updatePropertyPortfolioCmd = db.GetStoredProcCommand("SP_UpdatePropertyNetPosition");
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PropertyNPId", DbType.Int32, propertyVo.PropertyId);
                db.AddInParameter(updatePropertyPortfolioCmd, "@XMC_MeasureCode", DbType.String, propertyVo.MeasureCode);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_Name", DbType.String, propertyVo.Name);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PropertyAdrLine1", DbType.String, propertyVo.PropertyAdrLine1);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PropertyAdrLine2", DbType.String, propertyVo.PropertyAdrLine2);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PropertyAdrLine3", DbType.String, propertyVo.PropertyAdrLine3);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PropertyCity", DbType.String, propertyVo.PropertyCity);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PropertyState", DbType.String, propertyVo.PropertyState);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PropertyCountry", DbType.String, propertyVo.PropertyCountry);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PropertyPinCode", DbType.Int32, propertyVo.PropertyPinCode);
                if (propertyVo.PurchaseDate != DateTime.MinValue)
                    db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PurchaseDate", DbType.DateTime, propertyVo.PurchaseDate);
                else
                    db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PurchaseDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PurchasePrice", DbType.Decimal, propertyVo.PurchasePrice);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_Quantity", DbType.Decimal, propertyVo.Quantity);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_CurrentPrice", DbType.Decimal, propertyVo.CurrentPrice);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_CurrentValue", DbType.Decimal, propertyVo.CurrentValue);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_PurchaseValue", DbType.Decimal, propertyVo.PurchaseValue);
                if (propertyVo.SellDate != DateTime.MinValue)
                    db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_SellDate", DbType.DateTime, propertyVo.SellDate);
                else
                    db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_SellDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_SellPrice", DbType.Decimal, propertyVo.SellPrice);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_SellValue", DbType.Decimal, propertyVo.SellValue);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_Remark", DbType.String, propertyVo.Remark);
                db.AddInParameter(updatePropertyPortfolioCmd, "@CPNP_ModifiedBy", DbType.Decimal, userId);

                if (db.ExecuteNonQuery(updatePropertyPortfolioCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PropertyDao.cs:UpdatePropertyPortfolio()");

                object[] objects = new object[2];
                objects[0] = propertyVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool DeletePropertyPortfolio(int propertyId, int accountId)
        {
            bool bResult = false;

            Database db;
            DbCommand deletePropertyPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deletePropertyPortfolioCmd = db.GetStoredProcCommand("SP_DeletePropertyNetPostion");

                db.AddInParameter(deletePropertyPortfolioCmd, "@CPNP_PropertyNPId", DbType.Int32, propertyId);
                db.AddInParameter(deletePropertyPortfolioCmd, "@CPA_AccountId", DbType.Int32, accountId);

                if (db.ExecuteNonQuery(deletePropertyPortfolioCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceDao.cs:DeletePropertyPortfolio()");
                object[] objects = new object[2];
                objects[0] = propertyId;
                objects[1] = accountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public DataTable GetLoanAgainstProperty(int customerId)
        {


            Database db;
            DbCommand getPropertyPortfolioCmd;
            DataSet dsGetPropertyPortfolio;
            DataTable dt = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPropertyPortfolioCmd = db.GetStoredProcCommand("SP_GetLoanAgainstProperties");
                db.AddInParameter(getPropertyPortfolioCmd, "@CustomerId", DbType.Int32, customerId);

                dsGetPropertyPortfolio = db.ExecuteDataSet(getPropertyPortfolioCmd);
                if (dsGetPropertyPortfolio.Tables[0].Rows.Count > 0)
                {
                    dt = dsGetPropertyPortfolio.Tables[0];
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
                FunctionInfo.Add("Method", "PropertyDao.cs:GetLoanAgainstProperty()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dt;

        }

        public Dictionary<int, string> GetPropertyDropDown(string customerId, List<int> custBorrowerIds)
        {
            Database db;
            DbCommand getPropertyDDLCmd;
            DataSet dsGetPropertyDDL = new DataSet();
            Dictionary<int, string> propertyList = new Dictionary<int, string>();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                if (custBorrowerIds != null)
                {
                    if (custBorrowerIds.Count > 0)
                    {
                        for (int i = 0; i < custBorrowerIds.Count; i++)
                        {
                            getPropertyDDLCmd = db.GetStoredProcCommand("SP_GetPropertyDropDown");
                            db.AddInParameter(getPropertyDDLCmd, "@CustomerId", DbType.Int32, Int32.Parse(customerId));
                            db.AddInParameter(getPropertyDDLCmd, "@BorrowerId", DbType.Int32, DBNull.Value);
                            dsGetPropertyDDL = db.ExecuteDataSet(getPropertyDDLCmd);

                            if (dsGetPropertyDDL.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dsGetPropertyDDL.Tables[0].Rows.Count; j++)
                                {
                                    int AssetId = Int32.Parse(dsGetPropertyDDL.Tables[0].Rows[j]["AssetId"].ToString());
                                    string AssetName = dsGetPropertyDDL.Tables[0].Rows[j]["AssetName"].ToString();
                                    if (propertyList.Count > 0)
                                    {
                                        if (propertyList.ContainsKey(AssetId) == false)
                                        {
                                            propertyList.Add(AssetId, AssetName);
                                        }
                                    }
                                    else
                                    {
                                        propertyList.Add(AssetId, AssetName);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        getPropertyDDLCmd = db.GetStoredProcCommand("SP_GetPropertyDropDown");
                        db.AddInParameter(getPropertyDDLCmd, "@CustomerId", DbType.Int32, Int32.Parse(customerId));
                        db.AddInParameter(getPropertyDDLCmd, "@BorrowerId", DbType.Int32, DBNull.Value);
                        dsGetPropertyDDL = db.ExecuteDataSet(getPropertyDDLCmd);

                        if (dsGetPropertyDDL.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsGetPropertyDDL.Tables[0].Rows.Count; j++)
                            {
                                int AssetId = Int32.Parse(dsGetPropertyDDL.Tables[0].Rows[j]["AssetId"].ToString());
                                string AssetName = dsGetPropertyDDL.Tables[0].Rows[j]["AssetName"].ToString();
                                if (propertyList.Count > 0)
                                {
                                    if (propertyList.ContainsKey(AssetId) == false)
                                    {
                                        propertyList.Add(AssetId, AssetName);
                                    }
                                }
                                else
                                {
                                    propertyList.Add(AssetId, AssetName);
                                }
                            }
                        }
                    }
                }
                else
                {
                    getPropertyDDLCmd = db.GetStoredProcCommand("SP_GetPropertyDropDown");
                    db.AddInParameter(getPropertyDDLCmd, "@CustomerId", DbType.Int32, Int32.Parse(customerId));
                    db.AddInParameter(getPropertyDDLCmd, "@BorrowerId", DbType.Int32, DBNull.Value);
                    dsGetPropertyDDL = db.ExecuteDataSet(getPropertyDDLCmd);
                    if (dsGetPropertyDDL.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dsGetPropertyDDL.Tables[0].Rows.Count; j++)
                        {
                            int AssetId = Int32.Parse(dsGetPropertyDDL.Tables[0].Rows[j]["AssetId"].ToString());
                            string AssetName = dsGetPropertyDDL.Tables[0].Rows[j]["AssetName"].ToString();
                            if (propertyList.Count > 0)
                            {
                                if (propertyList.ContainsKey(AssetId) == false)
                                {
                                    propertyList.Add(AssetId, AssetName);
                                }
                            }
                            else
                            {
                                propertyList.Add(AssetId, AssetName);
                            }
                        }
                    }
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
                FunctionInfo.Add("Method", "PropertyDao.cs:GetLoanAgainstProperty()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return propertyList;
        }

    }
}
