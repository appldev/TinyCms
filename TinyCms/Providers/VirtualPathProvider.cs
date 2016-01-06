using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace TinyCms.Providers
{
    public class TinyCmsVirtualPathProvider : VirtualPathProvider
    {
        public override string CombineVirtualPaths(string basePath, string relativePath)
        {
            return base.CombineVirtualPaths(basePath, relativePath);
        }

        public override bool DirectoryExists(string virtualDir)
        {
            return base.DirectoryExists(virtualDir);
        }

        public override bool FileExists(string virtualPath)
        {
            bool b = base.FileExists(virtualPath);
            if (!b)
            {
                virtualPath= virtualPath.TrimStart('~');
                b = Caching.PublishedPages.ExistByFilePath(virtualPath);
            }
            return b;
        }


        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            Guid g = Caching.PublishedPages.IdByVirtualFilePath(virtualPath);
            if (!g.Equals(Guid.Empty))
            {
                return null;
            }
            return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            Guid g = Caching.PublishedPages.IdByVirtualFilePath(virtualPath);
            if (!g.Equals(Guid.Empty))
            {
                return new TinyCmsVirtualFile(Caching.PublishedPages.ById(g), virtualPath);
            }
            return Previous.GetFile(virtualPath);

        }

        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            Models.Page p = Caching.PublishedPages.ByFilePath(virtualPath.TrimStart('~'));
            if (p != null)
            {
                return Models.Page.GetFileHashByPage(p);
            }
            return Previous.GetFileHash(virtualPath, virtualPathDependencies);
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            return base.GetDirectory(virtualDir);
        }
    }


    public class TinyCmsCacheDependency : CacheDependency
    {
        private string _Id;
        public TinyCmsCacheDependency(Guid Id, DateTime LastChangedUtc)
        {
            _Id = Id.ToString();
            this.SetUtcLastModified(LastChangedUtc);
        }
        public override string GetUniqueID()
        {
            return _Id;
        }
    }

    public class TinyCmsVirtualFile : VirtualFile
    {
        private Models.Page _page;
        public TinyCmsVirtualFile(Models.Page page, string virtualPath)
            : base(virtualPath)
        {
            _page = page;
        }

        public override Stream Open()
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(_page.Html);
            return new MemoryStream(b);
        }
    }
}