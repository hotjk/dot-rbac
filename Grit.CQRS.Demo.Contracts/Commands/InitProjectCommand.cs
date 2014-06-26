using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Contracts.Commands
{
    public class InitProjectCommand : Command
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
