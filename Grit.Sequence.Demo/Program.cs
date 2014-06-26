using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.Threading;
using System.Transactions;
using Grit.Sequence.Repository.MySql;

namespace Grit.Sequence.Demo
{
    class Program
    {
        private const int SequenceID = 1;
        public static IKernel Kernel;
        static void Main(string[] args)
        {
            AddIocBindings();

            BasicTest();
            //MultiThreadTest();
            //TransactionScopeTest();
        }

        private static void AddIocBindings()
        {
            Kernel = new StandardKernel();
            Kernel.Bind<ISequenceRepository>().To<SequenceRepository>().InSingletonScope();
            Kernel.Bind<ISequenceService>().To<SequenceService>().InSingletonScope();
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
            ISequenceService sequenceService = Kernel.Get<ISequenceService>();
            for (int i = 0; i < 100; i++)
            {
                int next = sequenceService.Next(SequenceID, 10);
                Console.Write(string.Format("{0}-{1}, ", Thread.CurrentThread.Name,next));
            }
        }

        private static void TransactionScopeTest()
        {
            ISequenceService sequenceService = Kernel.Get<ISequenceService>();
            using (TransactionScope scope = new TransactionScope())
            {
                BasicTest();
            }
        }
    }
}
