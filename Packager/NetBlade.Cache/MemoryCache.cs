using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Cache
{
    public class MemoryCache : CacheBase<MemoryCache>
    {
        private ObjectCache Cache { get { return System.Runtime.Caching.MemoryCache.Default; } }

        public override void Add<T>(string key, T value, TimeSpan slidingExpiration)
        {
            lock (this.Cache)
            {
                T instancia = this.Get<T>(key);
                if (!EqualityComparer<T>.Default.Equals(instancia, default(T)))
                {
                    this.Cache.Remove(key);
                }

                if (!EqualityComparer<T>.Default.Equals(value, default(T)))
                {
                    this.Cache.Add(key, value, new CacheItemPolicy() { SlidingExpiration = slidingExpiration });
                }
            }
        }

        public override T Get<T>(string key)
        {
            object result = this.Cache.Contains(key) ? this.Cache.Get(key) : null;
            if (result is T)
            {
                return (T)result;
            }
            else
            {
                return default(T);
            }
        }

        public override Dictionary<string, long> GetTotalMemory()
        {
            Dictionary<string, object> enumerator = this.Cache.ToDictionary(d => d.Key, d => d.Value);
            Dictionary<string, long> result = new Dictionary<string, long>();

            foreach (KeyValuePair<string, object> item in enumerator)
            {
                result.Add(item.Key, this.GetTotalMemory(item.Value));
            }

            return result;
        }

        public override object Remove(string key)
        {
            if (this.Cache.Contains(key))
            {
                return this.Remove(key);
            }
            else
            {
                return null;
            }
        }
    }
}
