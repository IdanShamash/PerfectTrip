using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using Eventus.Models;

namespace Eventus.Controllers
{
    public class SearchController : Controller
    {

        Search s = new Search();

        //
        // POST: /Search/Result
        [HttpPost]
        public ActionResult Result(String json)
        {
            MemoryStream ms = new MemoryStream();
            JsonResult jsonR = new JsonResult();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<EventBL>));

            ms.Position = 0;
            //Dictionary<int,List<EventBL>> le = s.doSearch(new DateTime(2016, 5, 29), new DateTime(2016, 5, 31), "brazil", new List<EventSpecification>() { new EventSpecification("music", "rihana") });
            EventsContainer le = s.doSearch(new DateTime(2016, 5, 29), new DateTime(2016, 5, 31), "brazil", new List<EventSpecification>() { new EventSpecification(1, "music", "rihana") });
            ser.WriteObject(ms, le);
            jsonR.Data = ms;

            //return Json(le, JsonRequestBehavior.AllowGet);
            return View(le);
        }

        public ActionResult Save(String user, String jsonElemnts)
        {
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<EventBL>));

            List<EventBL> le = (List<EventBL>)ser.ReadObject(ms); // read the json elements

            if (s.SaveEvents(user, le))
            {
                return View();
            }
            else
            {
                return null;
            }
        }
    }
}
