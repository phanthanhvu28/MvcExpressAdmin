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
    public class UpdVideosController : PerSiteController
    {
        private NewsDataContext db = new NewsDataContext();
        AdminSiteController AS = new AdminSiteController();
        string languge = CookieCls.GetLanguge();

        public string LoadData()
        {
            StringBuilder s = new StringBuilder();
            try
            {
                s.Append("<div class='scrollbar1' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 150) + "px'>");
                s.Append("<table width='100%' class='mytable' id='dsMenu' style='' >");
                s.Append("<tbody>");
                decimal sumnews = 0;
                var dt1 = (from a in db.LMenuLefts
                           from b in db.mNewspapers
                           where a.NewspaperId == b.NewspaperId && a.Display == true
                           select new { a.NewspaperId, b.Title }).Distinct();
                if (dt1.Count() > 0)
                {

                    foreach (var r in dt1)
                    {
                        var dt2 = from a in db.LMenuLefts
                                  from b in db.mNewspapers
                                  where a.NewspaperId == b.NewspaperId && a.Display == true && a.NewspaperId == r.NewspaperId
                                  orderby a.STT ascending
                                  select new { a.IDMenuLeft, a.NewspaperId, a.Title };

                        s.Append("<tr id='trpaper_" + r.NewspaperId + "'>");
                        s.Append("<td class='heading' style='width:200px;height:50px;background-color:#b5ebf8;font-size:19px;font-weight:bold;color:blue' colspan='6'>" + r.Title + "</td>");
                        s.Append("</tr>");
                        if (dt2.Count() > 0)
                        {
                            foreach (var r2 in dt2)
                            {
                                s.Append("<tr id='tr1_" + r2.IDMenuLeft + "'>");
                                s.Append("<td class='heading' style='width:40px;height:50px;background-color:#b5ebf8;text-align:center'><label class='lbcheck' style='z-index:1000'><input class='chkhidden' value='" + r2.IDMenuLeft + "' type='checkbox' onclick=\"SelectNewspaper('chknewspaper','" + r2.IDMenuLeft + "')\" id='chknewspaper'> <span class='text'></span></td>");
                                s.Append("<td class='heading' style='width:200px;height:50px;background-color:#e7e9ed;font-size:19px;font-weight:bold;' colspan='5'>" + r2.IDMenuLeft + "-" + r2.Title + "</td>");
                                s.Append("</tr>");
                                var dt3 = from a in db.LMenuByLefts
                                          where a.Display == true && a.IDMenuLeft == r2.IDMenuLeft
                                          orderby a.STT ascending
                                          select new { a.IDMenu, a.Title, a.Link, a.DisplayWeb, a.DisplayHome };
                                if (dt3.Count() > 0)
                                {
                                    foreach (var r3 in dt3)
                                    {
                                        string web = r3.DisplayWeb == true ? " checked=checked" : "";
                                        var vcountVideo = from a in db.LNews
                                                          where a.IDMenu == r3.IDMenu
                                                          select a;
                                        sumnews += Conn.CVDecimal("" + vcountVideo.Count());
                                        s.Append("<tr id='submenu" + r3.IDMenu + "'>");
                                        s.Append("<td class='heading' style='width:40px;height:50px;background-color:#b5ebf8;'></td>");
                                        s.Append("<td  style='width:40px; Height:40px'  class='heading'  align='center'>" + r3.IDMenu + "</td>");

                                        s.Append("<td class='heading'  style='width:160px'>" + r3.Title + "</td>");
                                        s.Append("<td style='width:200px; text-align:left;'>" + r3.Link + "</td>");
                                        s.Append("<td style='width:80px; text-align:center; font-weight:bold; color:bule'>" + vcountVideo.Count() + "</td>");
                                        s.Append("<td style='width:70px; text-align:center; font-weight:bold; color:bule'><label class='lbcheck' style='z-index:1000' title='Hiển thị trên web' style='cursor:pointer'><input class='chkhidden'" + web + " type='checkbox' onchange=\"DisplayWeb(" + r3.IDMenu + ")\" id='chkweb" + r3.IDMenu + "'> <span class='text'></span></td>");

                                        s.Append("</tr>");
                                    }
                                }
                            }
                        }
                    }
                }
                s.Append("<tr>");
                s.Append("<td class='heading' colspan='4' style='height:50px;background-color:#1bbde1;font-weight:bold; color:Red; font-size:18px'>Total</td>");
                s.Append("<td class='heading' style='height:50px;background-color:#1bbde1; text-align:center;color:Red; font-weight:bold;font-size:18px'>" + sumnews + "</td>");
                s.Append("<td class='heading'  style='height:50px;background-color:#1bbde1;font-weight:bold; color:Red; font-size:18px'></td>");
                s.Append("</td>");
                s.Append("</tr>");
                s.Append("</tbody>");
                s.Append("</table></div>");
                return "" + s;
            }
            catch
            {
                return "";
            }
        }
        [HttpPost]
        public ActionResult DisplayWeb(string IDMenu, string DisplayWeb)
        {
            int ID = int.Parse(IDMenu);
            LMenuByLeft mn = db.LMenuByLefts.SingleOrDefault(r => r.IDMenu == ID);
            if (mn != null)
            {
                mn.DisplayWeb = DisplayWeb == "true" ? true : false;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
    }
}