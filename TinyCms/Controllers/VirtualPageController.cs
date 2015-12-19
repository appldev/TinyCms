using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TinyCms.Controllers
{
    public class VirtualPageController : Controller
    {
        // GET: VirtualPage
        public ActionResult Index(string path)
        {
            path = "/" + path;
            JObject o = new JObject();
            o.Add("Name", "Michael");
            o.Add("Path", path);

            Session.LCID
            
            



            return View("~/views/frontend/templates/default.cshtml",o);
        }
    }
}