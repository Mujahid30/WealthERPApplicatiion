﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data.Common;
using DaoCustomerPortfolio;
using VoUser;
using VoCustomerPortfolio;
using System.Collections;

namespace BoCustomerPortfolio
{
    public class BoDematAccount
    {
        DematAccountDao demataccountdao = new DematAccountDao();

        public DataSet GetCustomerAccociation(CustomerVo customervo)
        {
            return demataccountdao.GetCustomerAccociation(customervo);
        }
        public DataSet GetTradeAccountNumber(CustomerVo customervo)
        {
            return demataccountdao.GetTradeAccountNumber(customervo);
        }
        public DataSet GetAvailableTrades(int customerId, int dematAccountId)
        {
            return demataccountdao.GetAvailableTrades(customerId, dematAccountId);
        }
        public DataSet GetXmlModeOfHolding()
        {
            return demataccountdao.GetXmlModeOfHolding();
        }
        public void AddDematDetails(int customerId, int portfolioId, DematAccountVo demataccountvo, RMVo rmvo, ArrayList associationIdJH, ArrayList associationIdN, ArrayList lstassociatedtradeaccount)
        {
            demataccountdao.AddDematDetails(customerId, portfolioId, demataccountvo, rmvo, associationIdJH, associationIdN, lstassociatedtradeaccount);
        }
        //public void AddAssociationTypesForDemat(int associationId,string associationtype)
        //{
        //    demataccountdao.AddAssociationTypesForDemat(associationId, associationtype);
        //}
        //public void AddTradeAccountForDemat(int accountId)
        //{
        //    demataccountdao.AddTradeAccountForDemat(accountId);
        //}
        // We are getting details for Viewing Demat details in Grid which is in DematAccountDetails.ascx
        public DataSet GetDematDetails(int customerId, int dematId)
        {
            return demataccountdao.GetDematDetails(customerId, dematId);
        }
        public DataSet GetJointHoldersAndNominees(int demataccountId)
        {
            return demataccountdao.GetJointHoldersAndNominees(demataccountId);
        }
        //We are getting Demat accountHolder details in DEmatDetails.ascx
        public DataSet GetDematAccountHolderDetails(int customerId)
        {
            return demataccountdao.GetDematAccountHolderDetails(customerId);
        }
        // We are getting details for Viewing Demat details in Seperate Page
        public DataSet GetDematAccountDetails(int demataccountId)
        {
            return demataccountdao.GetDematAccountDetails(demataccountId);
        }
        public DataTable GetDepositoryName()
        {
            return demataccountdao.GetDepositoryNames();
        }
        public void UpdateDematDetails(int customerId, int portfolioId, int dematId, DematAccountVo demataccountvo, RMVo rmvo, ArrayList associationIdJH, ArrayList associationIdN, ArrayList lstassociatedtradeaccount)
        {
            demataccountdao.UpdateDematDetails(customerId, portfolioId, dematId, demataccountvo, rmvo, associationIdJH, associationIdN, lstassociatedtradeaccount);
        }
        //public void UpdateAssociationTypesForDemat(int associationId, string associationtype)
        //{
        //    demataccountdao.UpdateAssociationTypesForDemat(associationId, associationtype);
        //}
        //public void UpdateTradeAccountForDemat(int accountId)
        //{
        //    demataccountdao.UpdateTradeAccountForDemat(accountId);
        //}
        public bool UpdateCustomerDematAccountAssociates(int associationId, int dematAccountId, string associateType, string name, string panNum, string sex, DateTime dob, int isKYC, string relationshipCode, int modifiedBy)
        {
            bool blResult = false;
            try
            {

                demataccountdao.UpdateCustomerDematAccountAssociates(associationId, dematAccountId, associateType, name, panNum, sex, dob, isKYC, relationshipCode, modifiedBy);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:UpdateCustomerMFFolioDetails()");
                object[] objects = new object[2];
                objects[0] = associationId;
                objects[1] = dematAccountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }
        public DataSet GetCustomerDematAccountAssociates(int dematAccountId)
        {
            DataSet datasetDematAssociates = null;
            try
            {
                demataccountdao.GetCustomerDematAccountAssociates(dematAccountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetCustomerDematAccountAssociates()");


                object[] objects = new object[1];
                objects[0] = dematAccountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return datasetDematAssociates;

        }
        public bool DeleteCustomerDematAccountAssociates(int associationId)
        {

            bool bResult = false;
            try
            {
                bResult = demataccountdao.DeleteCustomerDematAccountAssociates(associationId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:DeleteCustomerDematAccountAssociates()");

                object[] objects = new object[1];
                objects[0] = associationId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
        public void AddCustomerDematAccountAssociates(int associationId, int dematAccountId, string associateType, string name, string panNum, string sex, DateTime dob, int isKYC, string relationshipCode, int createdBy)
        {
            try
            {
                demataccountdao.AddCustomerDematAccountAssociates(associationId, dematAccountId, associateType, name, panNum, sex, dob, isKYC, relationshipCode, createdBy);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:DeleteCustomerDematAccountAssociates()");

                object[] objects = new object[1];
                objects[0] = associationId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

    }
}
