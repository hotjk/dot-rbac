using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.ServiceBus.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            BootStrapper.BootStrap();
            BootStrapper.Bus.Send(new TestCommand { Content = "Hello Command!" });
            Console.ReadLine();
        }
    }
}
