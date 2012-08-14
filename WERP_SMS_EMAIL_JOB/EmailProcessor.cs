using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Collections;


namespace WERP_SMS_EMAIL_JOB
{
    class EmailProcessor
    {
        private static int _InstanceCount = 0;
        private string _DaemonCode = "";
        private int _InterMessageDelay = int.Parse(ConfigurationSettings.AppSettings["SMTPInterMessageDelay"]) * 1000;
        int adviserId = 0;
        int tempAdviserId = 0;
        public EmailProcessor()
        {
            _InstanceCount++;
            _DaemonCode = Environment.MachineName + "_Email_" + _InstanceCount.ToString();

            Trace("Starting email processor...");
        }

        public void Start()
        {
            try
            {
                while (1 == 1)
                {
                    Trace("Polling DB for emails...");

                    DataTable dtAdviserSMTP = new DataTable();
                    SqlParameter[] Params = new SqlParameter[1];
                    Params[0] = new SqlParameter("@DaemonCode", _DaemonCode);
                    Params[0].DbType = DbType.String;
                    DataSet DS = Utils.ExecuteDataSet("sproc_GetOutgoingEmailList", Params);
                    Trace("# of Records Retrieved : " + DS.Tables[0].Rows.Count.ToString());
                    bool HasItemsToProcess = false;
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        tempAdviserId = Convert.ToInt32(DS.Tables[0].Rows[0]["A_AdviserId"].ToString());
                        adviserId = tempAdviserId;
                        dtAdviserSMTP = getAdviserSMTPDetails(adviserId);
                    }
                   
                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        HasItemsToProcess = true;

                        int Id = int.Parse(DR["Id"].ToString());
                        string To = DR["To"].ToString();
                        string Cc = DR["Cc"].ToString();
                        string Bcc = DR["Bcc"].ToString();
                        string Subject = DR["Subject"].ToString();
                        string Body = DR["Body"].ToString();
                        bool HasAttachment = Convert.ToBoolean(DR["HasAttachment"]);
                        if (tempAdviserId != Convert.ToInt32(DR["A_AdviserId"].ToString()))
                        {
                            adviserId = Convert.ToInt32(DR["A_AdviserId"].ToString());
                        }

                        string emailFrom = DR["From"].ToString();
                        Trace("Processing email " + Id.ToString());

                        try
                        {
                            ArrayList Attachments = new ArrayList();

                            if (HasAttachment)
                            {
                                Trace("Fetching attachments for " + Id.ToString());

                                Params = new SqlParameter[1];
                                Params[0] = new SqlParameter("OutgoingEmailId", Id);
                                Params[0].DbType = DbType.Int32;
                                DataSet DSA = Utils.ExecuteDataSet("sproc_GetOutgoingEmailAttachment", Params);

                                foreach (DataRow DRA in DSA.Tables[0].Rows)
                                {
                                    Attachments.Add(DRA["Path"]);
                                }
                            }

                            Trace("Sending mail " + Id.ToString());
                            if (tempAdviserId != adviserId)
                            {

                                dtAdviserSMTP = getAdviserSMTPDetails(adviserId);
                                tempAdviserId = adviserId;
                            }

                            Utils.SendMail(To, Cc, Bcc, Subject, Body, Attachments, emailFrom, dtAdviserSMTP);

                            Trace("Updating email status " + Id.ToString());

                            Params = new SqlParameter[3];
                            Params[0] = new SqlParameter("Id", Id);
                            Params[0].DbType = DbType.Int32;
                            Params[1] = new SqlParameter("Status", 1);
                            Params[1].DbType = DbType.Int32;
                            Params[2] = new SqlParameter("ErrorMsg", "");
                            Params[2].DbType = DbType.String;

                            Utils.ExecuteNonQuery("sproc_UpdateOutgoingEmailStatus", Params);
                        }
                        catch (Exception Ex)
                        {
                            string Msg = "Unable to send email. " + Ex.ToString();
                            Utils.LogError(Msg);

                            Params = new SqlParameter[3];
                            Params[0] = new SqlParameter("Id", Id);
                            Params[0].DbType = DbType.Int32;
                            Params[1] = new SqlParameter("Status", 0);
                            Params[1].DbType = DbType.Int32;
                            Params[2] = new SqlParameter("ErrorMsg", Msg);
                            Params[2].DbType = DbType.String;

                            Utils.ExecuteNonQuery("sproc_UpdateOutgoingEmailStatus", Params);
                        }

                        Thread.Sleep(_InterMessageDelay);
                    }

                    if (!HasItemsToProcess)
                    {
                        Trace("No items to process, waiting 5 minutes...");
                        Thread.Sleep(5 * 60 * 1000);
                    }
                }
            }
            catch (Exception Ex)
            {
                string Msg = "Unable to process queue. " + Ex.ToString();
                Utils.LogError(Msg);
            }
        }

        private DataTable getAdviserSMTPDetails(int adviserId)
        {
            DataTable dtAdviserSMTP = new DataTable();
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@A_AdviserId", adviserId);
            Params[0].DbType = DbType.Int32;
            DataSet dsSMTPDetails = Utils.ExecuteDataSet("SP_GetAdviserSMTPSettings", Params);
            dtAdviserSMTP = dsSMTPDetails.Tables[0];
            return dtAdviserSMTP;
        }

        private void Trace(string Msg)
        {
            Utils.Trace(_DaemonCode + ": " + Msg);
        }
    }
}
