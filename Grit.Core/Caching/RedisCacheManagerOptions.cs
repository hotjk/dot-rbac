using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Core.Caching
{
    public class RedisCacheManagerOptions
    {
        public string Configuration { get; set; }
        public int DBIndex { get; set; }
    }
}
