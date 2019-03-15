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
    public class wConfigDefaultController : PerSiteController
    {
        private NewsDataContext db = new NewsDataContext();
        AdminSiteController AS = new AdminSiteController();
        string languge = CookieCls.GetLanguge();

        public string LoadData()
        {
            StringBuilder s = new StringBuilder();
            try
            {
                // s.Append("<div class='scrollbar' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 220) + "px'>");
                s.Append("<div style='margin: 0 auto;overflow: auto;width: 100%;height:" + (AS.sHeight() - 190) + "px'>");
                s.Append("<table width='100%' class='mytable' id='listmenu'>");
                s.Append("<thead>");
                s.Append("<tr style='height:50px'>");
                s.Append("<th class='heading' colspan=2 style='width:200px;text-align:left;font-size:18px'>Cấu hình trang chủ website</th>");
                s.Append("<th class='heading' style='width:200px;text-align:left;font-size:18px'>List menu</th>");
                s.Append("<th class='heading' style='width:100px;text-align:left;font-size:18px'>P.Right</th>");
                s.Append("<th class='heading' style='width:100px;text-align:center;font-size:18px'>Upper video</th>");
                s.Append("<th class='heading' style='width:50px;text-align:center;font-size:18px'>Column</th>");
                s.Append("<th class='heading' style='width:50px;text-align:center;font-size:18px'>Slider</th>");
                s.Append("<th class='heading' style='width:50px;text-align:center;font-size:18px'>Tab</th>");

                s.Append("</tr>");
                s.Append("</thead>");
                //s.Append("</table>");
                //s.Append("<div class='scrollbar' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 220) + "px'>");
                //s.Append("<table width='100%' class='mytable' id='dsMenu' style='' >");
                s.Append("<tbody>");

                var dt1 = (from a in db.wMenu1                           
                           select new { a.mID, a.Name, a.Effect, a.Folder, a.STT }).Where(d => d.Effect == true);
                if (dt1.Count() > 0)
                {
                    foreach (var r in dt1)
                    {
                        int ID = r.mID;
                        string FRight = "";
                        string UpperVideo = "";
                        string column = "";
                        string slider = "";
                        string tab = "";
                        var dt2 = (from a in db.wFormats
                                   where a.mID == ID
                                   select new { a.TID, a.FRight, a.UpperVideo }).SingleOrDefault();
                        if (dt2 != null)
                        {

                            FRight = dt2.FRight == true ? " checked=checked" : "";
                            UpperVideo = dt2.UpperVideo == true ? " checked=checked" : "";

                            column = dt2.TID == 1 ? " checked=checked" : "";
                            slider = dt2.TID == 2 ? " checked=checked" : "";
                            tab = dt2.TID == 3 ? " checked=checked" : "";
                        }

                        s.Append("<tr id='tr1_" + r.mID + "'>");

                        s.Append("<td class='heading' style='width:400px;height:50px;background-color:#b5ebf8;font-size:19px;font-weight:bold;' colspan='3'>" + r.Name + "</td>");

                        s.Append("<td style='width:100px; text-align:left;background-color:#b5ebf8'> <label class='lbcheck' style='z - index:1000'><input class='chkhidden' value='" + r.mID + "' type='checkbox' " + FRight + " onclick=\"CheckRight(" + r.mID + ")\" id='chkRight" + r.mID + "'> <span class='text'></span></label></td>");
                        s.Append("<td style='width:100px; text-align:center;background-color:#b5ebf8'> <label class='lbcheck' style='z - index:1000'><input class='chkhidden' value='" + r.mID + "' type='checkbox' " + UpperVideo + " onclick=\"CheckUpperVideo(" + r.mID + ")\" id='chkUpperVideo" + r.mID + "'> <span class='text'></span></label></td>");

                        s.Append("<td style='width:50px; text-align:center;background-color:#b5ebf8'> <label class='lbcheck' style='z - index:1000'><input class='chkhidden' value='" + r.mID + "' type='checkbox' " + column + " onclick=\"SelectFormat(" + r.mID + ",1)\" id='chkColumn" + r.mID + "'> <span class='text'></span></label></td>");
                        s.Append("<td style='width:50px; text-align:center;background-color:#b5ebf8'> <label class='lbcheck' style='z - index:1000'><input class='chkhidden' value='" + r.mID + "' type='checkbox' " + slider + " onclick=\"SelectFormat(" + r.mID + ",2)\" id='chkSlider" + r.mID + "'> <span class='text'></span></label></td>");

                        s.Append("<td style='width:50px; text-align:center;background-color:#b5ebf8'> <label class='lbcheck' style='z - index:1000'><input class='chkhidden' value='" + r.mID + "' type='checkbox' " + tab + " onclick=\"SelectFormat(" + r.mID + ",3)\" id='chkTab" + r.mID + "'> <span class='text'></span></label></td>");

                        s.Append("</tr>");

                        var dt3 = from a in db.wMenu2
                                  where a.Effect == true && a.mID == r.mID
                                  orderby a.STT ascending
                                  select new { a.sID, a.mID, a.Name, a.Effect, a.STT };
                        if (dt3.Count() > 0)
                        {
                            foreach (var r2 in dt3)
                            {
                                s.Append("<tr id='submenu" + r2.sID + "'>");
                                s.Append("<td class='heading'  style='width:40px; Height:40px'  align='center'>" + r2.sID + "</td>");
                                s.Append("<td class='heading' style='width:160px'>" + r2.Name + "</td>");
                                s.Append("<td class='heading' id='Rload" + r2.sID + "' style='width:200px; text-align:left;'>" + GetListMenu(r2.sID) + "</td>");
                                s.Append("<td class='heading' style='width:100px; text-align:center'> <input type='button' class='add2' id='sid" + r2.sID + "' onclick=\"LoadNewsPaperMenu(" + r2.sID + ")\"> <input type='button' class='list' id='list" + r2.sID + "' onclick=\"LoadListSelected(" + r2.sID + ")\"></td>");
                                s.Append("<td class='heading' colspan='4' style=''> </td>");
                                s.Append("</tr>");
                            }
                        }
                    }
                }
                s.Append("</tbody>");
                s.Append("</table>");
                s.Append("</div>");
                return "" + s;
            }
            catch
            {
                return "";
            }
        }
        StringBuilder GetListMenu(int sID)
        {
            StringBuilder s = new StringBuilder();
            var dt = from a in db.mNewspaperMenus
                     from b in db.wNewsMenuIds
                     from c in db.mNewspapers
                     where a.NewspaperMenuId == b.NewspaperMenuId && c.NewspaperId == a.NewspaperId && b.sID == sID
                     orderby a.Stt
                     select new { a.NewspaperMenuId, a.Title };
            if (dt.Count() > 0)
            {
                foreach (var r in dt)
                {
                    s.Append("<div style='margin-left:3px;margin-right: 5px;float:left;width:auto;vertical-align:middle;' class='boxper' align='left' id='mn" + sID + r.NewspaperMenuId + "'>");
                    s.Append("<label style='margin: 3px;'><input type='checkbox' checked='checkbox' class='checkbox-slider slider-icon colored-palegreen' id='idSelected2" + r.NewspaperMenuId + "' value='" + r.NewspaperMenuId + "' onclick=\"CheckListSelected2('" + sID + "'," + r.NewspaperMenuId + ")\"> <span class='text' id='lb" + r.NewspaperMenuId + "'></span>");
                    s.Append(" <span style='float:left; margin-right:5px;margin-top:3px;' title='" + r.NewspaperMenuId + "'>" + r.Title + "</span></label></div>");

                }
            }
            return s;
        }
        [HttpPost]
        public ActionResult ReloadListMenu(int sID)
        {
            StringBuilder s = new StringBuilder();
            string data = "";
            var dt = from a in db.mNewspaperMenus
                     from b in db.wNewsMenuIds
                     from c in db.mNewspapers
                     where a.NewspaperMenuId == b.NewspaperMenuId && c.NewspaperId == a.NewspaperId && b.sID == sID
                     orderby a.Stt
                     select new { a.NewspaperMenuId, a.Title };
            if (dt.Count() > 0)
            {
                foreach (var r in dt)
                {
                    s.Append("<div style='margin-left:3px;margin-right: 5px;float:left;width:auto;vertical-align:middle;' class='boxper' align='left' id='mn" + sID + r.NewspaperMenuId + "'>");
                    s.Append("<label style='margin: 3px;'><input type='checkbox' checked='checkbox' class='checkbox-slider slider-icon colored-palegreen' id='idSelected2" + r.NewspaperMenuId + "' value='" + r.NewspaperMenuId + "' onclick=\"CheckListSelected2('" + sID + "'," + r.NewspaperMenuId + ")\"> <span class='text' id='lb" + r.NewspaperMenuId + "'></span>");
                    s.Append(" <span style='float:left; margin-right:5px;margin-top:3px;' title='" + r.NewspaperMenuId + "'>" + r.Title + "</span></label></div>");

                }
            }
            data = "" + s;


            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CheckRight(string mID, string FRight)
        {
            int smID = int.Parse(mID);
            wFormat mn = db.wFormats.SingleOrDefault(r => r.mID == smID);
            if (mn != null)
            {
                mn.FRight = FRight == "true" ? true : false;
            }
            else
            {
                mn = new wFormat();
                mn.mID = smID;
                mn.TID = 1;
                mn.FRight = FRight == "true" ? true : false;
                db.wFormats.Add(mn);
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CheckUpperVideo(string mID, string UpperVideo)
        {
            int smID = int.Parse(mID);
            wFormat mn = db.wFormats.SingleOrDefault(r => r.mID == smID);
            if (mn != null)
            {
                mn.UpperVideo = UpperVideo == "true" ? true : false;
            }
            else
            {
                mn = new wFormat();
                mn.mID = smID;
                mn.TID = 1;
                mn.UpperVideo = UpperVideo == "true" ? true : false;
                db.wFormats.Add(mn);
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CheckFormat(string mID, string sTID, string sBit)
        {
            int smID = int.Parse(mID);
            int tID = int.Parse(sTID);
            if (sBit != "0")
            {
                wFormat mn = db.wFormats.SingleOrDefault(r => r.mID == smID);
                if (mn != null)
                {
                    mn.TID = tID;
                }
                else
                {
                    mn = new wFormat();
                    mn.mID = smID;
                    mn.TID = tID;
                    db.wFormats.Add(mn);
                }
            }
            else
            {
                if (db.wFormats.Where(r => r.mID == smID).Count() > 0)
                {
                    db.wFormats.RemoveRange(db.wFormats.Where(r => r.mID == smID));
                }
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ListMenuNewsPaper(string sID)
        {
            StringBuilder s = new StringBuilder();
            string LID = CookieCls.GetLanguge();
            int manv = int.Parse(CookieCls.GetMaNV());
            int ID = int.Parse(sID);
            string data = "";
            var dt = (from a in db.mNewspapers
                      from b in db.mNewspaperMenus
                      from c in db.PhanQuyenTBs
                      where a.Display == true && a.Languge.Equals("vie") && b.Display == true && c.MaNV == manv && a.NewspaperId==b.NewspaperId
                      orderby a.Stt ascending
                      select new { a.NewspaperId, a.Title, a.Stt }).Distinct();
            string[] clmenucha = { "bg-red", "bg-blue", "bg-purple", "bg-olive", "bg-yellow" };
            int cha = 0;
            string tenmenu = (from a in db.wMenu2 where a.sID == ID select new { a.Name }).FirstOrDefault().Name.ToUpper();
            if (dt.Count() > 0)
            {
                s.Append("<div class='scrollbar1' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 320) + "px;' >");
               // s.Append("<div class='box-header'><strong style='color:Red;font-size:20px'>Menu:</strong><h3 class='box-title' style='color:Blue'>" + (from a in db.wMenu2 where a.sID == ID select new { a.Name }).FirstOrDefault().Name + "</h3> </div>");
                s.Append("<ul class=\"timeline\" id=\"Loadlistmenu\">");

                foreach (var item in dt)
                {
                    int NewspaperId = item.NewspaperId;
                    s.Append("<li class=\"time-label\"> <span class=\"" + clmenucha[cha].ToString() + "\">" + item.Title + "</span> </li>");
                    cha++;
                    if (cha >= clmenucha.Count())
                    {
                        cha = 0;
                    }
                    //DataTable dt2 = Conn.getTable("select F1.NewspaperMenuId,F1.Title,(select count(*) From wNewsMenuId F2 where F2.sID='" + sID + "' and F2.NewspaperMenuId=F1.NewspaperMenuId) as Count_Menu from mNewspaperMenu F1 where F1.Display=1 and F1.NewspaperId=" + dt.Rows[i]["NewspaperId"].ToString() + " order by F1.STT");
                    var dt2 = from a in db.mNewspaperMenus
                              where a.Display == true && a.NewspaperId == NewspaperId
                              orderby a.Stt ascending
                              select new { a.NewspaperMenuId, a.Title };

                    string[] color = { "bg-blue", "bg-yellow", "bg-red", "bg-purple", "bg-olive" };

                    int c = 0;
                    if (dt2.Count() > 0)
                    {
                        foreach (var item2 in dt2)
                        {
                            var dt3 = from a in db.wNewsMenuIds
                                      where a.sID == ID && a.NewspaperMenuId == item2.NewspaperMenuId
                                      select a;
                            string checkdemenu = dt3.Count() == 0 ? "" : " checked=checked";

                            s.Append("<li> <i class=\"fa fa-check " + color[c].ToString() + "\"></i><div class=\"timeline-item\"><div style=\"margin:0;padding: 10px;font-size: 16px;\">" + item2.Title + "");
                            s.Append("<label class=\"lbcheck\" style=\"float: right;margin-top: 0px;\"><input type=\"checkbox\" " + checkdemenu + " id=\"idcheck" + item2.NewspaperMenuId + "\" onclick=\"CheckUpdateNewspaperId('" + sID + "'," + item2.NewspaperMenuId + ")\"/></label></div>");


                            c++;
                            if (c >= color.Count())
                            {
                                c = 0;
                            }
                            s.Append("</div></li>");
                        }
                    }
                }
                s.Append("<li><i class='fa fa-clock-o bg-gray'></i></li>");
                s.Append("</ul>");
                s.Append("</div>");
                data = "" + tenmenu + "#$%^" + s;

            }
            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ListSelected(string sID)
        {
            StringBuilder s = new StringBuilder();
            string LID = CookieCls.GetLanguge();
            int manv = int.Parse(CookieCls.GetMaNV());
            int ID = int.Parse(sID);
            string data = "";
            var dt = (from a in db.mNewspapers
                      from b in db.mNewspaperMenus
                      from c in db.wNewsMenuIds
                      where a.Display == true && a.Languge.Equals("vie") && b.Display == true && a.NewspaperId==b.NewspaperId && c.NewspaperMenuId == b.NewspaperMenuId && c.sID==ID
                      orderby a.Stt ascending
                      select new { a.NewspaperId, a.Title, a.Stt }).Distinct();
            string[] clmenucha = { "bg-red", "bg-blue", "bg-purple", "bg-olive", "bg-yellow" };
            int cha = 0;
            string tenmenu = (from a in db.wMenu2 where a.sID == ID select new { a.Name }).FirstOrDefault().Name.ToUpper();
            if (dt.Count() > 0)
            {
                //s.Append("<div class='scrollbar1' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 300) + "px;' >");
                
                s.Append("<ul class=\"timeline\" >");

                foreach (var item in dt)
                {
                    int NewspaperId = item.NewspaperId;
                    s.Append("<li class=\"time-label\"> <span class=\"" + clmenucha[cha].ToString() + "\">" + item.Title + "</span> </li>");
                    cha++;
                    if (cha >= clmenucha.Count())
                    {
                        cha = 0;
                    }
                    //DataTable dt2 = Conn.getTable("select F1.NewspaperMenuId,F1.Title,(select count(*) From wNewsMenuId F2 where F2.sID='" + sID + "' and F2.NewspaperMenuId=F1.NewspaperMenuId) as Count_Menu from mNewspaperMenu F1 where F1.Display=1 and F1.NewspaperId=" + dt.Rows[i]["NewspaperId"].ToString() + " order by F1.STT");
                    var dt2 = from a in db.mNewspaperMenus
                              from b in db.wNewsMenuIds
                              where a.Display == true && a.NewspaperId == NewspaperId && b.sID==ID && a.NewspaperMenuId==b.NewspaperMenuId
                              orderby a.Stt ascending
                              select new { a.NewspaperMenuId, a.Title };

                    string[] color = { "bg-blue", "bg-yellow", "bg-red", "bg-purple", "bg-olive" };

                    int c = 0;
                    if (dt2.Count() > 0)
                    {
                        foreach (var item2 in dt2)
                        {
                            var dt3 = from a in db.wNewsMenuIds
                                      where a.sID == ID && a.NewspaperMenuId == item2.NewspaperMenuId
                                      select a;
                            string checkdemenu = dt3.Count() == 0 ? "" : " checked=checked";

                            s.Append("<li id='limn"+item2.NewspaperMenuId+"'> <i class=\"fa fa-check " + color[c].ToString() + "\"></i><div class=\"timeline-item\"><div style=\"margin:0;padding: 10px;font-size: 16px;\">" + item2.Title + "");
                            s.Append("<label class=\"lbcheck\" style=\"float: right;margin-top: 0px;\"><input type=\"checkbox\" " + checkdemenu + " id=\"idSelected" + item2.NewspaperMenuId + "\" onclick=\"CheckListSelected('" + sID + "'," + item2.NewspaperMenuId + ")\"/></label></div>");


                            c++;
                            if (c >= color.Count())
                            {
                                c = 0;
                            }
                            s.Append("</div></li>");
                        }
                    }
                }
                s.Append("<li><i class='fa fa-clock-o bg-gray'></i></li>");
                s.Append("</ul>");
                //s.Append("</div>");              

            }
            else
            {
                s.Append("<strong style='color:Red; text-align:center; font-size:20px;margin-left:10px'>Chưa có trang báo</strong>");
            }
            data = "" + tenmenu + "#$%^" + s;
            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertNewsMenuId(string sID, string NewspaperMenuId, string chkMenu)
        {
            int sid = int.Parse(sID);
            int newspaperMenuId = int.Parse(NewspaperMenuId);
            if (chkMenu == "true")
            {
                wNewsMenuId mn = db.wNewsMenuIds.SingleOrDefault(r => r.sID == sid && r.NewspaperMenuId == newspaperMenuId);
                if (mn == null)
                {
                    mn = new wNewsMenuId();
                    mn.sID = sid;
                    mn.NewspaperMenuId = newspaperMenuId;
                    db.wNewsMenuIds.Add(mn);
                }
            }
            else
            {
                if (db.wNewsMenuIds.Where(r => r.sID == sid && r.NewspaperMenuId == newspaperMenuId).Count() > 0)
                {
                    db.wNewsMenuIds.RemoveRange(db.wNewsMenuIds.Where(r => r.sID == sid && r.NewspaperMenuId == newspaperMenuId));
                }
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
    }
}