using System;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using System.Collections.Specialized;
using DaoAdvisorProfiling;
using VoAdvisorProfiling;

namespace BoAdvisorProfiling
{
    public class AdviserLoanCommsnStrucWithLoanPartnerBo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdviserId"></param>
        /// <returns></returns>
        public DataSet GetAdvisorLoanPartnerCommissionForAdviser(int AdviserId)
        {
            DataSet ds = null;

            AdviserLoanCommsnStrucWithLoanPartnerDao AdviserLoanCommsnStrucWithLoanPartner = new AdviserLoanCommsnStrucWithLoanPartnerDao();

            try
            {

                ds = AdviserLoanCommsnStrucWithLoanPartner.GetAdvisorLoanPartnerCommissionForAdviser(AdviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserLoanCommsnStrucWithLoanPartnerDao.cs:GetAdviserAssociateCategory()");
                object[] objects = new object[1];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return ds;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LoanType"></param>
        /// <param name="LoanPartner"></param>
        /// <param name="AdviserId"></param>
        /// <returns></returns>
        public DataSet GetAdviserLoanSchemeNameForLnPtnrLnType(int LoanType, int LoanPartner, int AdviserId)
        {
            DataSet ds = null;

            AdviserLoanCommsnStrucWithLoanPartnerDao AdviserLoanCommsnStrucWithLoanPartner = new AdviserLoanCommsnStrucWithLoanPartnerDao();

            try
            {

                ds = AdviserLoanCommsnStrucWithLoanPartner.GetAdviserLoanSchemeNameForLnPtnrLnType(LoanType, LoanPartner, AdviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserLoanCommsnStrucWithLoanPartnerDao.cs:GetAdviserLoanSchemeNameForLnPtnrLnType()");
                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = LoanType;
                objects[2] = LoanPartner;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdviserLoanCommsnStrucWithLoanPartner"></param>
        /// <returns></returns>
        public bool InsertAdviserLoanSchemeNameForLnPtnrLnType(AdviserLoanCommsnStrucWithLoanPartnerVo AdviserLoanCommsnStrucWithLoanPartner)
        {
            bool result = false;
            AdviserLoanCommsnStrucWithLoanPartnerDao AdviserLoanCommsnStrucWithLoanPartnerdao = new AdviserLoanCommsnStrucWithLoanPartnerDao();
            try
            {

                result = AdviserLoanCommsnStrucWithLoanPartnerdao.InsertAdviserLoanSchemeNameForLnPtnrLnType(AdviserLoanCommsnStrucWithLoanPartner);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserLoanCommsnStrucWithLoanPartnerBo.cs:InsertAdviserLoanSchemeNameForLnPtnrLnType()");
                object[] objects = new object[1];
                objects[0] = AdviserLoanCommsnStrucWithLoanPartner;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdviserLoanCommsnStrucWithLoanPartner"></param>
        /// <returns></returns>
        public bool UpdateAdviserLoanSchemeNameForLnPtnrLnType(AdviserLoanCommsnStrucWithLoanPartnerVo AdviserLoanCommsnStrucWithLoanPartner)
        {
            bool result = false;
            AdviserLoanCommsnStrucWithLoanPartnerDao AdviserLoanCommsnStrucWithLoanPartnerdao = new AdviserLoanCommsnStrucWithLoanPartnerDao();
            try
            {

                result = AdviserLoanCommsnStrucWithLoanPartnerdao.UpdateAdvisorLoanPartnerCommissionForAdviser(AdviserLoanCommsnStrucWithLoanPartner);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserLoanCommsnStrucWithLoanPartnerBo.cs:UpdateAdviserLoanSchemeNameForLnPtnrLnType()");
                object[] objects = new object[1];
                objects[0] = AdviserLoanCommsnStrucWithLoanPartner;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdvisorLoanPartnerCommissionId"></param>
        /// <returns></returns>
        public bool DeleteAdvisorLoanPartnerCommission(int AdvisorLoanPartnerCommissionId)
        {
            bool result = false;
            AdviserLoanCommsnStrucWithLoanPartnerDao AdviserLoanCommsnStrucWithLoanPartnerdao = new AdviserLoanCommsnStrucWithLoanPartnerDao();
            try
            {

                result = AdviserLoanCommsnStrucWithLoanPartnerdao.DeleteAdvisorLoanPartnerCommission(AdvisorLoanPartnerCommissionId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserLoanCommsnStrucWithLoanPartnerBo.cs:DeleteAdvisorLoanPartnerCommission()");
                object[] objects = new object[1];
                objects[0] = AdvisorLoanPartnerCommissionId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return result;
        }

    }
}
