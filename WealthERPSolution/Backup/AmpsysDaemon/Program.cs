using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace AmpsysDaemon
{
    class Program
    {
        static void Main(string[] args)
        {
            Utils.Trace("Starting Daemon...");
            Process aProcess = Process.GetCurrentProcess();
            string aProcName = aProcess.ProcessName;

            if (Process.GetProcessesByName(aProcName).Length > 1)
            {
                Utils.Trace("Exiting Daemon, another instance is running.");
                return;
            }

            StartEmailProcessor();
            StartSMSProcessor();

            Console.ReadLine();
        }

        public static void StartEmailProcessor()
        {
            EmailProcessor EP = new EmailProcessor();
            Thread T = new Thread(EP.Start);
            T.IsBackground = true;
            T.Start();
        }

        public static void StartSMSProcessor()
        {
            SMSProcessor SP = new SMSProcessor();
            Thread T = new Thread(SP.Start);
            T.IsBackground = true;
            T.Start();
        }
    }
}

