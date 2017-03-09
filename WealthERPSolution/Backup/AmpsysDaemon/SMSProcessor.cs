using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace AmpsysDaemon
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

                        Trace("Processing SMS " + Id.ToString());

                        try
                        {
                            Trace("Sending SMS " + Id.ToString());

                            Utils.SendSMS(Number, Message);

                            Trace("Updating SMS status " + Id.ToString());

                            Params = new SqlParameter[3];
                            Params[0] = new SqlParameter("Id", Id);
                            Params[0].DbType = DbType.Int32;
                            Params[1] = new SqlParameter("Status", 1);
                            Params[1].DbType = DbType.Int32;
                            Params[2] = new SqlParameter("ErrorMsg", "");
                            Params[2].DbType = DbType.String;

                            Utils.ExecuteNonQuery("sproc_UpdateOutgoingSMSStatus", Params);
                        }
                        catch (Exception Ex)
                        {
                            string Msg = "Unable to send SMS. " + Ex.ToString();
                            Utils.LogError(Msg);

                            Params = new SqlParameter[3];
                            Params[0] = new SqlParameter("Id", Id);
                            Params[0].DbType = DbType.Int32;
                            Params[1] = new SqlParameter("Status", 0);
                            Params[1].DbType = DbType.Int32;
                            Params[2] = new SqlParameter("ErrorMsg", Msg);
                            Params[2].DbType = DbType.String;

                            Utils.ExecuteNonQuery("sproc_UpdateOutgoingSMSStatus", Params);
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

        private void Trace(string Msg)
        {
            Utils.Trace(_DaemonCode + ": " + Msg);
        }
    }
}

