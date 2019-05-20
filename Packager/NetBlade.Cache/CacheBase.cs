using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Cache
{
    public abstract class CacheBase<TCache> : ICache
        where TCache : class, ICache
    {
        private static TCache _default;

        public static TCache Default { get { return (CacheBase<TCache>._default as TCache) ?? (TCache)(CacheBase<TCache>._default = Activator.CreateInstance<TCache>()); } }

        public void Add<T>(string key, T value)
        {
            this.Add<T>(key, value, TimeSpan.FromHours(4));
        }

        public abstract void Add<T>(string key, T value, TimeSpan slidingExpiration);

        public T Get<T>(string key, Func<T> action = null, TimeSpan? slidingExpiration = null, bool atualizar = false)
        {
            T instancia = this.Get<T>(key);
            if ((EqualityComparer<T>.Default.Equals(instancia, default(T)) && action != null) || atualizar)
            {
                instancia = action.Invoke();
                this.Add<T>(key, instancia, slidingExpiration ?? TimeSpan.FromHours(4));
            }

            return instancia;
        }

        public T Get<T>(string key, Func<T> action, bool atualizar = false)
        {
            return this.Get<T>(key, action, null, atualizar);
        }

        public abstract T Get<T>(string key);

        public abstract Dictionary<string, long> GetTotalMemory();

        public abstract object Remove(string key);

        public string GetKeyString(string key, params object[] args)
        {
            return string.Format(key, args);
        }

        protected long GetTotalMemory(object obj)
        {
            long size = 0;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, obj);
                size = s.Length;
            }

            return size;
        }
    }
}
