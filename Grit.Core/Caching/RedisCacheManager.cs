using ProtoBuf;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Core.Caching
{
    public class RedisCacheManager : ICacheManager
    {
        private RedisCacheManagerOptions _options;
        private ConnectionMultiplexer _muxer;
        private int _DBIndex;

        public RedisCacheManager(RedisCacheManagerOptions options)
        {
            _options = options;
            _muxer = ConnectionMultiplexer.Connect(ConfigurationOptions.Parse(_options.Configuration));
            _DBIndex = _options.DBIndex;
        }

        private IDatabase DB
        {
            get
            {
                return _muxer.GetDatabase(_DBIndex);
            }
        }

        public T Get<T>(string key)
        {
            var value = DB.SafeStringGet(key);
            if (!value.HasValue) return default(T);
            using(MemoryStream ms = new MemoryStream(value))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }

        public void Set(string key, object data, int expireMinutes)
        {
            byte[] bytes = null;
            using(MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize(ms, data);
                bytes = ms.ToArray();
            }
            DB.SafeStringSet(key, bytes, TimeSpan.FromMinutes(expireMinutes));
        }

        public bool IsSet(string key)
        {
            return DB.SafeKeyExists(key);
        }

        public void Remove(string key)
        {
            DB.SafeKeyDelete(key);
        }

        public int RemoveByPattern(string pattern)
        {
            var db = DB;
            int count = 0;
            try
            {
                foreach (var endPoint in _muxer.GetEndPoints())
                {
                    foreach (var item in _muxer.GetServer(endPoint).Keys(_DBIndex, pattern))
                    {
                        db.KeyDelete(item);
                        count++;
                    }
                }
            }
            catch(StackExchange.Redis.RedisConnectionException)
            {
            }
            return count;
        }

        public void Clear()
        {
            foreach (var endPoint in _muxer.GetEndPoints())
            {
                _muxer.GetServer(endPoint).SafeFlushDatabase(_DBIndex);
            }
        }
    }
}
