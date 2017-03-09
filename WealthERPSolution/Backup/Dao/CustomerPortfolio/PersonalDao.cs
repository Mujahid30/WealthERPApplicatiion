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
    public class PersonalDao
    {
        public int CreatePersonalNetPosition(PersonalVo personalVo, int userId)
        {
            int personalId = 0;
            Database db;
            DbCommand createPersonalNetPosition;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createPersonalNetPosition = db.GetStoredProcCommand("SP_CreatePersonalNetPosition");
                db.AddInParameter(createPersonalNetPosition, "@CP_PortfolioId", DbType.Int32, personalVo.PortfolioId);
                db.AddInParameter(createPersonalNetPosition, "@PAIC_AssetInstrumentCategoryCode", DbType.String, personalVo.AssetCategoryCode);
                db.AddInParameter(createPersonalNetPosition, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, personalVo.AssetSubCategoryCode);
                db.AddInParameter(createPersonalNetPosition, "@PAG_AssetGroupCode", DbType.String, personalVo.AssetGroupCode);
                db.AddInParameter(createPersonalNetPosition, "@CPNP_Name", DbType.String, personalVo.Name);
                if (personalVo.PurchaseDate != DateTime.MinValue)
                    db.AddInParameter(createPersonalNetPosition, "@CPNP_PurchaseDate", DbType.DateTime, personalVo.PurchaseDate);
                else
                    db.AddInParameter(createPersonalNetPosition, "@CPNP_PurchaseDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createPersonalNetPosition, "@CPNP_PurchasePrice", DbType.Decimal, personalVo.PurchasePrice);
                db.AddInParameter(createPersonalNetPosition, "@CPNP_Quantity", DbType.Decimal, personalVo.Quantity);
                db.AddInParameter(createPersonalNetPosition, "@CPNP_PurchaseValue", DbType.Decimal, personalVo.PurchaseValue);
                db.AddInParameter(createPersonalNetPosition, "@CPNP_CurrentPrice", DbType.Decimal, personalVo.CurrentPrice);
                db.AddInParameter(createPersonalNetPosition, "@CPNP_CurrentValue", DbType.Decimal, personalVo.CurrentValue);
                db.AddInParameter(createPersonalNetPosition, "@CPNP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createPersonalNetPosition, "@CPNP_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(createPersonalNetPosition, "@PersonalId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(createPersonalNetPosition) != 0)

                    personalId = int.Parse(db.GetParameterValue(createPersonalNetPosition, "PersonalId").ToString());
                
               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PersonalDao.cs:CreatePersonalNetPosition()");


                object[] objects = new object[2];
                objects[0] = personalVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return personalId;
        }

        public List<PersonalVo> GetPersonalNetPosition(int portfolioId,int CurrentPage, string sortOrder , out int count)
        {
            List<PersonalVo> personalList=null;
            PersonalVo personalVo;
            Database db;
            DbCommand getPersonalNetPositionCmd;
            DataSet dsGetPersonalNetPosition;
            DataTable dtGetPersonalNetPosition;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPersonalNetPositionCmd = db.GetStoredProcCommand("SP_GetPersonalNetPoition");
                db.AddInParameter(getPersonalNetPositionCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getPersonalNetPositionCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getPersonalNetPositionCmd, "@SortOrder", DbType.String, sortOrder);
                dsGetPersonalNetPosition = db.ExecuteDataSet(getPersonalNetPositionCmd);
                dtGetPersonalNetPosition = dsGetPersonalNetPosition.Tables[0];
                if (dsGetPersonalNetPosition.Tables[0] != null && dsGetPersonalNetPosition.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(dsGetPersonalNetPosition.Tables[1].Rows[0][0].ToString());
                else
                    count = 0;
                if (dsGetPersonalNetPosition.Tables[0].Rows.Count > 0)
                {
                    personalList = new List<PersonalVo>();

                    foreach (DataRow dr in dtGetPersonalNetPosition.Rows)
                    {
                        personalVo = new PersonalVo();

                        personalVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        personalVo.PersonalPortfolioId = int.Parse(dr["CPNP_PersonalNPId"].ToString());
                        personalVo.AssetCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        personalVo.AssetSubCategoryCode = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                        personalVo.AssetCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        personalVo.AssetSubCategoryName = dr["PAISC_AssetInstrumentSubCategoryName"].ToString();
                        personalVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                        personalVo.Name = dr["CPNP_Name"].ToString();
                        if (dr["CPNP_PurchaseDate"].ToString() != string.Empty)
                            personalVo.PurchaseDate = DateTime.Parse(dr["CPNP_PurchaseDate"].ToString());
                        else
                            personalVo.PurchaseDate = DateTime.MinValue;
                        personalVo.PurchasePrice = float.Parse(dr["CPNP_PurchasePrice"].ToString());
                        personalVo.Quantity = float.Parse(dr["CPNP_Quantity"].ToString());
                        personalVo.PurchaseValue = float.Parse(dr["CPNP_PurchaseValue"].ToString());
                        personalVo.CurrentPrice = float.Parse(dr["CPNP_CurrentPrice"].ToString());
                        personalVo.CurrentValue = float.Parse(dr["CPNP_CurrentValue"].ToString());
                        personalList.Add(personalVo);
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

                FunctionInfo.Add("Method", "PersonalDao.cs:GetPersonalNetPosition()");


                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return personalList;

        }

        public PersonalVo GetPersonalAsset(int PersonalNetPositionId)
        {
            PersonalVo personalVo=null;
            Database db;
            DbCommand getPersonalNetPositionCmd;
            DataSet dsGetPersonalNetPosition;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPersonalNetPositionCmd = db.GetStoredProcCommand("SP_GetPersonalNetPositionFromID");
                db.AddInParameter(getPersonalNetPositionCmd, "@CPNP_PersonalNPId", DbType.Int32, PersonalNetPositionId);

                dsGetPersonalNetPosition = db.ExecuteDataSet(getPersonalNetPositionCmd);
                if (dsGetPersonalNetPosition.Tables[0].Rows.Count > 0)
                {
                    dr = dsGetPersonalNetPosition.Tables[0].Rows[0];

                    personalVo = new PersonalVo();

                    personalVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    personalVo.PersonalPortfolioId = int.Parse(dr["CPNP_PersonalNPId"].ToString());
                    personalVo.AssetCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    personalVo.AssetSubCategoryCode = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                    personalVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                    personalVo.Name = dr["CPNP_Name"].ToString();
                    if (dr["CPNP_PurchaseDate"].ToString() != string.Empty)
                        personalVo.PurchaseDate = DateTime.Parse(dr["CPNP_PurchaseDate"].ToString());
                    else
                        personalVo.PurchaseDate = DateTime.MinValue;
                    personalVo.PurchasePrice = float.Parse(dr["CPNP_PurchasePrice"].ToString());
                    personalVo.Quantity = float.Parse(dr["CPNP_Quantity"].ToString());
                    personalVo.PurchaseValue = float.Parse(dr["CPNP_PurchaseValue"].ToString());
                    personalVo.CurrentPrice = float.Parse(dr["CPNP_CurrentPrice"].ToString());
                    personalVo.CurrentValue = float.Parse(dr["CPNP_CurrentValue"].ToString());
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

                FunctionInfo.Add("Method", "PersonalDao.cs:GetPersonalAsset()");


                object[] objects = new object[1];
                objects[0] = PersonalNetPositionId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return personalVo;

        }

        public bool UpdatePersonalNetPosition(PersonalVo personalVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updatePersonalNetPosition;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updatePersonalNetPosition = db.GetStoredProcCommand("SP_UpdatePersonalNetPosition");
                db.AddInParameter(updatePersonalNetPosition, "@CP_PortfolioId", DbType.Int32, personalVo.PortfolioId);
                db.AddInParameter(updatePersonalNetPosition, "@CPNP_PersonalNPId", DbType.Int32, personalVo.PersonalPortfolioId);
                db.AddInParameter(updatePersonalNetPosition, "@PAIC_AssetInstrumentCategoryCode", DbType.String, personalVo.AssetCategoryCode);
                db.AddInParameter(updatePersonalNetPosition, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, personalVo.AssetSubCategoryCode);
                db.AddInParameter(updatePersonalNetPosition, "@PAG_AssetGroupCode", DbType.String, personalVo.AssetGroupCode);
                db.AddInParameter(updatePersonalNetPosition, "@CPNP_Name", DbType.String, personalVo.Name);
                if (personalVo.PurchaseDate != DateTime.MinValue)
                    db.AddInParameter(updatePersonalNetPosition, "@CPNP_PurchaseDate", DbType.DateTime, personalVo.PurchaseDate);
                else
                    db.AddInParameter(updatePersonalNetPosition, "@CPNP_PurchaseDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updatePersonalNetPosition, "@CPNP_PurchasePrice", DbType.Decimal, personalVo.PurchasePrice);
                db.AddInParameter(updatePersonalNetPosition, "@CPNP_Quantity", DbType.Decimal, personalVo.Quantity);
                db.AddInParameter(updatePersonalNetPosition, "@CPNP_PurchaseValue", DbType.Decimal, personalVo.PurchaseValue);
                db.AddInParameter(updatePersonalNetPosition, "@CPNP_CurrentPrice", DbType.Decimal, personalVo.CurrentPrice);
                db.AddInParameter(updatePersonalNetPosition, "@CPNP_CurrentValue", DbType.Decimal, personalVo.CurrentValue);
                db.AddInParameter(updatePersonalNetPosition, "@CPNP_ModifiedBy", DbType.Int32, userId);
               if( db.ExecuteNonQuery(updatePersonalNetPosition)!=0)
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

                FunctionInfo.Add("Method", "PersonalDao.cs:UpdatePersonalNetPosition()");


                object[] objects = new object[2];
                objects[0] = personalVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeletePersonalPortfolio(int personalId)
        {
            bool bResult = false;

            Database db;
            DbCommand deletePersonalPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deletePersonalPortfolioCmd = db.GetStoredProcCommand("SP_DeletePersonalNetPostion");

                db.AddInParameter(deletePersonalPortfolioCmd, "@CPNP_PersonalNPId", DbType.Int32, personalId);

               if( db.ExecuteNonQuery(deletePersonalPortfolioCmd)!=0)
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
                FunctionInfo.Add("Method", "PersonalDao.cs:DeletePersonalPortfolio()");
                object[] objects = new object[1];
                objects[0] = personalId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public DataSet GetPersonalDropDown(string customerId)
        {
            Database db;
            DbCommand getPersonalDDLCmd;
            DataSet dsGetPersonalDDL;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPersonalDDLCmd = db.GetStoredProcCommand("SP_GetPersonalDropDown");
                db.AddInParameter(getPersonalDDLCmd, "@CustomerId", DbType.Int32, Int32.Parse(customerId));
                dsGetPersonalDDL = db.ExecuteDataSet(getPersonalDDLCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PropertyDao.cs:GetPersonalDropDown()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetPersonalDDL;
        }
    }
}
