using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace AmpsysJobDaemon
{
    class JobPartProcessor
    {
        private static int _InstanceCount = 0;
        private string _DaemonCode = "";

        public JobPartProcessor()
        {
            _InstanceCount++;
            _DaemonCode = Environment.MachineName + "_JobPart_" + _InstanceCount.ToString();

            Trace("Starting job part processor...");
        }

        public void Start()
        {
            try
            {
                while (1 == 1)
                {
                    Trace("Polling DB for job parts...");

                    SqlParameter[] Params = new SqlParameter[1];
                    Params[0] = new SqlParameter("@DaemonCode", _DaemonCode);

                    DataSet DS = Utils.ExecuteDataSet("sproc_GetJobPartList", Params);

                    bool HasItemsToProcess = false;

                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        HasItemsToProcess = true;

                        int JobPartId = int.Parse(DR["Id"].ToString());
                        int JobInstanceId = int.Parse(DR["JobInstanceId"].ToString());
                        int JobId = int.Parse(DR["JobId"].ToString());
                        int JobTypeId = int.Parse(DR["JobTypeId"].ToString());
                        int JobPriority = int.Parse(DR["Priority"].ToString());
                        DateTime RunTime = DateTime.Parse(DR["RunTime"].ToString());
                        string JobParams = DR["Params"].ToString();

                        JobParams JP = new JobParams();
                        JP.Init(JobParams);

                        Trace("Processing job part " + JobPartId.ToString());

                        try
                        {
                            Trace("Updating job part status " + JobPartId.ToString());

                            Job J = JobMgr.GetJobByType(JobTypeId);

                            J.Init(JobId, JobInstanceId, JobPriority, RunTime, JobPartId);

                            string ErrorMsg;
                            JobPartStatus JPS = J.StartPart(JP, out ErrorMsg);

                            Params = new SqlParameter[3];
                            Params[0] = new SqlParameter("Id", JobPartId);
                            Params[1] = new SqlParameter("Status", JPS);
                            Params[2] = new SqlParameter("ErrorMsg", ErrorMsg);

                            Utils.ExecuteNonQuery("sproc_UpdateJobPartStatus", Params);
                        }
                        catch (Exception Ex)
                        {
                            string Msg = "Unable to process job part. " + Ex.ToString();
                            Utils.LogError(Msg);

                            Params = new SqlParameter[3];
                            Params[0] = new SqlParameter("Id", JobPartId);
                            Params[1] = new SqlParameter("Status", JobPartStatus.Failed);

                            Utils.ExecuteNonQuery("sproc_UpdateJobPartStatus", Params);
                        }
                    }

                    if (!HasItemsToProcess)
                    {
                        Trace("No job part items to process, waiting 5 minutes...");
                        Thread.Sleep(5 * 60 * 1000);
                    }
                }
            }
            catch (Exception Ex)
            {
                string Msg = "Unable to process job parts queue. " + Ex.ToString();
                Utils.LogError(Msg);
            }
        }

        private void Trace(string Msg)
        {
            Utils.Trace(_DaemonCode + ": " + Msg);
        }
    }
}

