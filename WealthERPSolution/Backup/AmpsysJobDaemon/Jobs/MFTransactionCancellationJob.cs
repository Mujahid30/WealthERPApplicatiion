using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace AmpsysJobDaemon
{
    class MFTransactionCancellationJob:Job
    {
        public override JobStatus Start(JobParams JP, out string ErrorMsg)
        {
            ErrorMsg = "";
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            try
            {
                customerTransactionBo.RunMFTRansactionsCancellationJob();
            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
                
                
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DailyValuation.ascx.cs:CreateAdviserEODLog()");
                object[] objects = new object[1];
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
                
            }

            return JobStatus.SuccessFull;
            
        }
    }
}
