using System;
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
        public void AddDematDetails(int customerId,int portfolioId, DematAccountVo demataccountvo, RMVo rmvo,ArrayList associationIdJH,ArrayList associationIdN,ArrayList lstassociatedtradeaccount)
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
        public DataSet GetDematDetails(int customerId,int dematId)
        {
            return demataccountdao.GetDematDetails(customerId,dematId);
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
        public void UpdateDematDetails(int customerId, int portfolioId, int dematId, DematAccountVo demataccountvo, RMVo rmvo, ArrayList associationIdJH, ArrayList associationIdN, ArrayList lstassociatedtradeaccount)
        {
            demataccountdao.UpdateDematDetails(customerId, portfolioId,dematId,demataccountvo, rmvo, associationIdJH, associationIdN, lstassociatedtradeaccount);
        }
        //public void UpdateAssociationTypesForDemat(int associationId, string associationtype)
        //{
        //    demataccountdao.UpdateAssociationTypesForDemat(associationId, associationtype);
        //}
        //public void UpdateTradeAccountForDemat(int accountId)
        //{
        //    demataccountdao.UpdateTradeAccountForDemat(accountId);
        //}
    }
}
