using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace AmpsysJobDaemon
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

            StartJobProcessor();
            StartPartProcessor();

            Console.ReadLine();
        }

        public static void StartJobProcessor()
        {
            JobProcessor EP = new JobProcessor();
            Thread T = new Thread(EP.Start);
            T.IsBackground = true;
            T.Start();
        }

        public static void StartPartProcessor()
        {
            JobPartProcessor EP = new JobPartProcessor();
            Thread T = new Thread(EP.Start);
            T.IsBackground = true;
            T.Start();
        }
    }
}

