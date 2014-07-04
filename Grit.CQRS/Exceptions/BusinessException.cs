using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(DomainMessage domainMessage, string message) : base(message) 
        { 
        }
    }
}
