using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public class DomainMessage
    {
        public Guid Id { get; private set; }
        public DomainMessage()
        {
            this.Id = Guid.NewGuid();
        }

        private static Regex _regexCamel = new Regex("[a-z][A-Z]");
        public static string ToDotString(string str)
        {
            return _regexCamel.Replace(str, m => m.Value[0] + "." + m.Value[1]).ToLower();
        }

        public static string ToCamelString(string str)
        {
            return string.Join("", str.Split(new char[] { '.' }).Select(n => char.ToUpper(n[0]) + n.Substring(1)));
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
