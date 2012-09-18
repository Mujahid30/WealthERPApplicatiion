using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace WERP_EMAIL_SMS_JOB
{
    class SMSProcessor
    {
        private static int _InstanceCount = 0;
        private string _DaemonCode = "";
        private int _InterMessageDelay = int.Parse(ConfigurationSettings.AppSettings["SMSInterMessageDelay"]) * 1000;

        public SMSProcessor()
        {
            _InstanceCount++;
            _DaemonCode = Environment.MachineName + "_SMS_" + _InstanceCount.ToString();

            Trace("Starting SMS processor...");
        }

        public void Start()
        {
            try
            {
                while (1 == 1)
                {
                    Trace("Polling DB for SMS...");

                    SqlParameter[] Params = new SqlParameter[1];
                    Params[0] = new SqlParameter("@DaemonCode", _DaemonCode);
                    Params[0].DbType = DbType.String;
                    DataSet DS = Utils.ExecuteDataSet("sproc_GetOutgoingSMSList", Params);

                    bool HasItemsToProcess = false;

                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        HasItemsToProcess = true;

                        int Id = int.Parse(DR["Id"].ToString());
                        string Number = DR["Number"].ToString();
                        string Message = DR["Message"].ToString();
                        int adviserId = Convert.ToInt32(DR["AdviserId"].ToString());
                        string smsAPI = string.Empty;
                        string userName = string.Empty;
                        string password = string.Empty;
                        string senderId = string.Empty;
                        string senderName = string.Empty;
                        int smsCreditLeft = 0;

                        Dictionary<string, string> SMSDetails = new Dictionary<string, string>();
                        Trace("Processing SMS " + Id.ToString());

                        Params = new SqlParameter[1];
                        Params[0] = new SqlParameter("adviserId", adviserId);
                        Params[0].DbType = DbType.Int32;
                        DataSet dsAdviserSMSAccountDetails = Utils.ExecuteDataSet("sproc_GetAdviserSMSAccountDetails", Params);
                        if (dsAdviserSMSAccountDetails.Tables[0].Rows.Count > 0)
                        {
                            smsAPI = dsAdviserSMSAccountDetails.Tables[0].Rows[0]["WERPSMSPM_URL"].ToString();
                            userName = dsAdviserSMSAccountDetails.Tables[0].Rows[0]["ASMSP_Username"].ToString();
                            password = dsAdviserSMSAccountDetails.Tables[0].Rows[0]["ASMSP_Password"].ToString();
                            senderId = dsAdviserSMSAccountDetails.Tables[0].Rows[0]["ASMSP_SenderId"].ToString();
                            senderName = dsAdviserSMSAccountDetails.Tables[0].Rows[0]["ASMSP_SenderName"].ToString();
                            smsCreditLeft = Convert.ToInt32(dsAdviserSMSAccountDetails.Tables[0].Rows[0]["ASMSP_CreditLeft"].ToString());

                            if (smsCreditLeft > 0)
                            {

                                SMSDetails.Add("SMSAPI", smsAPI);
                                SMSDetails.Add("USERNAME", userName);
                                SMSDetails.Add("PASSWORD", password);

                                SMSDetails.Add("SENDERID", senderId);
                                SMSDetails.Add("SENDERNAME", senderName);

                                SMSDetails.Add("NUMBER", Number);
                                SMSDetails.Add("MESSAGE", Message);


                                try
                                {
                                    Trace("Sending SMS " + Id.ToString());
                                    string message = string.Empty;
                                    message = Utils.SendSMS(SMSDetails);

                                    Trace("Updating SMS status " + Id.ToString());

                                    Params = new SqlParameter[4];
                                    Params[0] = new SqlParameter("Id", Id);
                                    Params[0].DbType = DbType.Int32;
                                    Params[1] = new SqlParameter("Status", 1);
                                    Params[1].DbType = DbType.Int32;
                                    Params[2] = new SqlParameter("ErrorMsg", message);
                                    Params[2].DbType = DbType.String;
                                    Params[3] = new SqlParameter("adviserId", adviserId);
                                    Params[3].DbType = DbType.Int32;
                                    Utils.ExecuteNonQuery("sproc_UpdateOutgoingSMSStatus", Params);
                                }
                                catch (Exception Ex)
                                {
                                    string Msg = "Unable to send SMS. " + Ex.ToString();
                                    Utils.LogError(Msg);

                                    Params = new SqlParameter[4];
                                    Params[0] = new SqlParameter("Id", Id);
                                    Params[0].DbType = DbType.Int32;
                                    Params[1] = new SqlParameter("Status", 0);
                                    Params[1].DbType = DbType.Int32;
                                    Params[2] = new SqlParameter("ErrorMsg", Msg);
                                    Params[2].DbType = DbType.String;
                                    Params[3] = new SqlParameter("adviserId", adviserId);
                                    Params[3].DbType = DbType.Int32;

                                    Utils.ExecuteNonQuery("sproc_UpdateOutgoingSMSStatus", Params);
                                }

                                Thread.Sleep(_InterMessageDelay);
                            }
                            else
                            {
                                Params = new SqlParameter[4];
                                Params[0] = new SqlParameter("Id", Id);
                                Params[0].DbType = DbType.Int32;
                                Params[1] = new SqlParameter("Status", 2);
                                Params[1].DbType = DbType.Int32;
                                Params[2] = new SqlParameter("ErrorMsg", "No_Credit_Left");
                                Params[2].DbType = DbType.String;
                                Params[3] = new SqlParameter("adviserId", adviserId);
                                Params[3].DbType = DbType.Int32;

                                Utils.ExecuteNonQuery("sproc_UpdateOutgoingSMSStatus", Params);
                            }

                        }
                        else
                        {
                            Params = new SqlParameter[4];
                            Params[0] = new SqlParameter("Id", Id);
                            Params[0].DbType = DbType.Int32;
                            Params[1] = new SqlParameter("Status", 2);
                            Params[1].DbType = DbType.Int32;
                            Params[2] = new SqlParameter("ErrorMsg", "No_SMS_Account_Found");
                            Params[2].DbType = DbType.String;
                            Params[3] = new SqlParameter("adviserId", adviserId);
                            Params[3].DbType = DbType.Int32;

                            Utils.ExecuteNonQuery("sproc_UpdateOutgoingSMSStatus", Params);

                        }


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

        private void Trace(string Msg)
        {
            Utils.Trace(_DaemonCode + ": " + Msg);
        }
    }
}
