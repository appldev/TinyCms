using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using TinyCms.Models;
using TinySql;

namespace TinyCms
{

    public class IncrementalCmsCache<TKey, T> : CmsCache<TKey, T>
    {
        public override Task Initialize(SqlBuilder loadCache, string KeyName, string ValueName = null)
        {
            return base.Initialize(loadCache, KeyName, ValueName);
        }

        public override Task Reload()
        {
            return Task.Run(() => { });
        }

        public override T this[TKey key]
        {
            get
            {
                T item = base[key];
                if (item == null || item.Equals(default(T)))
                {
                    
                    List<T> list = init.List<T>(null,30,false,false,key);
                    if (list.Count != 1)
                    {
                        return default(T);
                    }
                    item = list.First();
                    this.Add(key, item);
                }
                return item;
            }
        }

       
    }

    public class CmsCache<TKey, T>
    {
        protected ConcurrentDictionary<TKey, T> dict;
        protected SqlBuilder init;
        private string _key;
        private string _value;
        IEqualityComparer<TKey> _compare;
        public virtual Task Initialize(SqlBuilder loadCache, string KeyName, string ValueName = null)
        {
            return Task.Run(() =>
           {
               init = loadCache;
               _key = KeyName;
               _value = ValueName;
               _compare = typeof(TKey) == typeof(string) ? (IEqualityComparer<TKey>)StringComparer.OrdinalIgnoreCase : EqualityComparer<TKey>.Default;
               Reload();
           });
        }

        public virtual Task Reload()
        {
            return Task.Run(() =>
           {
               if (init != null)
               {
                   if (string.IsNullOrEmpty(_value))
                   {
                       dict = new ConcurrentDictionary<TKey, T>(init.Dictionary<TKey, T>(_key,EnforceTypesafety: false), _compare);
                   }
                   else
                   {
                       dict = new ConcurrentDictionary<TKey, T>(_compare);
                       ResultTable result = init.Execute(WithMetadata: false);
                       foreach (RowData row in result)
                       {
                           if (!dict.TryAdd(row.Column<TKey>(_key), row.Column<T>(_value)))
                           {
                               throw new InvalidOperationException(string.Format("Unable to load CmsCache<{0}, {1}>", typeof(TKey).Name, typeof(T).Name));
                           }
                       }
                   }
               }
               else
               {
                   dict = new ConcurrentDictionary<TKey, T>();
               }
           });
        }



        public virtual T this[TKey key]
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

        public bool Remove(TKey key)
        {
            T item;
            return dict.TryRemove(key, out item);
        }

        public bool Has(TKey key)
        {
            return this.dict.ContainsKey(key);
        }
        public virtual T Get(TKey key, T DefaultValue = default(T))
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

        public PageHost ByName(string HostName)
        {
            PageHost host = this[HostName];
            if (host == null)
            {
                host = this["Default"];
            }
            return host;
        }

        public Guid Get(string HostName)
        {
            return ByName(HostName).Id;
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

    public class DataTypeCache : CmsCache<Guid,DataType>
    {
        

        public async void Initialize()
        {
            SqlBuilder builder = SqlBuilder.Select()
                .From("DataType")
                .AllColumns()
                .Builder();

            await this.Initialize(builder, "Id");

            builder = SqlBuilder.Select()
                .From("Field")
                .AllColumns()
                .Builder();

            List<TinyCms.Models.Field> fields = TinySql.Data.All<TinyCms.Models.Field>(EnforceTypesafety: false);
            foreach (Guid id in dict.Keys)
            {
                DataType dt = this.Get(id);
            }
        }

        
    }



    public class PageCache
    {
        private readonly bool _Published = true;
        public bool Published
        {
            get { return _Published; }
        }

        public static Func<Page, bool> NotifyDeleteEvent = null;
        public static Func<Page, bool> NotifyChangeEvent = null;

        public bool NotifyDelete(Page page)
        {
            using (TransactionScope trans = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                int i = 0;
                i += IdByFilePath.Remove(page.FilePath) ? 1 : 0;
                i += IdByFullPath.Remove(page.FullPath) ? 1 : 0;
                i += PagesById.Remove(page.Id) ? 1 : 0;
                if (i != 3)
                {
                    return false;
                }
                trans.Complete();
            }
            return true;
        }

        public bool NotifyChange(Page page)
        {
            using (TransactionScope trans = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                Page OriginalPage = PagesById.Get(page.Id);
                if (OriginalPage != null)
                {
                    if (!OriginalPage.FilePath.Equals(page.FilePath, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!IdByFilePath.Remove(OriginalPage.FilePath))
                        {
                            return false;
                        }
                        IdByFilePath.Add(page.FilePath, page.Id);
                    }
                    if (!OriginalPage.FullPath.Equals(page.FullPath, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!IdByFullPath.Remove(OriginalPage.FullPath))
                        {
                            return false;
                        }
                        IdByFullPath.Add(page.FullPath, page.Id);
                    }
                    if (!PagesById.Add(page.Id,page).Equals(page))
                    {
                        return false;
                    }
                    trans.Complete();
                }
                return true;

            }
        }

        public PageCache(bool Published)
        {
            this._Published = Published;
        }


        public async void Initialize()
        {
            await PagesById.Initialize(Page.BuilderByHost(null, _Published), "Id");
            await IdByFilePath.Initialize(Page.FilePathMapBuilderByHost(null, _Published), "FilePath", "Id");
            await IdByFullPath.Initialize(Page.FullPathMapBuilderByHost(null, _Published), "FullPath", "Id");
        }





        /// <summary>
        /// Published pages
        /// </summary>
        protected CmsCache<Guid, Page> PagesById = new CmsCache<Guid, Page>();
        protected CmsCache<string, Guid> IdByFilePath = new CmsCache<string, Guid>();
        protected CmsCache<string, Guid> IdByFullPath = new CmsCache<string, Guid>();

        public Page ById(Guid Id)
        {
            return PagesById.Get(Id);
        }


        public bool ExistByFilePath(string FilePath)
        {
            return IdByFilePath.Has(FilePath);
        }

        public bool ExistByFullPath(string FullPath)
        {
            return IdByFullPath.Has(FullPath);
        }
        public Page ByFilePath(string FilePath)
        {
            Guid id = IdByFilePath.Get(FilePath, Guid.Empty);
            if (id.Equals(Guid.Empty))
            {
                return null;
            }
            return PagesById.Get(id);
        }

        public Page ByFullPath(string FullPath)
        {
            Guid id = IdByFullPath.Get(FullPath, Guid.Empty);
            if (id.Equals(Guid.Empty))
            {
                return null;
            }
            return PagesById.Get(id);
        }

        public Guid IdByVirtualFilePath(string FilePath)
        {
            return IdByFilePath.Get(FilePath);
        }
    }

    public class Caching
    {
        public static PageHostCache Hosts = new PageHostCache();
        public static PageCache UnpublishedPages = new PageCache(false);
        public static PageCache PublishedPages = new PageCache(true);

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

            UnpublishedPages.Initialize();
            PublishedPages.Initialize();
        }


    }
}