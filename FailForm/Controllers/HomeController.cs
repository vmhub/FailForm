using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FailForm.Models;
using System.Text;
using System.Data.Entity.Validation;
using System.Diagnostics;
namespace FailForm.Controllers
{
    //try-catch not implemented,unfortunately
    public class HomeController : Controller
    {
        private List<Sector> sectorList;
        public HomeController()
        {
        }
        /// <summary>
        /// Initialize sector list
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            sectorList = sectorDbEntries.getList();
        }
        /// <summary>
        /// Checks if session exists
        /// </summary>
        /// <returns></returns>
        protected bool sesssionThere()
        {
            return Session["Sent"] != null;
        }
        /// <summary>
        /// Insert model object to session
        /// </summary>
        /// <param name="data"></param>
        protected void setSession(InfoStorageUpdate data)
        {
            Session["Sent"] = data;
        }
        /// <summary>
        /// Cast session to InfoStorageUpdate type
        /// </summary>
        /// <returns></returns>
        protected InfoStorageUpdate castSesston()
        {
            return Session["Sent"] as InfoStorageUpdate;
        }
        /// <summary>
        /// Main action that loads views based on session existence
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            if (sesssionThere())
            {
                InfoStorageUpdate upd = castSesston();
                ViewBag.Data = new Bag { list = new MultiSelectList(sectorList, "Value", "htmlName", upd.secVals), partial = "EditForm" };
                return View("onSession", upd);
            }
            else
            {
                ViewBag.Data = new Bag { list = new MultiSelectList(sectorList, "Value", "htmlName"), partial = "PostForm" };
                return View();
            }
        }
        /// <summary>
        /// Action in charge of posting data to database
        /// </summary>
        /// <param name="gd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post(InfoStorage gd)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Data = new Bag { list = new MultiSelectList(sectorList, "Value", "htmlName", gd.secVals), partial = "PostForm" };
                return View("Index");
            }
            gd.dbSecVals = String.Join(",", gd.secVals);
            using (MyContext cont = new MyContext())
            {
                cont.infoStore.Add(gd);
                cont.SaveChanges();     
            }
            InfoStorageUpdate up = new InfoStorageUpdate
            {
                Email = gd.Email,
                Name = gd.Name,
                Terms = gd.Terms,
                secVals = gd.secVals,
            };
            setSession(up);
            ViewBag.Data = new Bag { list = new MultiSelectList(sectorList, "Value", "htmlName", gd.secVals), partial = "EditForm" };
            return PartialView("EditForm", castSesston());
        }
        /// <summary>
        /// Action in charge of editing stored data
        /// </summary>
        /// <param name="gd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(InfoStorageUpdate gd)
        {
            ViewBag.Data = new Bag { list = new MultiSelectList(sectorList, "Value", "htmlName", gd.secVals), partial = "EditForm" };
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            using (MyContext cont = new MyContext())
            {
                cont.Configuration.ValidateOnSaveEnabled = false; //Needed to avoid DB email validation on edit
                InfoStorage upd = cont.infoStore.Where(x => (x.Email == gd.Email)).First();
                upd.Name = gd.Name;
                upd.secVals = gd.secVals;
                upd.dbSecVals = String.Join(",", gd.secVals);
                cont.SaveChanges();
            }
            setSession(gd);
            return PartialView("EditForm", gd);
        }
        /// <summary>
        /// Client side email validation
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult MailInvalid([Bind(Prefix = "Email")]string mail)
        {
            InfoStorage back;
            using (MyContext cont = new MyContext())
            {
                back = cont.infoStore.Where(x => (x.Email == mail)).FirstOrDefault();
            }
            return Json(back == null ? true : false, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Helper that outputs data to console
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult logDb()
        {
            List<InfoStorage> back;
            using (MyContext cont = new MyContext())
            {
                back = cont.infoStore.ToList();
            }
            return Json(back, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Helper class for Viewbag to store
        /// </summary>
        public class Bag
        {
            public string partial { get; set; }
            public MultiSelectList list { get; set; }
        }
    }
}