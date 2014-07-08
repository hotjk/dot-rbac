using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Calls
{
    public class CallResponse : DomainMessage
    {
        public enum CallResponseResult
        {
            OK = 0,
            NG = 1,
        }
        public CallResponseResult Result { get; set; }
        public string Message { get; set; }
    }
}
