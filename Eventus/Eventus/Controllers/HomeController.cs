using Eventus.Context;
using Eventus.Models;
using Eventus.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eventus.Models.GeneticAlgo;

namespace Eventus.Controllers
{
    public class HomeController : Controller
    {

        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult firstpage()
        {
            return View();
        }
        public ActionResult search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult searchRes(Search searchParams)
        {
            List<JsonPath> jj = new List<JsonPath>();

            try
            {
                List<IndividualPath> IPlist = searchParams.doSearch(); //.AllEvents[searchParams.specs[0]]; ;//(DateTime.Parse(searchParams.startDate),DateTime.Parse(searchParams.endDate),searchParams.location);

                foreach (IndividualPath ip in IPlist)
                {
                    jj.Add(new JsonPath(ip));
                }

                return Json(jj);
            }
            catch (Exception)
            {
                // TODO: change!!!
                return Json("");
            }
        }

        public ActionResult recieveLocations()
        {
            return Json(Models.DAL.Location.GetLocations(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult recieveTypes()
        {
            return Json(Models.DAL.EventType.GetTypes(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult register()
        {
            return View();
        }

        public ActionResult tryForm()
        {
            return View();
        }

        public ActionResult SavedEvents()
        {
            return View();
        }
        
        public ActionResult meter()
        {
            return View();
        }
        public ActionResult History()
        {
            return View();
        }
    }
}
