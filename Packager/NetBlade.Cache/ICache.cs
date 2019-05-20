using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Cache
{
    public interface ICache
    {
        void Add<T>(string key, T value);

        void Add<T>(string key, T value, TimeSpan slidingExpiration);

        T Get<T>(string key, Func<T> action = null, TimeSpan? slidingExpiration = null, bool atualizar = false);

        T Get<T>(string key, Func<T> action, bool atualizar = false);

        T Get<T>(string key);

        Dictionary<string, long> GetTotalMemory();

        object Remove(string key);
    }
}
