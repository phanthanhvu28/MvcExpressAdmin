using MvcExpressAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcExpressAdmin.Controllers
{
    public class HomeController : Controller
    {
        private NewsDataContext db = new NewsDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoadData()
        {
            StringBuilder s = new StringBuilder();
            string data = "", sizeHost = "";
            int countApp = 0, count_news_vn = 0, count_news_nn = 0;
            countApp = (from a in db.mApps
                        select a).Count();
            sizeHost= Conn.getInfo(@"SELECT Convert(varchar,ROUND(SUM(mf.size) * 8 / 1024, 2)+100)+'MB ('+ CONVERT(varchar,CAST(((ROUND(SUM(mf.size) * 8 / 1024, 2)+100)/3072.0)*100 AS numeric(18,0))) + '%)' as HT
                from sys.database_files mf");
            DateTime date = DateTime.Now.Date;
            count_news_vn = (from a in db.rssNews
                             from b in db.mNewspaperMenus
                             from c in db.mNewspapers
                             where a.Effect == true && a.NewspaperMenuId == b.NewspaperMenuId && b.NewspaperId == c.NewspaperId && c.Languge.Equals("vie") && a.DateInput.Value.ToString("{0:mm/dd/yyyy}")=="02/14/2019"
                             select a).Count();

            data = "" + countApp + "#$%^" + sizeHost + "#$%^" + count_news_nn;
            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }
    }
}