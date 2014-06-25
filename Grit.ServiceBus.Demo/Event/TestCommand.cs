using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.ServiceBus.Demo
{
    public class TestCommand : Command
    {
        public string Content { get; set; }
    }
}
