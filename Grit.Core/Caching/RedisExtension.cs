using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Core.Caching
{
    public static class RedisExtension
    {
        public static RedisValue SafeStringGet(this IDatabase self, RedisKey key, CommandFlags flags = CommandFlags.None)
        {
            try
            {
                return self.StringGet(key, flags);
            }
            catch(RedisConnectionException)
            {
                return RedisValue.Null;
            }
        }

        public static bool SafeStringSet(this IDatabase self, RedisKey key, RedisValue value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            try
            {
                return self.StringSet(key, value, expiry, when, flags);
            }
            catch (RedisConnectionException)
            {
                return false;
            }
        }

        public static bool SafeKeyDelete(this IDatabase self, RedisKey key, CommandFlags flags = CommandFlags.None)
        {
            try
            {
                return self.KeyDelete(key, flags);
            }
            catch (RedisConnectionException)
            {
                return false;
            }
        }

        public static bool SafeKeyExists(this IDatabase self, RedisKey key, CommandFlags flags = CommandFlags.None)
        {
            try
            {
                return self.KeyExists(key, flags);
            }
            catch (RedisConnectionException)
            {
                return false;
            }
        }

        public static void SafeFlushDatabase(this IServer self, int database = 0, CommandFlags flags = CommandFlags.None)
        {
            try
            {
                self.FlushDatabase(database, flags);
            }
            catch (RedisConnectionException)
            {
            }
        }
    }
}
