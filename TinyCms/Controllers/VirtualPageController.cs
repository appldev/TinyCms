using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyCms.Models.Cms;

namespace TinyCms.Controllers
{
    public class VirtualPageController : Controller
    {
        // GET: VirtualPage
        public ActionResult Index(string culture, string path)
        {
            path = "/" + path;
            JObject o = new JObject();
            o.Add("Name", "Michael");
            o.Add("Path", path);
            dynamic m = new TinyCmsPageModel();
            var snippet = new ContentObject();
            snippet.Field("url", path);
            snippet.Field("height", 100);
            snippet.Field("width", 100);
            m.Name = "Michael";
            m.Path = path;
            m.Snippet = snippet;
            m.Culture = culture;
            m.json = Newtonsoft.Json.JsonConvert.SerializeObject(m);
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(m);
            TinyCmsPageModel model = Newtonsoft.Json.JsonConvert.DeserializeObject<TinyCmsPageModel>(s);

            
            // return View("~/views/frontend/templates/default.cshtml",model);
            return View("~/" + Guid.NewGuid().ToString() + "")
        }
    }
}