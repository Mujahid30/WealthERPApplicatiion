using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Net.Mail;
using System.Net;


namespace WERP_EMAIL_SMS_JOB
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
                    DataTable dtEmailParameterValue = new DataTable();

                    DataTable dtWERPEmailTemplateParameter = new DataTable();
                    DataTable dtAdviserEmailTemplate = new DataTable();
                    DataTable dtAdviserEmailTemplateParameterPre = new DataTable();

                    SqlParameter[] Params = new SqlParameter[1];
                    Params[0] = new SqlParameter("@DaemonCode", _DaemonCode);
                    Params[0].DbType = DbType.String;
                    DataSet DS = Utils.ExecuteDataSet("sproc_GetOutgoingEmailList", Params);
                    Trace("# of Records Retrieved : " + DS.Tables[0].Rows.Count.ToString());
                    bool HasItemsToProcess = false;
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        bool isHTMLBody = Convert.ToBoolean(DS.Tables[0].Rows[0]["IsHTMLBody"]);
                        tempAdviserId = Convert.ToInt32(DS.Tables[0].Rows[0]["A_AdviserId"].ToString());
                        adviserId = tempAdviserId;
                        dtAdviserSMTP = getAdviserSMTPDetails(adviserId);
                        if (isHTMLBody)
                        {
                            DataSet dsAdviserEmailTemplate = new DataSet();
                            dsAdviserEmailTemplate = GetAdviserHTMLTemplateParameter(adviserId);

                            if (dsAdviserEmailTemplate.Tables.Count == 3)
                            {
                                dtWERPEmailTemplateParameter = dsAdviserEmailTemplate.Tables[0];
                                dtAdviserEmailTemplate = dsAdviserEmailTemplate.Tables[1];
                                dtAdviserEmailTemplateParameterPre = dsAdviserEmailTemplate.Tables[2];
                            }
                        }
                    }
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        dtEmailParameterValue = DS.Tables[1];
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
                        string emailTypeCode = DR["WERPETM_TypeCode"].ToString();
                        string sourceId = DR["SourceId"].ToString();
                        bool HasAttachment = Convert.ToBoolean(DR["HasAttachment"]);
                        bool isHMTLTemplateBody = Convert.ToBoolean(DR["IsHTMLBody"]);
                        if (tempAdviserId != Convert.ToInt32(DR["A_AdviserId"].ToString()))
                        {
                            adviserId = Convert.ToInt32(DR["A_AdviserId"].ToString());

                            if (dtWERPEmailTemplateParameter.Rows.Count == 0 || isHMTLTemplateBody == true)
                            {
                                DataSet dsAdviserEmailTemplate = new DataSet();
                                dsAdviserEmailTemplate = GetAdviserHTMLTemplateParameter(adviserId);

                                if (dsAdviserEmailTemplate.Tables.Count == 3)
                                {
                                    dtWERPEmailTemplateParameter = dsAdviserEmailTemplate.Tables[0];
                                    dtAdviserEmailTemplate = dsAdviserEmailTemplate.Tables[1];
                                    dtAdviserEmailTemplateParameterPre = dsAdviserEmailTemplate.Tables[2];
                                }
                            }
                        }
                        DataSet dsEmailTemplateDetails = new DataSet();
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
                            if (isHMTLTemplateBody)
                            {
                                DataView dvMailParamerValues = new DataView(dtEmailParameterValue, "WR_RequestId='" + sourceId + "'", "WR_RequestId", DataViewRowState.CurrentRows);
                                DataView dvWERPParameter = new DataView(dtWERPEmailTemplateParameter, "WERPTTM_TypeCode='" + emailTypeCode + "'", "WERPTTM_TypeCode", DataViewRowState.CurrentRows);
                                DataView dvAdviserEmailTemplate = new DataView(dtAdviserEmailTemplate, "WERPTTM_TypeCode='" + emailTypeCode + "'", "WERPTTM_TypeCode", DataViewRowState.CurrentRows);
                                dsEmailTemplateDetails.Tables.Add(dvMailParamerValues.ToTable());
                                dsEmailTemplateDetails.Tables[0].TableName = "MailParameterValues";
                                dsEmailTemplateDetails.Tables.Add(dvWERPParameter.ToTable());
                                dsEmailTemplateDetails.Tables[1].TableName = "EmailTemplateParameterList";
                                dsEmailTemplateDetails.Tables.Add(dvAdviserEmailTemplate.ToTable());
                                dsEmailTemplateDetails.Tables[2].TableName = "AdviserEmailTemplateList";
                                dsEmailTemplateDetails.Tables.Add(dtAdviserEmailTemplateParameterPre.Copy());
                                dsEmailTemplateDetails.Tables[3].TableName = "AdviserEmailTemplateParameterPreference";


                            }

                            string fromSMTPEmail = string.Empty;
                            string statusMessage = string.Empty;
                            if (isHMTLTemplateBody)
                                Emailer.SendMail(To, Cc, Bcc, Subject, Body, Attachments, emailFrom, emailTypeCode, dtAdviserSMTP, out fromSMTPEmail, dsEmailTemplateDetails, out statusMessage);
                            else
                                Emailer.SendMail(To, Cc, Bcc, Subject, Body, Attachments, emailFrom, dtAdviserSMTP, out fromSMTPEmail);

                            
                            Trace("Updating email status " + Id.ToString());

                            Params = new SqlParameter[4];
                            Params[0] = new SqlParameter("Id", Id);
                            Params[0].DbType = DbType.Int32;
                            Params[1] = new SqlParameter("Status", 1);
                            Params[1].DbType = DbType.Int32;
                            Params[2] = new SqlParameter("ErrorMsg", "");
                            Params[2].DbType = DbType.String;
                            Params[3] = new SqlParameter("@FromSMTPEmail", fromSMTPEmail);
                            Params[3].DbType = DbType.String;

                            Utils.ExecuteNonQuery("sproc_UpdateOutgoingEmailStatus", Params);

                            if (emailTypeCode == "RT")
                            {
                                UpdateBulkMailRequestLog(Convert.ToInt32(sourceId), "SUCCESS",1);

                            }
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

                            if (emailTypeCode == "RT")
                            {
                                UpdateBulkMailRequestLog(Convert.ToInt32(sourceId), Msg,0);

                            }
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

        public DataSet GetAdviserHTMLTemplateParameter(int adviserId)
        {

            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@AdviserId", adviserId);
            Params[0].DbType = DbType.Int32;
            DataSet dsSMTPDetails = Utils.ExecuteDataSet("SPROC_GetAdviserHTMLTemplateParameter", Params);
            return dsSMTPDetails;

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

        private void UpdateBulkMailRequestLog(Int32 requestId, string message,int requestStatus)
        {
            SqlParameter[] Params = new SqlParameter[3];
            Params[0] = new SqlParameter("RequestId", requestId);
            Params[0].DbType = DbType.Int32;
            Params[1] = new SqlParameter("Message", message);
            Params[1].DbType = DbType.String;
            Params[2] = new SqlParameter("RequestStatus", requestStatus);
            Params[2].DbType = DbType.Int16;
            Utils.ExecuteNonQuery("SPROC_UpdateBulkMailRequestStatus", Params);
 
        }

        private void Trace(string Msg)
        {
            Utils.Trace(_DaemonCode + ": " + Msg);
        }


    }
}
