using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public class Event : IEvent
    {
        private static Regex _regexCamel = new Regex("[a-z][A-Z]");
        private static string ToDotString(string str)
        {
            return _regexCamel.Replace(str, m => m.Value[0] + "." + m.Value[1]).ToLower();
        }

        [JsonIgnore]
        public string RoutingKey
        {
            get
            {
                return ToDotString(this.GetType().Name);
            }
        }
    }
}
