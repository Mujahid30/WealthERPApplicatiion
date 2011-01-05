using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace SendMailAlert
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("waiting"+DateTime.Now.ToLongTimeString());
            int secs = int.Parse(ConfigurationSettings.AppSettings["Wait"].ToString());
            System.Threading.Thread.Sleep(secs*1000);
            Console.WriteLine("starting" + DateTime.Now.ToLongTimeString());
            MailClass mailClass = new MailClass();
            MailVo mailVo=new MailVo();
            string filepath = ConfigurationSettings.AppSettings["logPath"].ToString();
  
            
            mailVo.Host=ConfigurationSettings.AppSettings["host"].ToString();
            mailVo.Port = int.Parse(ConfigurationSettings.AppSettings["port"].ToString());
            mailVo.UserName = ConfigurationSettings.AppSettings["UserName"].ToString();
            mailVo.Password = ConfigurationSettings.AppSettings["Password"].ToString();
            mailVo.From = ConfigurationSettings.AppSettings["From"].ToString();
           
            mailVo.Subject = ConfigurationSettings.AppSettings["Subject"].ToString();
            mailVo.Message = ConfigurationSettings.AppSettings["Message"].ToString();
            mailVo.To = ConfigurationSettings.AppSettings["To"].ToString().Split(';');
            mailVo.Cc = ConfigurationSettings.AppSettings["Cc"].ToString().Split(';');
            mailVo.Bcc = ConfigurationSettings.AppSettings["Bcc"].ToString().Split(';');
            mailVo.IsBulk = int.Parse(ConfigurationSettings.AppSettings["IsBulk"].ToString());
            if (mailVo.IsBulk == 0)
            {
                if (mailClass.SendMail(mailVo))
                {
                    Console.WriteLine("Alert Send Successfully");
                }
                else
                {
                    Console.WriteLine("Alert Not Send");
                }
            }
            else
            {
                if (mailClass.SendBulkMail(mailVo))
                {
                    Console.WriteLine("Alert Send Successfully");
                }
                else
                {
                    Console.WriteLine("Alert Not Send");
                }
            }
            Console.ReadLine();

        }
        
    }
}
