using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoAlerts;

namespace AmpsysJobDaemon
{
    class AlertServicesTriggerJob:Job
    {
        public override JobStatus Start(JobParams JP, out string ErrorMsg)
        {
            AlertsBo alertBo = new AlertsBo();

            alertBo.ExecuteReminderAlert();

            alertBo.ExecuteConfirmationAlert();

            //alertBo.ExecuteOccurrenceAlert();

            //alertBo.ExecuteProcessAlertstoEmailQueue();

            ErrorMsg = "";
            return JobStatus.SuccessFull;
           
        }
    }
}
