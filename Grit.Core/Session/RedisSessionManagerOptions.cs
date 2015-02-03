using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Core.Session
{
    public class RedisSessionManagerOptions
    {
        public RedisSessionManagerOptions(string configuration, int db, int sessionTimeoutMinutes)
        {
            Configuration = configuration;
            DBIndex = db;
            SessionTimeoutMinutes = sessionTimeoutMinutes;
        }
        public string Configuration { get; private set; }
        public int DBIndex { get; private set; }
        public int SessionTimeoutMinutes { get; private set; }
    }
}
