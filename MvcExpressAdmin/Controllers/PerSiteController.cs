using MvcExpressAdmin.Models;
using MvcExpressAdmin.MyClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExpressAdmin.Controllers
{
    public class PerSiteController : Controller
    {
        NewsDataContext db = new NewsDataContext();
        // GET: PerSite    
        public ActionResult Index()
        {
            string action = Request.Url.Segments[Request.Url.Segments.Length - 1].ToString();
         
            if (CookieCls.GetMaNV() == "" || CookieCls.GetMaNV() == null)
            {
                Session["WebID"] = "/" + action;
                return PartialView(@"~/Views/Login/Index.cshtml");
            }
            else
            {
                int manv = int.Parse(CookieCls.GetMaNV());
                string languge = CookieCls.GetLanguge();
                var heading = (from a in db.DanhMucs
                               from b in db.DanhMucCTs
                               from c in db.DanhMucChas
                               from d in db.DanhMucChaCTs
                               where a.MaDM == b.MaDM && c.MaDMC == d.MaDMC && a.MaDMC == c.MaDMC && b.LID.Equals(d.LID) && b.LID.Equals(languge) && a.Site.Equals(action)
                               select new { d.TenDMC, b.TenDM }).Distinct();
                if (heading.Count() > 0)
                {                    
                    ViewData["HeadingTenDM"] = heading.SingleOrDefault().TenDM.ToUpper();
                    ViewData["HeadingTopRight"] = "<li><a><i class='fa fa-dashboard'></i>"+ heading.SingleOrDefault().TenDMC+ "</a></li><li class='active'>"+ heading.SingleOrDefault().TenDM + "</li>";
                }
                var pq = from a in db.PhanQuyens
                         from b in db.DanhMucs
                         where a.MaDM == b.MaDM && a.MaNV == manv && b.Site.Equals(action)
                         select a;
                if (pq.Count() > 0)
                    return View();
                else
                    return PartialView(@"~/Views/Home/Error404.cshtml");
            }
        }

        
        private bool ViewExists(string name)
        {
            ViewEngineResult result = ViewEngines.Engines.FindView(ControllerContext, name, null);
            return (result.View != null);
        }
    }
}