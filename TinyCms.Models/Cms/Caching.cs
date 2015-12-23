using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TinyCms.Models;
using TinySql;

namespace TinyCms
{

    public class CmsCache<TKey, T>
    {
        private SqlBuilder init;
        private ConcurrentDictionary<TKey, T> dict;
        private string _key;
        public virtual Task Initialize(SqlBuilder loadCache,string KeyName)
        {
            return Task.Run(() =>
           {
               init = loadCache;
               _key = KeyName;
               IEqualityComparer<TKey> compare = typeof(TKey) == typeof(string) ? (IEqualityComparer<TKey>)StringComparer.OrdinalIgnoreCase : EqualityComparer<TKey>.Default;
               if (init != null)
               {
                   dict = new ConcurrentDictionary<TKey, T>(init.Dictionary<TKey, T>(_key), compare);
               }
               else
               {
                   dict = new ConcurrentDictionary<TKey, T>(compare);
               }
               
           });
        }

        public T this[TKey key]
        {
            get
            {
                T value;
                if (dict.TryGetValue(key, out value))
                {
                    return value;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public T Add(TKey key, T item)
        {
            return dict.AddOrUpdate(key, item, (k, existing) =>
            {
                return item;
            });
        }

        public bool Has(TKey key)
        {
            return this.dict.ContainsKey(key);
        }
        public T Get(TKey key, T DefaultValue = default(T))
        {
            T value = this[key];
            if (value.Equals(default(T)))
            {
                return DefaultValue;
            }
            return value;
        }
    }

    public class PageHostCache : CmsCache<string, PageHost>
    {
        public PageHostCache()
        {
            this.Initialize();
        }
        public Guid Get(string HostName)
        {
            PageHost host = this[HostName];
            if (host == null)
            {
                host = this["Default"];
            }
            return host.Id;
        }
        public async void Initialize()
        {
            //
            // Load hosts
            //
            SqlBuilder builder = SqlBuilder.Select()
                .From("PageHost")
                .AllColumns()
                .Builder();

            await this.Initialize(builder, "Name");
        }
        

        
    }

    public class PageCache
    {
        private ConcurrentDictionary<Guid, CmsCache<string, PublishedPage>> pub = new ConcurrentDictionary<Guid, CmsCache<string, PublishedPage>>();
        private ConcurrentDictionary<Guid, CmsCache<string, Page>> unpub = new ConcurrentDictionary<Guid, CmsCache<string, Page>>();


        public PublishedPage GetPublished(int LCID, string Path, string Host)
        {
            Guid g = Caching.Hosts.Get(Host);
            CmsCache<string, PublishedPage> cache = pub[g];
            if (cache == null)
            {
                cache = new CmsCache<string, PublishedPage>();
                SqlBuilder builder = SqlBuilder.Select()
                    .From("PublishedPage")
                    .AllColumns()
                    .Where<Guid>("PublishedPage", "PageHostId", SqlOperators.Equal, g)
                    .Builder();

                cache.Initialize(builder, "fullpath");
                if (!pub.TryAdd(g,cache))
                {
                    throw new InvalidOperationException(string.Format("The cache for the host '{0}' could not be added", Host));
                }
            }
            return cache[Path];
        }

        public Page GetUnPublished(int LCID, string Path, string Host)
        {
            Guid g = Caching.Hosts.Get(Host);
            CmsCache<string, Page> cache = unpub[g];
            if (cache == null)
            {
                cache = new CmsCache<string, Page>();
                SqlBuilder builder = SqlBuilder.Select()
                    .From("Page")
                    .AllColumns()
                    .Where<Guid>("Page", "PageHostId", SqlOperators.Equal, g)
                    .Builder();

                cache.Initialize(builder, "fullpath");
                if (!unpub.TryAdd(g, cache))
                {
                    throw new InvalidOperationException(string.Format("The cache for the host '{0}' could not be added", Host));
                }
            }
            return cache[Path];
        }

    }

    public class Caching
    {
        public static PageHostCache Hosts = new PageHostCache();
        public static PageCache Pages = new PageCache();

        public static CmsCache<Guid, DataType> DataTypes = new CmsCache<Guid, DataType>();


        public static async void Initialize()
        {

            //
            // Load hosts
            //
            SqlBuilder builder = SqlBuilder.Select()
                .From("PageHost")
                .AllColumns()
                .Builder();

            await Hosts.Initialize(builder, "Name");
            await DataTypes.Initialize(null, "Id");
        }


    }
}