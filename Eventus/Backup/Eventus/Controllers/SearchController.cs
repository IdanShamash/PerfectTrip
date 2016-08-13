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
        //
        // GET: /Search/

        public ActionResult Get(String json)
        {
            MemoryStream ms = new MemoryStream();
            JsonResult jsonR = new JsonResult();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Models.Search));

            ms.Position = 0;
            Models.Search s = (Models.Search)ser.ReadObject(ms);

            ser = new DataContractJsonSerializer(typeof(List<Event>));

            ms.Position = 0;
            ser.WriteObject(ms, s.desiredEvents);
            jsonR.Data = ms;                                //Models.Search.doSearch(s.dateStart, s.dateEnd);

            return jsonR;
        }

        public ActionResult PostStatic()
        {
            MemoryStream ms = new MemoryStream();
            JsonResult jsonR = new JsonResult();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Event>));

            ms.Position = 0;
            List<Event> le = Search.doSearch(new DateTime(2016,5,30), new DateTime(2016,5,31));
            ser.WriteObject(ms, le);
            jsonR.Data = ms;

            return jsonR;
        }
    }
}
