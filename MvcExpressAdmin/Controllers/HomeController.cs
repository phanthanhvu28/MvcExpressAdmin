using MvcExpressAdmin.Models;
using MvcExpressAdmin.MyClass;
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
        NewsDataContext db = new NewsDataContext();
        AdminSiteController ad = new AdminSiteController();
        int UserId = int.Parse(CookieCls.GetMaNV());
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoadData()
        {
           
            string data = "", sizeHost = "";
            int countApp = 0, count_news_vn = 0, count_news_nn = 0, count_lantruycap=0,count_luotxem=0;
            countApp = (from a in db.mApps
                        select a).Count();
            sizeHost= Conn.getInfo(@"SELECT Convert(varchar,ROUND(SUM(mf.size) * 8 / 1024, 2)+100)+'MB ('+ CONVERT(varchar,CAST(((ROUND(SUM(mf.size) * 8 / 1024, 2)+100)/3072.0)*100 AS numeric(18,0))) + '%)' as HT
                from sys.database_files mf");
            
            count_news_vn = (from a in db.rssNews
                             from b in db.mNewspaperMenus
                             from c in db.mNewspapers
                             where a.Effect == true && a.NewspaperMenuId == b.NewspaperMenuId && b.NewspaperId == c.NewspaperId && c.Languge.Equals("vie") 
                             //&& a.DateInput.Value.Equals("02/14/2019")
                             select a).Count();

            count_lantruycap = (from a in db.wViewIPs
                                select a).Count();
            count_luotxem = (from a in db.wViewsDates
                             select a).Count();

            data = "" + countApp + "#$%^" + sizeHost + "#$%^" + count_news_vn + "#$%^" + count_news_nn + "#$%^" + count_lantruycap + "#$%^" + count_luotxem;
            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }


        public string LoadRegionNews()
        {
            StringBuilder s = new StringBuilder();
           // s.Append("<div class='box-body' style='height:" + (ad.sHeight() - 370) + "px'>");
            s.Append("<table width='100%' class='mytable' style=''>");
            s.Append("<thead>");
            s.Append("<tr style='height:50px'>");          
            s.Append("<th class='heading' style='width:50px;text-align:center'>Stt</th>");
            s.Append("<th class='heading' style='text-align:left'>Quốc gia</th>");
            s.Append("<th class='heading' style='width:70px;text-align:left'>S.L Web</th>");
            s.Append("<th class='heading' style='width:70px;text-align:left'>S.L App</th>");
            s.Append("<th class='heading' style='width:70px;text-align:center'>S.L Tin</th>");            
            s.Append("</tr>");
            s.Append("</thead>");
            s.Append("</table>");
            s.Append("<div class='scrollbar' style='overflow-y:overlay;max-height:" + (ad.sHeight() - 300) + "px'>");
            s.Append("<table id='listRegionNews' width='100%' class='mytable'>");
            s.Append("<tbody>");
            var regions = (from a in db.Regions
                          where a.HienThi == true
                          select a).OrderBy(x=>x.ThuTu);
            if (regions.Count() > 0)
            {
                int stt = 1;
                foreach (var r in regions)
                {
                    var slweb = (from a in db.mNewspapers
                                 where a.Languge.Equals(r.MaNuoc) && a.Display == true
                                 select a);

                    var slapp = (from a in db.mApps
                                 where a.MaNuoc.Equals(r.MaNuoc) && a.HieuLuc == true
                                 select a);
                    var sltin = (from a in db.rssNews
                                 from b in db.mNewspaperMenus
                                 from c in db.mNewspapers
                                 where a.Effect == true && a.NewspaperMenuId == b.NewspaperMenuId && b.NewspaperId == c.NewspaperId && c.Languge.Equals(r.MaNuoc)
                                 //&& a.DateInput.Value.Equals("02/14/2019")
                                 select a);


                    s.Append("<tr style='font-size:18px;cursor:pointer;'>");
                    s.Append("<td style='width:50px; text-align:center'>" + stt + "</td>");
                    s.Append("<td style='text-align:left'>" + r.TenNuoc + "</td>");
                    s.Append("<td style='width:70px;text-align:center'>" + slweb.Count() + "</td>");
                    s.Append("<td style='width:70px;text-align:center'>" + slapp.Count() + "</td>");
                    s.Append("<td style='width:70px;text-align:center'>" + sltin.Count() + "</td>");

                    s.Append("</tr>");
                    stt++;
                }
            }
           
            s.Append("</tbody>");          
            s.Append("</table>");
            s.Append("</div>");
            //s.Append("</div>");
            return "" + s;
        }
        public string LoadChart()
        {
            StringBuilder s = new StringBuilder();

           
            return "" + s;
        }
    }
}