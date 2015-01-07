using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grit.Utility.Basic
{
    public static class Diagnostics
    {
        public static float CpuUsage()
        {
            using (var counter = new PerformanceCounter(
                "Processor", "% Processor Time", "_Total"))
            {
                counter.NextValue();
                Thread.Sleep(1000);
                return counter.NextValue();
            }
        }

        public static float MemoryUsage()
        {
            using (var counter = new PerformanceCounter(
                "Memory", "Available MBytes"))
            {
                return counter.NextValue();
            }
        }

        public struct ASPNETThreadInfo
        {
            public int MaxWorkerThreads;
            public int MaxIOThreads;
            public int AvailableWorkerThreads;
            public int AvailableIOThreads;
        }

        public static ASPNETThreadInfo GetThreadInfo()
        {
            int availableWorker, availableIO;
            int maxWorker, maxIO;

            ThreadPool.GetAvailableThreads(out availableWorker, out availableIO);
            ThreadPool.GetMaxThreads(out maxWorker, out maxIO);

            ASPNETThreadInfo threadInfo = new ASPNETThreadInfo();
            threadInfo.AvailableWorkerThreads = availableWorker;
            threadInfo.AvailableIOThreads = availableIO;
            threadInfo.MaxWorkerThreads = maxWorker;
            threadInfo.MaxIOThreads = maxIO;

            return threadInfo;
        }
    }
}
