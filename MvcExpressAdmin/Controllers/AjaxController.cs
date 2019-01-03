
using MvcExpressAdmin.Models;
using MvcExpressAdmin.MyClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace MvcExpressAdmin.Controllers
{
    public class AjaxController : Controller
    {
        NewsDataContext db = new NewsDataContext();
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string MatKhau, string LID)
        {
            string passw = MyClss.Encode(MatKhau);
            var dn = from a in db.DangNhaps
                     from b in db.NhanViens
                     where a.MaNV == b.MaNV && a.Allow == true && a.Username.Equals(Username) && a.Pass.Equals(passw)
                     select new { b.MaNV, b.SoThe, b.HoTen, b.HienThi,b.NhomNVID };
            if (dn.Count() > 0)
            {
                HttpCookie user = new HttpCookie("" + ConfigurationManager.AppSettings["CKUserLogin"], "" + Username);
                user.Expires = DateTime.Now.AddDays(30);
                HttpContext.Response.Cookies.Add(user);

                HttpCookie pass = new HttpCookie("" + ConfigurationManager.AppSettings["CKPass"], "" + passw);
                pass.Expires = DateTime.Now.AddDays(30);
                HttpContext.Response.Cookies.Add(pass);

                HttpCookie thdangnhap = new HttpCookie("" + ConfigurationManager.AppSettings["CKMaNV"], "" + dn.SingleOrDefault().MaNV);
                thdangnhap.Expires = DateTime.Now.AddDays(30);
                HttpContext.Response.Cookies.Add(thdangnhap);
                HttpCookie thusername = new HttpCookie("" + ConfigurationManager.AppSettings["CKHoTen"], "" + dn.SingleOrDefault().HoTen);
                thusername.Expires = DateTime.Now.AddDays(30);
                HttpContext.Response.Cookies.Add(thusername);

                HttpCookie derpartment = new HttpCookie("" + ConfigurationManager.AppSettings["CKPhongBan"], "" + dn.SingleOrDefault().NhomNVID);
                derpartment.Expires = DateTime.Now.AddDays(30);
                HttpContext.Response.Cookies.Add(derpartment);

                HttpCookie lid = new HttpCookie("" + ConfigurationManager.AppSettings["CKLang"], "" + LID);
                lid.Expires = DateTime.Now.AddDays(30);
                HttpContext.Response.Cookies.Add(lid);

                string urlweb = "";
                if (System.Web.HttpContext.Current.Session["WebID"] == null)
                {
                    urlweb = "Home";
                }
                else
                {
                    urlweb = System.Web.HttpContext.Current.Session["WebID"].ToString();
                }
                return Json(urlweb, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Signout()
        {
            HttpCookie thdangnhap = new HttpCookie("" + ConfigurationManager.AppSettings["CKMaNV"], "");
            thdangnhap.Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(thdangnhap);
            // return PartialView(@"~/Views/Login/Index.cshtml");
            return Json("/", JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult ChangPass(string MatKhau_old, string MatKhau_new)
        {
            int manv = int.Parse(CookieCls.GetMaNV());
            string passw_old = MyClss.Encode(MatKhau_old);
            string passw_new = MyClss.Encode(MatKhau_new);
            var checkUser = from a in db.DangNhaps
                            where a.MaNV == manv && a.Pass.Equals(passw_old)
                            select a;
            if (checkUser.Count() <= 0)
                return Json("0", JsonRequestBehavior.AllowGet);//Pass old không đúng  
            else
            {
                var login = db.DangNhaps.Where(a => a.MaNV == manv).SingleOrDefault();
                login.Pass = passw_new;
                db.SaveChanges();
                return Json("1", JsonRequestBehavior.AllowGet);//Update Pass success
            }
        }
        [HttpPost]
        public ActionResult ChonPhongBan(string F)
        {    
            HttpCookie tw = new HttpCookie("" + ConfigurationManager.AppSettings["CKPhongBan"], "" + F);
            tw.Expires = DateTime.Now.AddDays(365);
            HttpContext.Response.Cookies.Add(tw);
            return Json("" + F, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult ChonLag(string L)
        {
            HttpCookie tw = new HttpCookie("" + ConfigurationManager.AppSettings["CKLang"], "" + L);
            tw.Expires = DateTime.Now.AddDays(365);
            HttpContext.Response.Cookies.Add(tw);
            return Json("" + L, JsonRequestBehavior.AllowGet);

        }
    }
}