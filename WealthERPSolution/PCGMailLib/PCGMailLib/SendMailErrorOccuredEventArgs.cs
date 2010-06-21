using System;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Xml.Serialization;
namespace PCGMailLib
{
  public  class SendMailErrorOccuredEventArgs : EventArgs
    {
        public MailMessage FailedMailMessage;
        public Exception RaisedException;

        public SendMailErrorOccuredEventArgs(MailMessage message, Exception exception)
        {
            FailedMailMessage = message;
            RaisedException = exception;
        }
    }
}
