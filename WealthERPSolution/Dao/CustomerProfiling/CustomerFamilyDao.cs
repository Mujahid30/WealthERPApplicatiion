using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using VoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoCustomerProfiling
{
    public class CustomerFamilyDao
    {
        public bool CreateCustomerFamily(CustomerFamilyVo customerFamilyVo, int customerId, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createCustomerFamilyCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerFamilyCmd = db.GetStoredProcCommand("SP_CreateCustomerAssociates");
                //db.AddInParameter(createCustomerFamilyCmd, "@CA_AssociationId", DbType.String, customerFamilyVo.AssociationId);
                db.AddInParameter(createCustomerFamilyCmd, "@C_AssociateCustomerId", DbType.Int32, customerFamilyVo.AssociateCustomerId);
                db.AddInParameter(createCustomerFamilyCmd, "@XR_RelationshipCode", DbType.String, customerFamilyVo.Relationship);
                db.AddInParameter(createCustomerFamilyCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(createCustomerFamilyCmd, "@CA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createCustomerFamilyCmd, "@CA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createCustomerFamilyCmd) != 0)

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

                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:CreateCustomerFamily()");

                object[] objects = new object[3];
                objects[0] = customerFamilyVo;
                objects[1] = customerId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool UpdateCustomerAssociate(CustomerFamilyVo customerFamilyVo, int customerId, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand UpdateCustomerAssociateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateCustomerAssociateCmd = db.GetStoredProcCommand("SP_UpdateCustomerAssociates");
                db.AddInParameter(UpdateCustomerAssociateCmd, "@CA_AssociationId", DbType.String, customerFamilyVo.AssociationId);
                db.AddInParameter(UpdateCustomerAssociateCmd, "@C_AssociateCustomerId", DbType.String, customerFamilyVo.AssociateCustomerId);
                db.AddInParameter(UpdateCustomerAssociateCmd, "@XR_RelationshipCode", DbType.String, customerFamilyVo.Relationship);
                db.AddInParameter(UpdateCustomerAssociateCmd, "@C_CustomerId", DbType.Int32, customerFamilyVo.CustomerId);
                db.AddInParameter(UpdateCustomerAssociateCmd, "@CA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(UpdateCustomerAssociateCmd) != 0)

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

                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:UpdateCustomerAssociate()");

                object[] objects = new object[3];
                objects[0] = customerFamilyVo;
                objects[1] = customerId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public CustomerFamilyVo GetCustomerFamilyAssociateDetails(int AssociationId)
        {
            CustomerFamilyVo customerFamilyVo = new CustomerFamilyVo();
            Database db;
            DbCommand getCustomerFamilyAssocciateDetailsCmd;
            DataSet getCustomerFamilyAssocciateDetailsDs;
            DataRow dr;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerFamilyAssocciateDetailsCmd = db.GetStoredProcCommand("SP_GetCustomerFamilyAssociateDetails");
                db.AddInParameter(getCustomerFamilyAssocciateDetailsCmd, "@CA_Associationid", DbType.Int32, AssociationId);
                getCustomerFamilyAssocciateDetailsDs = db.ExecuteDataSet(getCustomerFamilyAssocciateDetailsCmd);

                if (getCustomerFamilyAssocciateDetailsDs.Tables[0].Rows.Count > 0)
                {

                    dr = getCustomerFamilyAssocciateDetailsDs.Tables[0].Rows[0];
                    customerFamilyVo = new CustomerFamilyVo();
                    customerFamilyVo.AssociationId = Int32.Parse(dr["CA_AssociationId"].ToString());
                    customerFamilyVo.AssociateCustomerId = Int32.Parse(dr["C_AssociateCustomerId"].ToString());
                    customerFamilyVo.Relationship = dr["XR_RelationshipCode"].ToString();
                    customerFamilyVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
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

                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:GetCustomerFamilyAssociateDetails()");


                object[] objects = new object[1];
                objects[0] = AssociationId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerFamilyVo;
        }

        public List<CustomerFamilyVo> GetCustomerFamily(int customerId)
        {
            List<CustomerFamilyVo> familyList = null;

            CustomerFamilyVo customerFamilyVo;
            Database db;
            DbCommand getCustomerFamilyCmd;
            DataSet getCustomerFamilyDs;
            //DataRow dr;

            try
            {
                //  string query = "getCustomerFamily" + customerId.ToString();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerFamilyCmd = db.GetStoredProcCommand("SP_GetCustomerAssociatesRel");
                db.AddInParameter(getCustomerFamilyCmd, "@C_CustomerId", DbType.Int32, customerId);
                getCustomerFamilyDs = db.ExecuteDataSet(getCustomerFamilyCmd);

                if (getCustomerFamilyDs.Tables[0].Rows.Count > 0)
                {
                    familyList = new List<CustomerFamilyVo>();
                    //dr = getCustomerFamilyDs.Tables[0].Rows[0];
                    foreach (DataRow dr in getCustomerFamilyDs.Tables[0].Rows)
                    {
                        customerFamilyVo = new CustomerFamilyVo();
                        customerFamilyVo.AssociationId = Int32.Parse(dr["CA_AssociationId"].ToString());
                        customerFamilyVo.CustomerId = Int32.Parse(dr["C_CustomerId"].ToString());
                        customerFamilyVo.RelationshipCode = dr["XR_RelationshipCode"].ToString();
                        customerFamilyVo.AssociateCustomerId = Int32.Parse(dr["C_AssociateCustomerId"].ToString());
                        customerFamilyVo.AssociateCustomerName = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                        if (!string.IsNullOrEmpty(dr["C_DOB"].ToString()))
                        {
                            customerFamilyVo.DOB = DateTime.Parse(dr["C_DOB"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dr["C_Email"].ToString()))
                        {
                            customerFamilyVo.EmailId = dr["C_Email"].ToString();
                        }
                        customerFamilyVo.FirstName = dr["C_FirstName"].ToString();
                        customerFamilyVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerFamilyVo.LastName = dr["C_LastName"].ToString();
                        customerFamilyVo.Relationship = dr["XR_Relationship"].ToString();
                        familyList.Add(customerFamilyVo);
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

                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:GetCustomerFamily()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return familyList;
        }

        public DataTable GetCustomerAssociations(int rmId, string nameSrchValue)
        {
            Database db;
            DbCommand cmdGetCustomerAssociations;
            DataSet dsCustomerAssociations;
            DataTable dtCustomerAssociations;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCustomerAssociations = db.GetStoredProcCommand("SP_GetCustomerAssociations");
                db.AddInParameter(cmdGetCustomerAssociations, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(cmdGetCustomerAssociations, "@nameFilter", DbType.String, nameSrchValue);
                dsCustomerAssociations = db.ExecuteDataSet(cmdGetCustomerAssociations);
                dtCustomerAssociations = dsCustomerAssociations.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:GetCustomerFamily()");


                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerAssociations;
        }

        public bool DeleteCustomerFamily(int CustomerAssociationID)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteCustomerFamilyCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteCustomerFamilyCmd = db.GetStoredProcCommand("SP_DeleteCustomerAssociates");
                db.AddInParameter(deleteCustomerFamilyCmd, "@CA_AssociationId", DbType.Int32, CustomerAssociationID);
                if (db.ExecuteNonQuery(deleteCustomerFamilyCmd) != 0)

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

                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:DeleteCustomerFamily()");

                object[] objects = new object[1];
                objects[0] = CustomerAssociationID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public DataTable GetCustomerAssociates(int customerId)
        {



            Database db;
            DbCommand getCustomerFamilyCmd;
            DataSet getCustomerFamilyDs;
            DataTable dtCustomerFamily = null;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerFamilyCmd = db.GetStoredProcCommand("SP_LoanSchemeGetCustomerAssociate");
                db.AddInParameter(getCustomerFamilyCmd, "@C_CustomerId", DbType.Int32, customerId);
                getCustomerFamilyDs = db.ExecuteDataSet(getCustomerFamilyCmd);
                if (getCustomerFamilyDs.Tables[0].Rows.Count > 0)
                {
                    dtCustomerFamily = getCustomerFamilyDs.Tables[0];
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
                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:GetCustomerAssociates()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerFamily;
        }

        /// <summary>
        /// Returns the Customer Associates including the Group head.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable GetAllCustomerAssociates(int customerId)
        {



            Database db;
            DbCommand getCustomerFamilyCmd;
            DataSet getCustomerFamilyDs;
            DataTable dtCustomerFamily = null;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerFamilyCmd = db.GetStoredProcCommand("SP_GetAllCustomerAssociates");
                db.AddInParameter(getCustomerFamilyCmd, "@C_CustomerId", DbType.Int32, customerId);
                getCustomerFamilyDs = db.ExecuteDataSet(getCustomerFamilyCmd);
                if (getCustomerFamilyDs.Tables[0].Rows.Count > 0)
                {
                    dtCustomerFamily = getCustomerFamilyDs.Tables[0];
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
                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:GetCustomerAssociates()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerFamily;
        }
        public DataTable GetCustomerAssociateDetails(int associationId)
        {
            Database db;
            DbCommand getAssociateDetailsCmd;
            DataSet getAssociateDetailsDs;
            DataTable dtAssociateDetails = null;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssociateDetailsCmd = db.GetStoredProcCommand("SP_GetAssociateDetails");
                db.AddInParameter(getAssociateDetailsCmd, "@CA_AssocaitionId", DbType.Int32, associationId);
                getAssociateDetailsDs = db.ExecuteDataSet(getAssociateDetailsCmd);
                if (getAssociateDetailsDs.Tables[0].Rows.Count > 0)
                {
                    dtAssociateDetails = getAssociateDetailsDs.Tables[0];
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
                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:GetCustomerAssociates()");
                object[] objects = new object[1];
                objects[0] = associationId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtAssociateDetails;
        }

        public int GetCustomersAssociationId(int customerId)
        {
            Database db;
            DbCommand getCustomerAssoIdCmd;
            DataSet getCustomerAssoIdDs;
            int AssociationId = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssoIdCmd = db.GetStoredProcCommand("SP_GetCustomersAssociationId");
                db.AddInParameter(getCustomerAssoIdCmd, "@C_CustomerId", DbType.Int32, customerId);
                getCustomerAssoIdDs = db.ExecuteDataSet(getCustomerAssoIdCmd);
                if (getCustomerAssoIdDs.Tables[0].Rows.Count > 0)
                {
                    AssociationId = Int32.Parse(getCustomerAssoIdDs.Tables[0].Rows[0]["AssoID"].ToString());
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
                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:GetCustomerAssociates()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return AssociationId;
        }

        public bool CustomerAssociateUpdate(int customerId, int associateId, string relCode, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand cmdUpdateCustomerAssociate;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCustomerAssociate = db.GetStoredProcCommand("SP_CustomerAssociatesUpdate");
                db.AddInParameter(cmdUpdateCustomerAssociate, "@C_AssociateCustomerId", DbType.String, associateId);
                db.AddInParameter(cmdUpdateCustomerAssociate, "@XR_RelationshipCode", DbType.String, relCode);
                db.AddInParameter(cmdUpdateCustomerAssociate, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(cmdUpdateCustomerAssociate, "@CA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdUpdateCustomerAssociate) != 0)

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

                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:CustomerAssociateUpdate()");

                object[] objects = new object[4];
                objects[0] = customerId;
                objects[1] = associateId;
                objects[2] = relCode;
                objects[3] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public int CustomerFamilyDissociation(string association)
        {

            Database db;
            DbCommand getCustomerListCmd;
            int associationcount = 0;
            
           

            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCustomerListCmd = db.GetStoredProcCommand("SP_CustomerRelationCount");
            db.AddInParameter(getCustomerListCmd, "@GaolIds", DbType.String, association);
            db.AddOutParameter(getCustomerListCmd, "@CountFlag", DbType.Int32, 0);
            associationcount = db.ExecuteNonQuery(getCustomerListCmd);
            associationcount = (int)db.GetParameterValue(getCustomerListCmd, "@CountFlag");
            if (associationcount != 1)
                return 2;
            else return 1;

        }

        public int CustomerDissociate(string association,int UserId)
        {

            Database db;
            DbCommand getCustomerListCmd;
            int associationcount = 0;



            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCustomerListCmd = db.GetStoredProcCommand("SP_CustomerDissociate");
            db.AddInParameter(getCustomerListCmd, "@GaolIds", DbType.String, association);
            db.AddInParameter(getCustomerListCmd, "@UserID", DbType.Int32, UserId);
            db.AddOutParameter(getCustomerListCmd, "@CountFlag", DbType.Int32, 0);
            associationcount = db.ExecuteNonQuery(getCustomerListCmd);
            associationcount = (int)db.GetParameterValue(getCustomerListCmd, "@CountFlag");
            if (associationcount != 1)
                return 2;
            else return 1;

        }
    
    }
}
