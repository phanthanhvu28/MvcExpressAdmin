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

        [HttpPost]
        public ActionResult Shrinkfile()
        {
            if (
            Conn.UpdateRowData(@"ALTER DATABASE expressviet_com_expressviet SET RECOVERY SIMPLE
            DBCC SHRINKFILE (expressviet_com_expressviet_log, 1)
            ALTER DATABASE expressviet_com_expressviet SET RECOVERY FULL"))
                return Json(1, JsonRequestBehavior.AllowGet);
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateWebsite()
        {
            Conn.UpdateRowData("update rssNews set  HotNews=0 where HotNews=1 update rssNews set  HotNews=1 where rssID in (select top 10 rssID from rssNews F1 inner join wNewsMenuId F2 on F1.NewspaperMenuId=F2.NewspaperMenuId where Convert(date,DateInput)=Convert(Date,getdate()) and isnull(PopularNews,0)=0 and isnull(MoreNews,0)=0 order by NEWID())");
            Conn.UpdateRowData("update rssNews set  PopularNews=0 where PopularNews=1 update rssNews set  PopularNews=1 where rssID in (select top 10 rssID from rssNews F1 inner join wNewsMenuId F2 on F1.NewspaperMenuId=F2.NewspaperMenuId where Convert(date,DateInput)=Convert(Date,getdate()) and isnull(MoreNews,0)=0 and isnull(HotNews,0)=0 order by NEWID())");
            Conn.UpdateRowData("update rssNews set  MoreNews=0 where  MoreNews=1 update rssNews set  MoreNews=1 where rssID in (select top 10 rssID from rssNews F1 inner join wNewsMenuId F2 on F1.NewspaperMenuId=F2.NewspaperMenuId where Convert(date,DateInput)=Convert(Date,getdate()) and isnull(PopularNews,0)=0 and isnull(HotNews,0)=0 order by NEWID())");
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteNewsTowDay()
        {
            if (Conn.UpdateRowData(@"delete rssNews where NewspaperMenuId in(select distinct NewspaperMenuId from rssNews where Convert(date,DateInput) = convert(date,getdate())) and Convert(date,DateInput) < convert(date,'" + DateTime.Now.AddDays(-2) + @"')"))
                return Json(1, JsonRequestBehavior.AllowGet);
            return Json(0, JsonRequestBehavior.AllowGet);

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
            s.Append("<div class='scrollbar' style='overflow-y:overlay;max-height:" + (ad.sHeight() - 340) + "px'>");
            s.Append("<table id='listRegionNews' width='100%' class='mytable table-hover'>");
            s.Append("<tbody>");
            var regions = (from a in db.Regions
                          where a.HienThi == true
                          select a).OrderBy(x=>x.ThuTu);
            int stt = 1, totalweb = 0, totalapp = 0, totalnews = 0;
            if (regions.Count() > 0)
            {
               
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
                    totalweb += slweb.Count();
                    totalapp += slapp.Count();
                    totalnews += sltin.Count();
                    s.Append("<tr style='font-size:18px;cursor:pointer;' onclick=\"ViewChart('" + r.MaNuoc + "','"+r.TenNuoc.ToUpper()+"')\">");
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
            s.Append("<tfooter>");
            s.Append("<tr>");
            s.Append("<td colspan=2 style='text-align:center;background-color:#00bfff;color:#FFF;font-weight:bold'>Total</td>");
            s.Append("<td style='width:70px;text-align:center;background-color:#0080ff;color:#FFF'>" + totalweb + "</td>");
            s.Append("<td style='width:70px;text-align:center;background-color:#0040ff;color:#FFF'>" + totalapp + "</td>");
            s.Append("<td style='width:70px;text-align:center;background-color:#0000ff;color:#FFF'>" + totalnews + "</td>");
            s.Append("</tr>");
            s.Append("</tfooter>");
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