using Grit.Sequence.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.Threading;
using System.Transactions;

namespace Grit.Sequence.Demo
{
    class Program
    {
        private const int SequenceID = 1;
        static void Main(string[] args)
        {
            BootStrapper.BootStrap();
            BasicTest();
            //MultiThreadTest();
            //TransactionScopeTest();
        }

        private static void MultiThreadTest()
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(BasicTest);
                thread.Name = i.ToString();
                threads.Add(thread);
            }
            threads.ForEach(x => x.Start());
            threads.ForEach(x => x.Join());
        }

        private static void BasicTest()
        {
            ISequenceService sequenceService = BootStrapper.Kernel.Get<ISequenceService>();
            for (int i = 0; i < 100; i++)
            {
                int next = sequenceService.Next(SequenceID, 10);
                Console.Write(string.Format("{0}-{1}, ", Thread.CurrentThread.Name,next));
            }
        }

        private static void TransactionScopeTest()
        {
            ISequenceService sequenceService = BootStrapper.Kernel.Get<ISequenceService>();
            using (TransactionScope scope = new TransactionScope())
            {
                BasicTest();
            }
        }
    }
}
