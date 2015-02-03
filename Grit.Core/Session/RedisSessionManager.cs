using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Core.Session
{
    public class RedisSessionManager
    {
        private RedisSessionManagerOptions _options;
        private ConnectionMultiplexer _muxer;

        public RedisSessionManager(RedisSessionManagerOptions options)
        {
            _options = options;
            _muxer = ConnectionMultiplexer.Connect(ConfigurationOptions.Parse(_options.Configuration));
        }

        private IDatabase DB
        {
            get
            {
                return _muxer.GetDatabase(_options.DBIndex);
            }
        }

        public bool Set(string key, string field, string value)
        {
            bool result = DB.SafeHashSet(key, field, value);
            result = result && DB.SafeKeyExpire(key, TimeSpan.FromMinutes(_options.SessionTimeoutMinutes));
            return result;
        }

        public string Get(string key, string field)
        {
            var value = DB.SafeHashGet(key, field);
            DB.SafeKeyExpire(key, TimeSpan.FromMinutes(_options.SessionTimeoutMinutes));
            return value;
        }
    }
}
