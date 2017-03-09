using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAlerts;
using BoCommon;

namespace WealthERP.Alerts
{
    public partial class AlertServicesExecution : System.Web.UI.UserControl
    {
        AlertsBo alertsBo = new AlertsBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
        }

        protected void btnDateService_Click(object sender, EventArgs e)
        {
            alertsBo.ExecuteReminderAlert();
        }

        protected void btnDataConditionService_Click(object sender, EventArgs e)
        {
            alertsBo.ExecuteOccurrenceAlert();
        }

        protected void btnTransactionService_Click(object sender, EventArgs e)
        {
            alertsBo.ExecuteConfirmationAlert();
        }
    }
}