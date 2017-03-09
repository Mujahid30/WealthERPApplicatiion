using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AmpsysJobDaemon
{
    class Job
    {
        protected int _JobId = 0;
        protected int _JobInstanceId = 0;
        protected int _JobPartId = 0;
        protected int _Priority = 1;
        protected DateTime _RunTime = new DateTime(1990, 1, 1);

        public void Init(int JobId, int JobInstanceId, int Priority, DateTime RunTime)
        {
            _JobId = JobId;
            _JobInstanceId = JobInstanceId;
            _Priority = Priority;
            _RunTime = RunTime;
        }

        public void Init(int JobId, int JobInstanceId, int Priority, DateTime RunTime, int JobPartId)
        {
            _JobId = JobId;
            _JobInstanceId = JobInstanceId;
            _Priority = Priority;
            _RunTime = RunTime;
            _JobPartId = JobPartId;
        }

        public virtual JobStatus Start(JobParams JP, out string ErrorMsg)
        {
            ErrorMsg = "";

            return JobStatus.SuccessFull;
        }

        public virtual JobPartStatus StartPart(JobParams JP, out string ErrorMsg)
        {
            ErrorMsg = "";

            return JobPartStatus.Success;
        }

        public void AddJob(int JobId, int Priority, string JobParams)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[4];
                Params[0] = new SqlParameter("@JobId", JobId);
                Params[1] = new SqlParameter("@RunTime", DateTime.Now);
                Params[2] = new SqlParameter("@Priority", Priority);
                Params[3] = new SqlParameter("@Params", JobParams);

                Utils.ExecuteDataSet("sproc_CreateJob", Params);
            }
            catch (Exception Ex)
            {
                throw new Exception("Failed to create job: " + Ex.ToString());
            }
        }

        public void AddJobPart(int Priority, string JobParams)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@JobInstanceId", _JobInstanceId);
                Params[1] = new SqlParameter("@Priority", Priority);
                Params[2] = new SqlParameter("@Params", JobParams);

                Utils.ExecuteDataSet("sproc_CreateJobPart", Params);
            }
            catch (Exception Ex)
            {
                throw new Exception("Failed to create job part: " + Ex.ToString());
            }
        }
    }
}
