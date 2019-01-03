using MvcExpressAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExpressAdmin.Controllers
{
    public class AngularMvcController : PerSiteController
    {
        // GET: Angular
        public JsonResult getAll()
        {
            using (NewsDataContext dataContext = new NewsDataContext())
            {
                var listGhichu = dataContext.GhiChus.ToList();
                return Json(listGhichu, JsonRequestBehavior.AllowGet);
            }
        }
    }
}