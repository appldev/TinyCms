using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TinyCms.Filters;
using TinyCms.Models.Cms;

namespace TinyCms.Controllers
{
    [RequireSslActionFilterAttribute]
    [CmsPageAuthorizeAttribute]
    public class VirtualPageController : Controller
    {
        // GET: VirtualPage
        public ActionResult Index(string culture, string path)
        {
            //path = "/" + path;
            //dynamic m = new TinyCmsPageModel();
            //var snippet = new ContentObject();
            //snippet.Field("url", path);
            //snippet.Field("height", 100);
            //snippet.Field("width", 100);
            //m.Name = "Michael";
            //m.Path = path;
            //m.Snippet = snippet;
            //m.Culture = culture;
            //m.json = Newtonsoft.Json.JsonConvert.SerializeObject(m);
            //string s = Newtonsoft.Json.JsonConvert.SerializeObject(m);
            //TinyCmsPageModel model = Newtonsoft.Json.JsonConvert.DeserializeObject<TinyCmsPageModel>(s);
            culture = culture.Equals("xx-YY", StringComparison.InvariantCultureIgnoreCase) ? null : culture;
            string fullpath = Models.PageHost.BuildPath(Request.UserHostName, culture, path);
            Models.Page page = Caching.PublishedPages.ByFullPath(fullpath);
            if (page != null)
            {
                TinyCmsPageModel model = new TinyCmsPageModel();
                if (page.Model != null)
                {
                    model = Newtonsoft.Json.JsonConvert.DeserializeObject<TinyCmsPageModel>(page.Model);
                }
                return View(page.FilePath, model);
            }

            return new HttpStatusCodeResult(404, "The TinyCrm page does not exist");
            // return View("~/views/frontend/templates/default.cshtml",model);
            // return View("~/" + Guid.NewGuid().ToString() + "");
        }
    }
}