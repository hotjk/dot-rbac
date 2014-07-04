using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public class DomainMessage
    {
        public Guid Id { get; set; }
        public DomainMessage()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
