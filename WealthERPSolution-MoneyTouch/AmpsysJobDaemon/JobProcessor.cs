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
    class JobProcessor
    {
        private static int _InstanceCount = 0;
        private string _DaemonCode = "";
        private ArrayList _ThreadList = new ArrayList();

        public JobProcessor()
        {
            _InstanceCount++;
            _DaemonCode = Environment.MachineName + "_Job_" + _InstanceCount.ToString();

            Trace("Starting job processor...");
        }

        public void Start()
        {
            try
            {
                while (1 == 1)
                {
                    Trace("Polling DB for jobs...");

                    SqlParameter[] Params = new SqlParameter[1];
                    Params[0] = new SqlParameter("@DaemonCode", _DaemonCode);

                    DataSet DS = Utils.ExecuteDataSet("sproc_GetJobList", Params);

                    bool HasItemsToProcess = false;

                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        HasItemsToProcess = true;

                        int JobInstanceId = int.Parse(DR["Id"].ToString());
                        int JobId = int.Parse(DR["JobId"].ToString());
                        int JobTypeId = int.Parse(DR["JobTypeId"].ToString());
                        int JobPriority = int.Parse(DR["Priority"].ToString());
                        DateTime RunTime = DateTime.Parse(DR["RunTime"].ToString());
                        string JobParams = DR["Params"].ToString();

                        object[] Data = new object[] { _DaemonCode, JobInstanceId, JobId, JobTypeId, JobPriority, RunTime, JobParams };

                        Thread T = new Thread(StartJob);
                        T.IsBackground = true;

                        T.Start(Data);

                        _ThreadList.Add(T);
                    }

                    if (HasItemsToProcess)
                    {
                        Trace("Waiting for jobs to complete...");
                        // started job threads, now wait for some of them to close before getting new jobs from database
                        while (1 == 1)
                        {
                            for (int i = _ThreadList.Count - 1; i >= 0; i--)
                            {
                                Thread T = (Thread)_ThreadList[i];
                                if (T.ThreadState == ThreadState.Stopped || T.ThreadState == ThreadState.Aborted)
                                {
                                    _ThreadList.RemoveAt(i);
                                }
                            }

                            if (_ThreadList.Count < 5) break;

                            Trace("Jobs in process, waiting 1 minute...");
                            Thread.Sleep(60 * 1000);
                        }

                        Trace("Eligible to process jobs, checking in DB...");
                    }
                    else
                    {
                        Trace("No job items to process, waiting 5 minutes...");
                        Thread.Sleep(5 * 60 * 1000);
                    }
                }
            }
            catch (Exception Ex)
            {
                string Msg = "Unable to process job queue. " + Ex.ToString();
                Utils.LogError(Msg);
            }
        }

        private void Trace(string Msg)
        {
            Utils.Trace(_DaemonCode + ": " + Msg);
        }

        private static void StartJob(object Data)
        {
            object[] DataArray = (object[])Data;

            string DaemonCode = (string)DataArray[0];
            int JobInstanceId = (int)DataArray[1];
            int JobId = (int)DataArray[2];
            int JobTypeId = (int)DataArray[3];
            int JobPriority = (int)DataArray[4];
            DateTime RunTime = (DateTime)DataArray[5];
            string JobParams = (string)DataArray[6];

            JobParams JP = new JobParams();
            JP.Init(JobParams);

            Utils.Trace(DaemonCode + ": " + "Processing job " + JobInstanceId.ToString());

            try
            {
                Job J = JobMgr.GetJobByType(JobTypeId);

                J.Init(JobId, JobInstanceId, JobPriority, RunTime);

                string ErrorMsg;
                JobStatus JS = J.Start(JP, out ErrorMsg);

                Utils.Trace(DaemonCode + ": " + "Updating job status " + JobInstanceId.ToString());

                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("Id", JobInstanceId);
                Params[1] = new SqlParameter("Status", JS);
                Params[2] = new SqlParameter("ErrorMsg", ErrorMsg);

                Utils.ExecuteNonQuery("sproc_UpdateJobStatus", Params);
            }
            catch (Exception Ex)
            {
                string Msg = "Unable to process job. " + Ex.ToString();
                Utils.LogError(Msg);

                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("Id", JobInstanceId);
                Params[1] = new SqlParameter("Status", JobStatus.Failed);
                Params[2] = new SqlParameter("ErrorMsg", Msg);

                Utils.ExecuteNonQuery("sproc_UpdateJobStatus", Params);
            }
        }
    }
}

