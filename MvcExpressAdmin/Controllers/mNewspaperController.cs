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
    public class mNewspaperController : PerSiteController
    {
        // GET: mNewspaper
        private NewsDataContext db = new NewsDataContext();
        AdminSiteController AS = new AdminSiteController();
        string languge = CookieCls.GetLanguge();

        public string LoadData()
        {
            StringBuilder s = new StringBuilder();
            try
            {

                s.Append("<table width='100%' class='mytable' id=''>");
                s.Append("<thead>");
                s.Append("<tr style='height:50px;'>");
                s.Append("<td style='width:40px;height:55px' align='center'><input type='button' class='additem' id='adddanhmucm' value='&nbsp;' onclick=\"AddFlatform()\"></td>");
                s.Append("<td style='width:40px; text-align:center; font-size:18px'>" + AS.GetTT("tt_id") + "</td>");
                s.Append("<td style='width:120px;text-align:left; font-size:18px'>Tên danh mục</td>");
                s.Append("<td style='width:160px;text-align:left; font-size:18px'>Link website</td>");
                s.Append("<td style='width:50px;text-align:left; font-size:18px'>Icon</td>");

                s.Append("<td style='width:50px;text-align:center; font-size:18px'>Menu left</td>");

                s.Append("<td style='width:50px;text-align:center; font-size:18px'>Menu top</td>");
                s.Append("<td style='width:50px;text-align:center; font-size:18px'>Video</td>");

                s.Append("<td style='width:50px;text-align:center; font-size:18px'>" + AS.GetTT("tt_hieuluc") + "</td>");
                s.Append("<td style='width:50px;text-align:center; font-size:18px'>" + AS.GetTT("tt_order") + "</td>");

                s.Append("</tr>");

                s.Append("</thead>");
                s.Append("</table>");

                s.Append("<div style='margin: 0 auto;overflow: auto;width: 100%;position: relative;height:" + (AS.sHeight() - 190) + "px'>");
                s.Append("<table width='100%' class='mytable' id='listMenu' style='' >");
                s.Append("<tbody>");
                var dt = from a in db.mNewspapers
                         where a.Languge.Equals("vie")
                         select a;
                if (dt.Count() > 0)
                {
                    foreach (var r in dt.OrderBy(x => x.Stt))
                    {
                        s.Append("<tr id='tr1_" + r.NewspaperId + "'>");
                        s.Append("<td  align='center' style='width:40px;background-color:#b5ebf8;' class='heading'>");
                        s.Append("<input type='button' class='add2' id='a" + r.NewspaperId + "'onclick=\"AddDanhmuc('" + r.NewspaperId + "')\">");
                        s.Append("<input type='button' class='imgdel' id='d" + r.NewspaperId + "'  onclick=\"DeleteM1('" + r.NewspaperId + "',1)\">");
                        s.Append("</td>");
                        s.Append("<td  style='width:40px;background-color:#b5ebf8' class='heading'  align='center' id='td1_" + r.NewspaperId + "'>" + r.NewspaperId + "</td>");
                        s.Append("<td class='heading' style='width:120px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' colspan='2' id='tname1_" + r.NewspaperId + "'><input class='txtn' type='text' id='txtMenu1" + r.NewspaperId + "' value='" + r.Title + "' onchange='Update_Name_Menu1(" + r.NewspaperId + ")' style='width:95%;'></td>");
                        s.Append("<td class='heading' style='width:160px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' id='tdlinkweb" + r.NewspaperId + "'><input class='txtn' type='text' id='txtLinkwebsite" + r.NewspaperId + "' value='" + r.Website + "' onchange='Update_Link_Website(" + r.NewspaperId + ")' style='width:95%;'></td>");
                        s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;'><img onclick=\"ChangeIcon(" + r.NewspaperId + " ,'" + r.Icon + "')\" style='width:40px; height:40px; ' src='" + r.Icon + "' /></td>");

                        s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:center'><img onclick=\"PopupMenuLeft(" + r.NewspaperId + ")\" style='width:40px; height:40px; ' src='/images/add.png' /></td>");


                        s.Append("<td style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left'><label class='lbcheck' style='z-index:1000'></td>");
                        s.Append("<td style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left'><label class='lbcheck' style='z-index:1000'></td>");

                        string HT = r.Display == true ? " checked=checked" : "";
                        s.Append("<td style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT + " type='checkbox' onchange=\"UpdateHienThi1('" + r.NewspaperId + "')\"  id='chkCate_" + r.NewspaperId + "'> <span class='text' style='z-index:0'></span></td>");
                        s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' id='n" + r.NewspaperId + "'><input class='txtn' type='text' id='txtSTT_Type" + r.NewspaperId + "' value='" + r.Stt + "' onchange='UpdateSTT_Menu1(" + r.NewspaperId + ")' style='width:70%;'></td>");
                        s.Append("</tr>");

                        var dt2 = from a in db.mNewspaperMenus
                                  where a.NewspaperId == r.NewspaperId
                                  select new { a.NewspaperMenuId, a.NewspaperId, a.Title, a.Display, a.Stt, a.rssLink, a.TopMenu, a.GroupApp, a.Video };
                        if (dt2.Count() > 0)
                        {
                            foreach (var r2 in dt2)
                            {
                                s.Append("<tr id='submenu" + r2.NewspaperMenuId + "'>");
                                s.Append("<td  align='center'  style='width:40px;background-color:#b5ebf8'>&nbsp;</td>");
                                s.Append("<td  align='center' style='width:40px;' class='heading'>");
                                s.Append("<input type='button' class='imgdel' id='sub_del" + r2.NewspaperMenuId + "'  onclick=\"DeleteM2('" + r2.NewspaperMenuId + "',2)\">");
                                s.Append("</td>");

                                s.Append("<td  style='margin-left:40px;width:40px;'  class='heading'  align='center' id='m" + r2.NewspaperMenuId + "'>" + r2.NewspaperMenuId + "</td>");
                                s.Append("<td class='heading'  style='width:80px; text-align:center'  id='n" + r2.NewspaperMenuId + "'><input class='txtn' type='text' id='txtNameCate" + r2.NewspaperMenuId + "' value='" + r2.Title + "' onchange='UpdateNameCate(" + r2.NewspaperMenuId + ")' style='width:90%'></td>");

                                s.Append("<td class='heading'  style='width:160px; text-align:center'  id='rss" + r2.NewspaperMenuId + "'><input class='txtn' type='text' id='txtRss" + r2.NewspaperMenuId + "' value='" + r2.rssLink + "' onchange='UpdateRssLink(" + r2.NewspaperMenuId + ")' style='width:90%'></td>");

                                s.Append("<td class='heading'></td>");
                                s.Append("<td class='heading'></td>");
                                string HT2 = r2.Display == true ? " checked=checked" : "";
                                string topmenu = r2.TopMenu == true ? " checked=checked" : "";
                                string video = r2.Video == true ? " checked=checked" : "";
                                s.Append("<td style='width:50px; text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + topmenu + " type='checkbox' onchange=\"UpdateTopMenu('" + r2.NewspaperMenuId + "')\"  id='chkTopMenu_" + r2.NewspaperMenuId + "'> <span class='text' style='z-index:0'></span></td>");
                                s.Append("<td style='width:50px; text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + video + " type='checkbox' onchange=\"UpdateVideo('" + r2.NewspaperMenuId + "')\"  id='chkVideo_" + r2.NewspaperMenuId + "'> <span class='text' style='z-index:0'></span></td>");

                                s.Append("<td style='width:50px; text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT2 + " type='checkbox' onchange=\"UpdateHienThi2('" + r2.NewspaperMenuId + "')\"  id='chkCate2_" + r2.NewspaperMenuId + "'> <span class='text' style='z-index:0'></span></td>");

                                s.Append("<td class='heading'  style='width:50px; text-align:center'  id='n" + r2.NewspaperMenuId + "'><input class='txtn' type='text' id='txtSTT" + r2.NewspaperMenuId + "' value='" + r2.Stt + "' onchange='UpdateSTT(" + r2.NewspaperMenuId + ")' style='width:70%;'></td>");

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

        [HttpPost]
        public ActionResult LOAD_DANHMUC_AJAX(string Region)
        {
            StringBuilder s = new StringBuilder();
            string data = "";
            s.Append("<tbody>");
            var dt = from a in db.mNewspapers
                     where a.Languge.Equals(Region)
                     select a;
            if (dt.Count() > 0)
            {
                foreach (var r in dt.OrderBy(x => x.Stt))
                {
                    s.Append("<tr id='tr1_" + r.NewspaperId + "'>");
                    s.Append("<td  align='center' style='width:40px;background-color:#b5ebf8;' class='heading'>");
                    s.Append("<input type='button' class='add2' id='a" + r.NewspaperId + "'onclick=\"AddDanhmuc('" + r.NewspaperId + "')\">");
                    s.Append("<input type='button' class='imgdel' id='d" + r.NewspaperId + "'  onclick=\"DeleteM1('" + r.NewspaperId + "',1)\">");
                    s.Append("</td>");
                    s.Append("<td  style='width:40px;background-color:#b5ebf8' class='heading'  align='center' id='td1_" + r.NewspaperId + "'>" + r.NewspaperId + "</td>");
                    s.Append("<td class='heading' style='width:120px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' colspan='2' id='tname1_" + r.NewspaperId + "'><input class='txtn' type='text' id='txtMenu1" + r.NewspaperId + "' value='" + r.Title + "' onchange='Update_Name_Menu1(" + r.NewspaperId + ")' style='width:95%;'></td>");
                    s.Append("<td class='heading' style='width:160px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' id='tdlinkweb" + r.NewspaperId + "'><input class='txtn' type='text' id='txtLinkwebsite" + r.NewspaperId + "' value='" + r.Website + "' onchange='Update_Link_Website(" + r.NewspaperId + ")' style='width:95%;'></td>");
                    s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;'><img onclick=\"ChangeIcon(" + r.NewspaperId + " ,'" + r.Icon + "')\" style='width:40px; height:40px; ' src='" + r.Icon + "' /></td>");

                    s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:center'><img onclick=\"PopupMenuLeft(" + r.NewspaperId + ")\" style='width:40px; height:40px; ' src='/images/add.png' /></td>");


                    s.Append("<td style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left'><label class='lbcheck' style='z-index:1000'></td>");
                    s.Append("<td style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left'><label class='lbcheck' style='z-index:1000'></td>");

                    string HT = r.Display == true ? " checked=checked" : "";
                    s.Append("<td style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT + " type='checkbox' onchange=\"UpdateHienThi1('" + r.NewspaperId + "')\"  id='chkCate_" + r.NewspaperId + "'> <span class='text' style='z-index:0'></span></td>");
                    s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' id='n" + r.NewspaperId + "'><input class='txtn' type='text' id='txtSTT_Type" + r.NewspaperId + "' value='" + r.Stt + "' onchange='UpdateSTT_Menu1(" + r.NewspaperId + ")' style='width:70%;'></td>");
                    s.Append("</tr>");

                    var dt2 = from a in db.mNewspaperMenus
                              where a.NewspaperId == r.NewspaperId
                              select new { a.NewspaperMenuId, a.NewspaperId, a.Title, a.Display, a.Stt, a.rssLink, a.TopMenu, a.GroupApp, a.Video };
                    if (dt2.Count() > 0)
                    {
                        foreach (var r2 in dt2)
                        {
                            s.Append("<tr id='submenu" + r2.NewspaperMenuId + "'>");
                            s.Append("<td  align='center'  style='width:40px;background-color:#b5ebf8'>&nbsp;</td>");
                            s.Append("<td  align='center' style='width:40px;' class='heading'>");
                            s.Append("<input type='button' class='imgdel' id='sub_del" + r2.NewspaperMenuId + "'  onclick=\"DeleteM2('" + r2.NewspaperMenuId + "',2)\">");
                            s.Append("</td>");

                            s.Append("<td  style='margin-left:40px;width:40px;'  class='heading'  align='center' id='m" + r2.NewspaperMenuId + "'>" + r2.NewspaperMenuId + "</td>");
                            s.Append("<td class='heading'  style='width:80px; text-align:center'  id='n" + r2.NewspaperMenuId + "'><input class='txtn' type='text' id='txtNameCate" + r2.NewspaperMenuId + "' value='" + r2.Title + "' onchange='UpdateNameCate(" + r2.NewspaperMenuId + ")' style='width:90%'></td>");

                            s.Append("<td class='heading'  style='width:160px; text-align:center'  id='rss" + r2.NewspaperMenuId + "'><input class='txtn' type='text' id='txtRss" + r2.NewspaperMenuId + "' value='" + r2.rssLink + "' onchange='UpdateRssLink(" + r2.NewspaperMenuId + ")' style='width:90%'></td>");

                            s.Append("<td class='heading'></td>");
                            s.Append("<td class='heading'></td>");
                            string HT2 = r2.Display == true ? " checked=checked" : "";
                            string topmenu = r2.TopMenu == true ? " checked=checked" : "";
                            string video = r2.Video == true ? " checked=checked" : "";
                            s.Append("<td style='width:50px; text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + topmenu + " type='checkbox' onchange=\"UpdateTopMenu('" + r2.NewspaperMenuId + "')\"  id='chkTopMenu_" + r2.NewspaperMenuId + "'> <span class='text' style='z-index:0'></span></td>");
                            s.Append("<td style='width:50px; text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + video + " type='checkbox' onchange=\"UpdateVideo('" + r2.NewspaperMenuId + "')\"  id='chkVideo_" + r2.NewspaperMenuId + "'> <span class='text' style='z-index:0'></span></td>");

                            s.Append("<td style='width:50px; text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT2 + " type='checkbox' onchange=\"UpdateHienThi2('" + r2.NewspaperMenuId + "')\"  id='chkCate2_" + r2.NewspaperMenuId + "'> <span class='text' style='z-index:0'></span></td>");

                            s.Append("<td class='heading'  style='width:50px; text-align:center'  id='n" + r2.NewspaperMenuId + "'><input class='txtn' type='text' id='txtSTT" + r2.NewspaperMenuId + "' value='" + r2.Stt + "' onchange='UpdateSTT(" + r2.NewspaperMenuId + ")' style='width:70%;'></td>");

                            s.Append("</tr>");
                        }
                    }
                }
            }

            s.Append("</tbody>");
            data = "" + s;
            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update_Name_Newspaper(string mID, string sNAME)
        {
            int ID = int.Parse(mID);
            mNewspaper mn = db.mNewspapers.SingleOrDefault(r => r.NewspaperId == ID);
            if (mn != null)
            {
                mn.Title = sNAME;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update_Link_Website(string mID, string sLINK)
        {
            int ID = int.Parse(mID);
            mNewspaper mn = db.mNewspapers.SingleOrDefault(r => r.NewspaperId == ID);
            if (mn != null)
            {
                mn.Website = sLINK;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateSTT_Newspaper(string mID, string sSTT)
        {
            int ID = int.Parse(mID);
            int stt = int.Parse(sSTT);
            mNewspaper mn = db.mNewspapers.SingleOrDefault(r => r.NewspaperId == ID);
            if (mn != null)
            {
                mn.Stt = stt;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateTitle_NewsMenu(string sID, string sNAME)
        {
            int ID = int.Parse(sID);
            mNewspaperMenu mn = db.mNewspaperMenus.SingleOrDefault(r => r.NewspaperMenuId == ID);
            if (mn != null)
            {
                mn.Title = sNAME;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateRssLink_NewsMenu(string sID, string sRSSLINK)
        {
            int ID = int.Parse(sID);
            mNewspaperMenu mn = db.mNewspaperMenus.SingleOrDefault(r => r.NewspaperMenuId == ID);
            if (mn != null)
            {
                mn.rssLink = sRSSLINK;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateSTT(string sID, string sSTT)
        {
            int ID = int.Parse(sID);
            mNewspaperMenu mn = db.mNewspaperMenus.SingleOrDefault(r => r.NewspaperMenuId == ID);
            if (mn != null)
            {
                mn.Stt = int.Parse(sSTT);
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateTopMenu(string sID, string sTopMenu)
        {
            int ID = int.Parse(sID);
            mNewspaperMenu mn = db.mNewspaperMenus.SingleOrDefault(r => r.NewspaperMenuId == ID);
            if (mn != null)
            {
                mn.TopMenu = sTopMenu == "true" ? true : false;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateVideo(string sID, string sVideo)
        {
            int ID = int.Parse(sID);
            mNewspaperMenu mn = db.mNewspaperMenus.SingleOrDefault(r => r.NewspaperMenuId == ID);
            if (mn != null)
            {
                mn.Video = sVideo == "true" ? true : false;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateHienThi2(string sID, string sHieuLuc)
        {
            int ID = int.Parse(sID);
            mNewspaperMenu mn = db.mNewspaperMenus.SingleOrDefault(r => r.NewspaperMenuId == ID);
            if (mn != null)
            {
                mn.Display = sHieuLuc == "true" ? true : false;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LOADMENU_BY_NEWSPAPERID(string sNewspaperId)
        {
            StringBuilder s = new StringBuilder();
            string data = "";
            s.Append("<table width='100%' class='mytable' style='width:100%;'>");
            s.Append("<thead>");
            s.Append("<tr style='height:50px;'>");

            s.Append("<td style='width:40px;height:55px' align='center'><input type='button' class='additem' id='idmnleft' value='&nbsp;' onclick=\"AddMenuLeft("+ sNewspaperId + ")\">");


            s.Append("</td>");
            s.Append("<td style='width:40px; text-align:center; font-size:18px'>" + AS.GetTT("tt_id") + "</td>");

            s.Append("<td style='width:110px;text-align:left; font-size:18px'>Menu left</td>");

            s.Append("<td style='width:170px;text-align:left; font-size:18px'>Link</td>");


            s.Append("<td style='width:50px;text-align:center; font-size:18px'>" + AS.GetTT("tt_hieuluc") + "</td>");
            s.Append("<td style='width:50px;text-align:center; font-size:18px'>" + AS.GetTT("tt_order") + "</td>");

            s.Append("</tr>");


            s.Append("</thead></table>");
            s.Append("<div class='scrollbar' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 400) + "px'>");
            s.Append("<table width='100%' class='mytable' id='dsMenuLeft' style='' >");
            s.Append("<tbody>");
            int ID = int.Parse(sNewspaperId);
            var dt = from a in db.LMenuLefts
                     where a.NewspaperId == ID
                     select a;
            if (dt.Count() > 0)
            {
                foreach (var r in dt)
                {
                    s.Append("<tr id='trleft_" + r.IDMenuLeft + "'>");

                    s.Append("<td  align='center' style='width:40px;background-color:#b5ebf8;' class='heading'>");

                    s.Append("<input type='button' class='add2' id='a" + r.IDMenuLeft + "'onclick=\"AddLeft(" + r.IDMenuLeft + ")\">");


                    s.Append("<input type='button' class='imgdel' id='d" + r.IDMenuLeft + "'  onclick=\"DeleteMenuLeft(" + r.IDMenuLeft + ")\">");
                    s.Append("</td>");

                    s.Append("<td  style='width:30px;background-color:#b5ebf8' class='heading'  align='center' id='td1_" + r.IDMenuLeft + "'>" + r.IDMenuLeft + "</td>");
                    s.Append("<td class='heading' style='width:80px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' colspan='3' id='titleLeft" + r.IDMenuLeft + "'><input class='txtn' type='text' id='txtTitleLeft" + r.IDMenuLeft + "' value='" + r.Title + "' onchange='Update_Title_MenuLeft(" + r.IDMenuLeft + ")' style='width:96%;'></td>");

                    //s.Append("<td  align='center' style='width:120px;background-color:#b5ebf8' class='heading'></td>");

                    string HT = r.Display == true ? " checked=checked" : "";

                    s.Append("<td style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT + " type='checkbox' onchange=\"UpdateDisplay_MenuLeft('" + r.IDMenuLeft + "')\"  id='chkMenuLeft" + r.IDMenuLeft + "'> <span class='text' style='z-index:0'></span></td>");
                    s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' id='n" + r.IDMenuLeft + "'><input class='txtn' type='text' id='txtSTTLeft" + r.IDMenuLeft + "' value='" + r.STT + "' onchange='UpdateSTT_MenuLeft(" + r.IDMenuLeft + ")' style='width:70%;'></td>");
                    s.Append("</tr>");
                    int id2 = r.IDMenuLeft;
                    var dt2 = from a in db.LMenuByLefts
                              where a.IDMenuLeft == id2
                              select a;
                    int colspan = dt2.Count() > 0 ? 2 : 0;
                    if (dt2.Count() > 0)
                    {
                        foreach (var r2 in dt2)
                        {
                            s.Append("<tr id='submenuleft" + r2.IDMenu + "'>");
                            s.Append("<td  align='center'  style='width:40px;background-color:#b5ebf8'>&nbsp;</td>");


                            s.Append("<td  align='center' style='width:40px;' class='heading'>");
                            s.Append("<input type='button' class='imgdel' id='sub_del" + r2.IDMenu + "'  onclick=\"DeleteMenu(" + r2.IDMenu + ")\">");
                            s.Append("</td>");

                            s.Append("<td  style='width:30px;'  class='heading'  align='center' id='m" + r2.IDMenu + "'>" + r2.IDMenu + "</td>");
                            s.Append("<td class='heading'  style='width:80px; text-align:center'  id='n" + r2.IDMenu + "'><input class='txtn' type='text' id='txtTitleMenu" + r2.IDMenu + "' value='" + r2.Title + "' onchange='UpdateTitleMenu(" + r2.IDMenu + ")' style='width:90%'></td>");
                            s.Append("<td class='heading'  style='width:170px; text-align:center'  id='l" + r2.IDMenu + "'><input class='txtn' type='text' id='txtLinkMenu" + r2.IDMenu + "' value='" + r2.Link + "' onchange='UpdateLinkMenu(" + r2.IDMenu + ")' style='width:90%'></td>");
                            string HT2 = r2.Display == true ? " checked=checked" : "";

                            s.Append("<td style='width:50px; text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT2 + " type='checkbox' onchange=\"UpdateDisplayMenu('" + r2.IDMenu + "')\"  id='chkDisplayMenu" + r2.IDMenu + "'> <span class='text' style='z-index:0'></span></td>");
                            s.Append("<td class='heading'  style='width:50px; text-align:center'  id='n" + r2.IDMenu + "'><input class='txtn' type='text' id='txtSTTMenu" + r2.IDMenu + "' value='" + r2.STT + "' onchange='UpdateSTTMenu(" + r2.IDMenu + ")' style='width:70%; text-align:right'></td>");

                            s.Append("</tr>");
                        }
                    }
                }
            }
            s.Append("</tbody>");
            s.Append("</table></div>");
            data = "" + s;


            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LOAD_MENU_AJAX(string sNewspaperId)
        {
            StringBuilder s = new StringBuilder();
            string data = "";
          
            s.Append("<tbody>");
            int ID = int.Parse(sNewspaperId);
            var dt = from a in db.LMenuLefts
                     where a.NewspaperId == ID
                     select a;
            if (dt.Count() > 0)
            {
                foreach (var r in dt)
                {
                    s.Append("<tr id='trleft_" + r.IDMenuLeft + "'>");

                    s.Append("<td  align='center' style='width:40px;background-color:#b5ebf8;' class='heading'>");
                    s.Append("<input type='button' class='add2' id='a" + r.IDMenuLeft + "'onclick=\"AddLeft(" + r.IDMenuLeft + ")\">");
                    s.Append("<input type='button' class='imgdel' id='d" + r.IDMenuLeft + "'  onclick=\"DeleteMenuLeft(" + r.IDMenuLeft + ")\">");
                    s.Append("</td>");
                    s.Append("<td  style='width:30px;background-color:#b5ebf8' class='heading'  align='center' id='td1_" + r.IDMenuLeft + "'>" + r.IDMenuLeft + "</td>");
                    s.Append("<td class='heading' style='width:80px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' colspan='3' id='titleLeft" + r.IDMenuLeft + "'><input class='txtn' type='text' id='txtTitleLeft" + r.IDMenuLeft + "' value='" + r.Title + "' onchange='Update_Title_MenuLeft(" + r.IDMenuLeft + ")' style='width:96%;'></td>");

                    string HT = r.Display == true ? " checked=checked" : "";

                    s.Append("<td style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT + " type='checkbox' onchange=\"UpdateDisplay_MenuLeft('" + r.IDMenuLeft + "')\"  id='chkMenuLeft" + r.IDMenuLeft + "'> <span class='text' style='z-index:0'></span></td>");
                    s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' id='n" + r.IDMenuLeft + "'><input class='txtn' type='text' id='txtSTTLeft" + r.IDMenuLeft + "' value='" + r.STT + "' onchange='UpdateSTT_MenuLeft(" + r.IDMenuLeft + ")' style='width:70%;'></td>");
                    s.Append("</tr>");
                    int id2 = r.IDMenuLeft;
                    var dt2 = from a in db.LMenuByLefts
                              where a.IDMenuLeft == id2
                              select a;
                    int colspan = dt2.Count() > 0 ? 2 : 0;
                    if (dt2.Count() > 0)
                    {
                        foreach (var r2 in dt2)
                        {
                            s.Append("<tr id='submenuleft" + r2.IDMenu + "'>");
                            s.Append("<td  align='center'  style='width:40px;background-color:#b5ebf8'>&nbsp;</td>");


                            s.Append("<td  align='center' style='width:40px;' class='heading'>");
                            s.Append("<input type='button' class='imgdel' id='sub_del" + r2.IDMenu + "'  onclick=\"DeleteMenu(" + r2.IDMenu + ")\">");
                            s.Append("</td>");

                            s.Append("<td  style='width:30px;'  class='heading'  align='center' id='m" + r2.IDMenu + "'>" + r2.IDMenu + "</td>");
                            s.Append("<td class='heading'  style='width:80px; text-align:center'  id='n" + r2.IDMenu + "'><input class='txtn' type='text' id='txtTitleMenu" + r2.IDMenu + "' value='" + r2.Title + "' onchange='UpdateTitleMenu(" + r2.IDMenu + ")' style='width:90%'></td>");
                            s.Append("<td class='heading'  style='width:170px; text-align:center'  id='l" + r2.IDMenu + "'><input class='txtn' type='text' id='txtLinkMenu" + r2.IDMenu + "' value='" + r2.Link + "' onchange='UpdateLinkMenu(" + r2.IDMenu + ")' style='width:90%'></td>");
                            string HT2 = r2.Display == true ? " checked=checked" : "";

                            s.Append("<td style='width:50px; text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT2 + " type='checkbox' onchange=\"UpdateDisplayMenu('" + r2.IDMenu + "')\"  id='chkDisplayMenu" + r2.IDMenu + "'> <span class='text' style='z-index:0'></span></td>");
                            s.Append("<td class='heading'  style='width:50px; text-align:center'  id='n" + r2.IDMenu + "'><input class='txtn' type='text' id='txtSTTMenu" + r2.IDMenu + "' value='" + r2.STT + "' onchange='UpdateSTTMenu(" + r2.IDMenu + ")' style='width:70%; text-align:right'></td>");

                            s.Append("</tr>");
                        }
                    }
                }
            }
            s.Append("</tbody>");           
            data = "" + s;
            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update_Title_MenuLeft(string mID, string sTitle)
        {
            int ID = int.Parse(mID);
            LMenuLeft mn = db.LMenuLefts.SingleOrDefault(r => r.IDMenuLeft == ID);
            if (mn != null)
            {
                mn.Title = sTitle;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateSTT_MenuLeft(string mID, string sSTT)
        {
            int ID = int.Parse(mID);
            LMenuLeft mn = db.LMenuLefts.SingleOrDefault(r => r.IDMenuLeft == ID);
            if (mn != null)
            {
                mn.STT = int.Parse(sSTT);
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update_Title_Menu(string IDMenu, string sTitle)
        {
            int ID = int.Parse(IDMenu);
            LMenuByLeft mn = db.LMenuByLefts.SingleOrDefault(r => r.IDMenu == ID);
            if (mn != null)
            {
                mn.Title = sTitle;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateLinkMenu(string IDMenu, string sLink)
        {
            int ID = int.Parse(IDMenu);
            LMenuByLeft mn = db.LMenuByLefts.SingleOrDefault(r => r.IDMenu == ID);
            if (mn != null)
            {
                mn.Link = sLink;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateSTTMenu(string IDMenu, string sSTT)
        {
            int ID = int.Parse(IDMenu);
            LMenuByLeft mn = db.LMenuByLefts.SingleOrDefault(r => r.IDMenu == ID);
            if (mn != null)
            {
                mn.STT = int.Parse(sSTT);
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateDisplay_MenuLeft(string mID, string sHieuLuc)
        {
            int ID = int.Parse(mID);
            LMenuLeft mn = db.LMenuLefts.SingleOrDefault(r => r.IDMenuLeft == ID);
            if (mn != null)
            {
                mn.Display = sHieuLuc == "true" ? true : false;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateDisplayMenu(string IDMenu, string sHieuLuc)
        {
            int ID = int.Parse(IDMenu);
            LMenuByLeft mn = db.LMenuByLefts.SingleOrDefault(r => r.IDMenu == ID);
            if (mn != null)
            {
                mn.Display = sHieuLuc == "true" ? true : false;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Load_Title_ByID(string LeftID)
        {
            int ID = int.Parse(LeftID);
            string data = "";
            var v = (from a in db.LMenuLefts
                     where a.IDMenuLeft == ID
                     select new { a.Title }).SingleOrDefault();
            data = v.Title;
            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult INSERTMENUBYLEFT(int sLID, string sTitle, string sLink, string sHIEULUC)
        {
            LMenuByLeft entity = new LMenuByLeft();
            int ID = db.LMenuByLefts.Select(x => x.IDMenu).DefaultIfEmpty(0).Max() + 1;
            int? stt = db.LMenuByLefts.Select(x => x.STT).DefaultIfEmpty(0).Max() + 1;
            entity.IDMenu = ID;
            entity.IDMenuLeft = sLID;
            entity.Title = sTitle;
            entity.Display = true;
            entity.Link = sLink;
            entity.STT = stt;
            db.LMenuByLefts.Add(entity);
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteMenu(string IDMenu)
        {
            int madm = int.Parse(IDMenu);
            var cn = db.LMenuByLefts.Where(r => r.IDMenu == madm).ToList();
            if (cn.Count() > 0)
            {
                db.LMenuByLefts.RemoveRange(db.LMenuByLefts.Where(r => r.IDMenu == madm));
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadTrangBao(string IDTrangBao)
        {
            int ID = int.Parse(IDTrangBao);
            string data = "";
            var v = (from a in db.mNewspapers
                     where a.NewspaperId == ID
                     select new { a.Title }).SingleOrDefault();
            data = v.Title;
            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult INSERT_NEWSPAPERMENU(int sMID, string sNAME, string sRSSLINK, string sTOPMENU, string sVIDEO)
        {
            mNewspaperMenu entity = new mNewspaperMenu();
            int ID = db.mNewspaperMenus.Select(x => x.NewspaperMenuId).DefaultIfEmpty(0).Max() + 1;
            int? stt = db.mNewspaperMenus.Select(x => x.Stt).DefaultIfEmpty(0).Max() + 1;
            entity.NewspaperMenuId = ID;
            entity.NewspaperId = sMID;
            entity.Title = sNAME;
            entity.rssLink = sRSSLINK;
            entity.TopMenu = sTOPMENU == "true" ? true : false;
            entity.GroupApp = sRSSLINK == "" ? false : true;
            entity.Video = sVIDEO == "true" ? true : false;
            entity.Display = true;      
            entity.Stt = stt;
            db.mNewspaperMenus.Add(entity);
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteM2(string sID)
        {
            int ID = int.Parse(sID);
            var cn = db.mNewspaperMenus.Where(r => r.NewspaperMenuId == ID).ToList();
            if (cn.Count() > 0)
            {
                if(db.wNewsMenuIds.Where(r => r.NewspaperMenuId == ID).ToList().Count()>0)
                    db.wNewsMenuIds.RemoveRange(db.wNewsMenuIds.Where(r => r.NewspaperMenuId == ID));
                if (db.rssNews.Where(r => r.NewspaperMenuId == ID).ToList().Count() > 0)
                    db.rssNews.RemoveRange(db.rssNews.Where(r => r.NewspaperMenuId == ID));

                db.mNewspaperMenus.RemoveRange(db.mNewspaperMenus.Where(r => r.NewspaperMenuId == ID));
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult INSERTMENULEFT(int NewspaperId, string sTitle, string sHIEULUC)
        {
            LMenuLeft entity = new LMenuLeft();
            int ID = db.LMenuLefts.Select(x => x.IDMenuLeft).DefaultIfEmpty(0).Max() + 1;
            int? stt = db.LMenuLefts.Select(x => x.STT).DefaultIfEmpty(0).Max() + 1;
            entity.IDMenuLeft = ID;
            entity.NewspaperId = NewspaperId;
            entity.Title = sTitle;
            entity.Display = sHIEULUC == "true" ? true : false;           
            entity.STT = stt;
            db.LMenuLefts.Add(entity);
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteMenuLeft(string mID)
        {
            int ID = int.Parse(mID);
            var cn = db.LMenuByLefts.Where(r => r.IDMenuLeft == ID).ToList();
            if (cn.Count() > 0)
            {
                db.LMenuByLefts.RemoveRange(db.LMenuByLefts.Where(r => r.IDMenuLeft == ID));  
            }
            if (db.LMenuLefts.Where(r => r.IDMenuLeft == ID).ToList().Count() > 0)
                db.LMenuLefts.RemoveRange(db.LMenuLefts.Where(r => r.IDMenuLeft == ID));
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
    }
}