using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NetBlade.Cache
{
    public class HttpCache : CacheBase<HttpCache>
    {
        private System.Web.Caching.Cache _cache;

        private System.Web.Caching.Cache Cache
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Cache != null)
                {
                    return HttpContext.Current.Cache;
                }
                else
                {
                    return this._cache ?? (new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null))).Cache;
                }
            }
        }

        private System.Web.Caching.CacheItemPriority _cacheItemPriority { get { return System.Web.Caching.CacheItemPriority.Normal; } }

        private DateTime _noAbsoluteExpiration { get { return System.Web.Caching.Cache.NoAbsoluteExpiration; } }

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
                    this.Cache.Add(key, value, null, this._noAbsoluteExpiration, slidingExpiration, this._cacheItemPriority, (a, b, c) => { });
                }
            }
        }

        public override T Get<T>(string key)
        {
            object instancia = this.Cache.Get(key);

            if (instancia is T)
            {
                return (T)instancia;
            }
            else
            {
                return default(T);
            }
        }

        public override Dictionary<string, long> GetTotalMemory()
        {
            var enumerator = this.Cache.GetEnumerator();
            Dictionary<string, long> result = new Dictionary<string, long>();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Key.ToString(), this.GetTotalMemory(enumerator.Value));
            }

            return result;
        }

        public override object Remove(string key)
        {
            if (this.Cache[key] != null)
            {
                return this.Cache.Remove(key);
            }
            else
            {
                return null;
            }
        }
    }
}
