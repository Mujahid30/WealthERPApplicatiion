using System;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Xml.Serialization;

namespace PCGMailLib
{
    /// <summary>
    /// This class is build on top of .Net default SmtpClient with queing and some more added functionality 
    /// This can be modified to have some more methods for bulk mail solutions and using multiple SMTP server at same time by using More them one thread 
    /// once the need comes when we integrate it with Email alert engine .and the number of mail to send is reallt huge 
    /// </summary>
   public class PCGSmtpClient:SmtpClient
    
    {
        public string TempFolder { get; set; }
        private int mSleepIntervalSeconds;
        private static object locker = new object();
        Thread mHelperSendingSerializedMails;

        public delegate MailMessageErrorSolution SendMailErrorOccuredDelegate(object sender, SendMailErrorOccuredEventArgs args);

        public SendMailErrorOccuredDelegate mMailErrorOccuredDelegate;
            

        //The smtp port. Usually, port 25
        public PCGSmtpClient(string tempFolder, string host, int port, int sleepIntervalSeconds) : base(host, port)
        {
            TempFolder = tempFolder;
            mSleepIntervalSeconds = sleepIntervalSeconds;

            mHelperSendingSerializedMails = new Thread(TryToSend);
            mHelperSendingSerializedMails.Start();
        }

        public void Stop()
        {
            if (mHelperSendingSerializedMails != null)
                mHelperSendingSerializedMails.Abort();
        }

        public new void Send(MailMessage msg)
        {
            //don't send the message now, but serialize it and let a thread deserialize and send.
            SerializeMailMessage(msg);
        }

        public void TryToSend()
        {
            while (true)
            {
                lock (locker)//lock the resource to avoid deadlock ....
                {
                    string[] SerializedMailMessages = Directory.GetFiles(TempFolder);

                    XmlSerializer serializer = new XmlSerializer(typeof (SerializableMailMessage));

                    for (int i = 0; i < SerializedMailMessages.Length; i++)
                    {
                        SerializableMailMessage mailToDeserialize;

                        using (TextReader streamReader = new StreamReader(SerializedMailMessages[i]))
                        {
                            mailToDeserialize = serializer.Deserialize(streamReader) as SerializableMailMessage;
                        }

                        if (mailToDeserialize != null &&
                            System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                        {
                            try

                            {
                                base.UseDefaultCredentials = true;
                                //SmtpMail.SmtpServer = "smtp.your-server.com";
                                //base.DeliveryMethod = SmtpDeliveryMethod.Network;
                                base.PickupDirectoryLocation = @"C:\Inetpub\mailroot\Pickup\";
                                base.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                                base.Send(mailToDeserialize.Email);

                                File.Delete(SerializedMailMessages[i]);
                            }
                            catch (Exception e)
                            {
                                if (mMailErrorOccuredDelegate != null)
                                {
                                    SendMailErrorOccuredEventArgs args = new SendMailErrorOccuredEventArgs(mailToDeserialize.Email, e);
                                    if (mMailErrorOccuredDelegate(this, args) == MailMessageErrorSolution.Delete)
                                    {
                                        File.Delete(SerializedMailMessages[i]);
                                    }
                                }
                            }
                        }
                    }
                }

                Thread.Sleep(mSleepIntervalSeconds * 1000);
            }
        }

        private void SerializeMailMessage(MailMessage msg)
        {
            lock (locker)
            {
                //serialize message to temp folder
                SerializableMailMessage mailToSerialize = new SerializableMailMessage();
                mailToSerialize.Email = msg;

                XmlSerializer serializer = new XmlSerializer(typeof (SerializableMailMessage));

                using (
                    TextWriter streamWriter = new StreamWriter(TempFolder + "\\" + System.DateTime.Now.Ticks + ".msg"))
                {
                    serializer.Serialize(streamWriter, mailToSerialize);
                }
            }
        }
    }
}
