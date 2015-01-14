using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public SecurityKey RequestKey { get; set; }
        public SecurityKey ResponseKey { get; set; }
    }
}
