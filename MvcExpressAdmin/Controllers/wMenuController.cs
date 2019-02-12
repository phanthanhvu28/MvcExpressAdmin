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
    public class wMenuController : PerSiteController
    {
        private NewsDataContext db = new NewsDataContext();
        AdminSiteController AS = new AdminSiteController();
        string languge = CookieCls.GetLanguge();
        // GET: wMenu
        public string LoadData()
        {
            StringBuilder s = new StringBuilder();
            try
            {
                s.Append("<table width='100%' class='mytable'>");
                s.Append("<thead>");
                s.Append("<tr style='height:50px'>");
                s.Append("<td style='width:40px;height:55px' align='center'><input type='button' class='additem' id='adddanhmucm' value='&nbsp;' onclick=\"AddFlatform()\">");
                s.Append("</td>");
                s.Append("<td style='width:40px; text-align:center; font-size:18px'>" + AS.GetTT("tt_id") + "</td>");

                s.Append("<td style='width:200px;text-align:left; font-size:18px'>" + AS.GetTT("tt_menu") + "</td>");
                //s.Append("<td style='width:50px;text-align:left; font-size:18px'>Icon</td>");
                //s.Append("<td style='width:100px;text-align:left; font-size:18px'>Thư mục</td>");

                s.Append("<td style='width:50px;text-align:center; font-size:18px'>" + AS.GetTT("tt_hieuluc") + "</td>");
                s.Append("<td style='width:50px;text-align:center; font-size:18px'>" + AS.GetTT("tt_order") + "</td>");
                s.Append("</tr>");
                s.Append("</thead></table>");
                s.Append("<div class='scrollbar1' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 210) + "px'>");
                s.Append("<table width='100%' class='mytable' id='dsdata' style='' >");
                s.Append("<tbody>");
                var dt = from F1 in db.wMenu1
                         orderby F1.STT ascending
                         select new { F1.mID, F1.Name, F1.Folder, F1.Icon, F1.Effect, F1.STT };
                if (dt.Count() > 0)
                {
                    foreach (var r in dt)
                    {
                        string HT = r.Effect == true ? " checked=checked" : "";
                        s.Append("<tr id='trdmcha" + r.mID + "'>");
                        s.Append("<td  align='center' style='width:40px;background-color:#b5ebf8;' class='heading'>");
                        s.Append("<input type='button' class='add2' id='a" + r.mID + "'onclick=\"AddDanhmuc('" + r.mID + "','" + r.Name + "')\">");
                        s.Append("<input type='button' class='imgdel' id='d" + r.mID + "'  onclick=\"DeleteM1('" + r.mID + "'," + r.STT + ")\">");
                        s.Append("</td>");

                        s.Append("<td  style='width:40px;background-color:#b5ebf8' class='heading'  align='center' id='td1_" + r.mID + "'>" + r.mID + "</td>");

                        s.Append("<td class='heading' style='width:200px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' colspan='2' id='tname1_" + r.mID + "'><input class='txtn' type='text' id='txtMenu1" + r.mID + "' value='" + r.Name + "' onchange='Update_Name_Menu1(" + r.mID + ")' style='width:100%;'></td>");

                        //s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;'><img onclick=\"ChangeIcon(" + r.mID + " ,'" + r.Icon + "')\" style='width:40px; height:40px; ' src='" + r.Icon + "' /></td>");
                        //s.Append("<td class='heading' style='width:100px;background-color:#b5ebf8;font-size:17px;font-weight:bold;'>" + r.Folder + "</td>");

                        s.Append("<td style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT + " type='checkbox' onchange=\"UpdateHienThi1('" + r.mID + "')\"  id='chkCate_" + r.mID + "'> <span class='text' style='z-index:0'></span></td>");
                        s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' id='n" + r.mID + "'><input class='txtn' type='text' id='txtSTT_Type" + r.mID + "' value='" + r.STT + "' onchange='UpdateSTT_Menu1(" + r.mID + ")' style='width:70%;'></td>");

                        s.Append("</tr>");
                        var dt2 = from F1 in db.wMenu2
                                  where F1.mID == r.mID
                                  orderby F1.STT ascending
                                  select new { F1.sID, F1.mID, F1.Name, F1.Effect, F1.STT };
                        if (dt2.Count() > 0)
                        {
                            foreach (var r2 in dt2)
                            {
                                string HT2 = r2.Effect == true ? " checked=checked" : "";
                                s.Append("<tr id='submenu" + r2.sID + "'>");
                                s.Append("<td  align='center'  style='width:40px;background-color:#b5ebf8'>&nbsp;</td>");
                                s.Append("<td  align='center' style='width:40px;' class='heading'>");
                                s.Append("<input type='button' class='imgdel' id='sub_del" + r2.sID + "'  onclick=\"DeleteM2('" + r2.sID + "',2)\">");
                                s.Append("</td>");

                                s.Append("<td  style='margin-left:40px;width:40px;'  class='heading'  align='center' id='m" + r2.sID + "'>" + r2.sID + "</td>");
                                s.Append("<td class='heading'  style='width:160px; text-align:center'  id='n" + r2.sID + "'><input class='txtn' type='text' id='txtNameCate" + r2.sID + "' value='" + r2.Name + "' onchange='UpdateNameCate(" + r2.sID + ")' style='width:100%'></td>");
                                //s.Append("<td class='heading'></td>");
                                //s.Append("<td class='heading'></td>");
                                s.Append("<td style='width:50px; text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT2 + " type='checkbox' onchange=\"UpdateHienThi2('" + r2.sID + "')\"  id='chkCate2_" + r2.sID + "'> <span class='text' style='z-index:0'></span></td>");

                                s.Append("<td class='heading'  style='width:50px; text-align:center'  id='n" + r2.sID + "'><input class='txtn' type='text' id='txtSTT" + r2.sID + "' value='" + r2.STT + "' onchange='UpdateSTT(" + r2.sID + ")' style='width:70%;'></td>");
                                s.Append("</tr>");
                            }
                        }
                    }
                }
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
        public ActionResult LoadAjaxData()
        {
            StringBuilder s = new StringBuilder();
            try
            {
                s.Append("<tbody>");
                var dt = from F1 in db.wMenu1
                         orderby F1.STT ascending
                         select new { F1.mID, F1.Name, F1.Folder, F1.Icon, F1.Effect, F1.STT };
                if (dt.Count() > 0)
                {
                    foreach (var r in dt)
                    {
                        string HT = r.Effect == true ? " checked=checked" : "";
                        s.Append("<tr id='trdmcha" + r.mID + "'>");
                        s.Append("<td  align='center' style='width:40px;background-color:#b5ebf8;' class='heading'>");
                        s.Append("<input type='button' class='add2' id='a" + r.mID + "'onclick=\"AddDanhmuc('" + r.mID + "','" + r.Name + "')\">");
                        s.Append("<input type='button' class='imgdel' id='d" + r.mID + "'  onclick=\"DeleteM1('" + r.mID + "'," + r.STT + ")\">");
                        s.Append("</td>");

                        s.Append("<td  style='width:40px;background-color:#b5ebf8' class='heading'  align='center' id='td1_" + r.mID + "'>" + r.mID + "</td>");

                        s.Append("<td class='heading' style='width:200px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' colspan='2' id='tname1_" + r.mID + "'><input class='txtn' type='text' id='txtMenu1" + r.mID + "' value='" + r.Name + "' onchange='Update_Name_Menu1(" + r.mID + ")' style='width:100%;'></td>");

                        //s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;'><img onclick=\"ChangeIcon(" + r.mID + " ,'" + r.Icon + "')\" style='width:40px; height:40px; ' src='" + r.Icon + "' /></td>");
                        //s.Append("<td class='heading' style='width:100px;background-color:#b5ebf8;font-size:17px;font-weight:bold;'>" + r.Folder + "</td>");

                        s.Append("<td style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT + " type='checkbox' onchange=\"UpdateHienThi1('" + r.mID + "')\"  id='chkCate_" + r.mID + "'> <span class='text' style='z-index:0'></span></td>");
                        s.Append("<td class='heading' style='width:50px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' id='n" + r.mID + "'><input class='txtn' type='text' id='txtSTT_Type" + r.mID + "' value='" + r.STT + "' onchange='UpdateSTT_Menu1(" + r.mID + ")' style='width:70%;'></td>");

                        s.Append("</tr>");
                        var dt2 = from F1 in db.wMenu2
                                  where F1.mID == r.mID
                                  orderby F1.STT ascending
                                  select new { F1.sID, F1.mID, F1.Name, F1.Effect, F1.STT };
                        if (dt2.Count() > 0)
                        {
                            foreach (var r2 in dt2)
                            {
                                string HT2 = r2.Effect == true ? " checked=checked" : "";
                                s.Append("<tr id='submenu" + r2.sID + "'>");
                                s.Append("<td  align='center'  style='width:40px;background-color:#b5ebf8'>&nbsp;</td>");
                                s.Append("<td  align='center' style='width:40px;' class='heading'>");
                                s.Append("<input type='button' class='imgdel' id='sub_del" + r2.sID + "'  onclick=\"DeleteM2('" + r2.sID + "',2)\">");
                                s.Append("</td>");

                                s.Append("<td  style='margin-left:40px;width:40px;'  class='heading'  align='center' id='m" + r2.sID + "'>" + r2.sID + "</td>");
                                s.Append("<td class='heading'  style='width:160px; text-align:center'  id='n" + r2.sID + "'><input class='txtn' type='text' id='txtNameCate" + r2.sID + "' value='" + r2.Name + "' onchange='UpdateNameCate(" + r2.sID + ")' style='width:100%'></td>");
                                //s.Append("<td class='heading'></td>");
                                //s.Append("<td class='heading'></td>");
                                s.Append("<td style='width:50px; text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT2 + " type='checkbox' onchange=\"UpdateHienThi2('" + r2.sID + "')\"  id='chkCate2_" + r2.sID + "'> <span class='text' style='z-index:0'></span></td>");

                                s.Append("<td class='heading'  style='width:50px; text-align:center'  id='n" + r2.sID + "'><input class='txtn' type='text' id='txtSTT" + r2.sID + "' value='" + r2.STT + "' onchange='UpdateSTT(" + r2.sID + ")' style='width:70%;'></td>");
                                s.Append("</tr>");
                            }
                        }
                    }
                }
                s.Append("</tbody>");
                string data = "" + s;
                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult INSERT_MENU1(string sNAME)
        {
            wMenu1 m1 = new wMenu1();
            int mID = db.wMenu1.Select(x => x.mID).DefaultIfEmpty(0).Max() + 1;
            int? stt = db.wMenu1.Select(x => x.STT).DefaultIfEmpty(0).Max() + 1;
            m1.mID = mID;
            m1.Name = sNAME;
            m1.Effect = true;
            m1.STT = stt;
            db.wMenu1.Add(m1);
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult INSERT_MENU2(string MaDMC, string TenDMCon)
        {
            wMenu2 m2 = new wMenu2();
            int sID = db.wMenu2.Select(x => x.sID).DefaultIfEmpty(0).Max() + 1;
            int mID = int.Parse(MaDMC);
            int? stt = db.wMenu2.Select(x => x.STT).DefaultIfEmpty(0).Max() + 1;
            m2.sID = sID;
            m2.mID = mID;
            m2.Name = TenDMCon;
            m2.Effect = true;
            m2.STT = stt;
            db.wMenu2.Add(m2);
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteM1(string mID)
        {
            int smID = int.Parse(mID);
            var w2 = db.wMenu2.Where(r => r.mID == smID).ToList();
            if (w2.Count() > 0)
            {
                foreach(var item in w2)
                {
                    int sID = item.sID;
                    if(db.wNewsMenuIds.Where(r => r.sID == sID).Count()>0)
                        db.wNewsMenuIds.RemoveRange(db.wNewsMenuIds.Where(r => r.sID == sID));
                }
                db.wMenu2.RemoveRange(db.wMenu2.Where(r => r.mID == smID));              

            }
            var w22 = db.wMenu1.Where(r => r.mID == smID).ToList();
            if (w22.Count() > 0)
            {
                db.wMenu1.RemoveRange(db.wMenu1.Where(r => r.mID == smID));
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteM2(string sID)
        {
            int sId = int.Parse(sID);
            var w2 = db.wNewsMenuIds.Where(r => r.sID == sId).ToList();
            if (w2.Count() > 0)
            {

                db.wNewsMenuIds.RemoveRange(db.wNewsMenuIds.Where(r => r.sID == sId));
                var w22 = db.wMenu2.Where(r => r.sID == sId).ToList();
                if (w22.Count() > 0)
                {
                    db.wMenu2.RemoveRange(db.wMenu2.Where(r => r.sID == sId));
                }
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update_Name_Menu1(string mID, string sNAME)
        {
            int smID = int.Parse(mID);
            wMenu1 mn = db.wMenu1.SingleOrDefault(r => r.mID == smID);
            if (mn != null)
            {
                mn.Name = sNAME;
            }           
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateNameCate(string sID, string sNAME)
        {
            int ID = int.Parse(sID);
            wMenu2 mn = db.wMenu2.SingleOrDefault(r => r.sID == ID);
            if (mn != null)
            {
                mn.Name = sNAME;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateSTT_Menu1(string mID, string sSTT)
        {
            int smID = int.Parse(mID);
            int stt = int.Parse(sSTT);
            wMenu1 mn = db.wMenu1.SingleOrDefault(r => r.mID == smID);
            if (mn != null)
            {
                mn.STT = stt;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateSTT(string sID, string sSTT)
        {
            int ID = int.Parse(sID);
            int stt = int.Parse(sSTT);
            wMenu2 mn = db.wMenu2.SingleOrDefault(r => r.mID == ID);
            if (mn != null)
            {
                mn.STT = stt;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateHienThi1(string mID, string sHIEULUC)
        {
            int smID = int.Parse(mID);
            wMenu1 mn = db.wMenu1.SingleOrDefault(r => r.mID == smID);
            if (mn != null)
            {
                mn.Effect = sHIEULUC == "true" ? true : false;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateHienThi2(string sID, string sHIEULUC)
        {
            int ID = int.Parse(sID);
            wMenu2 mn = db.wMenu2.SingleOrDefault(r => r.sID == ID);
            if (mn != null)
            {
                mn.Effect = sHIEULUC == "true" ? true : false;
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        public ActionResult PartialForm()
        {
            return View();
        }
    }
}