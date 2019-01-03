using MvcExpressAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExpressAdmin.Controllers
{
    public class LoginController : Controller
    {
        NewsDataContext db = new NewsDataContext();
        // GET: Login
        public ActionResult Index()
        {
            //var lg = (from a in db.NgonNgus
            //          where a.LShow == true
            //          orderby a.ThuTu
            //          select new { a.LID, a.LName });

            ViewData["DropListLanguge"] = new SelectList(db.NgonNgus.Where(a => a.LShow == true).OrderBy(a => a.ThuTu).ToList(), "LID", "LName");
            return View();
        }
    }
}
