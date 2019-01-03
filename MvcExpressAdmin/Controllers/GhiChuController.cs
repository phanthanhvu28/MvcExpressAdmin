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
    public class GhiChuController : PerSiteController
    {
        // GET: GhiChu
        NewsDataContext db = new NewsDataContext();
        AdminSiteController AS = new AdminSiteController();
        public string LoadData()
        {
            StringBuilder s = new StringBuilder();
            string cookienn = CookieCls.GetLanguge();
            try
            {
                var nn = from a in db.NgonNgus
                         where a.LShow == true
                         orderby a.ThuTu ascending
                         select new { a.LID, a.LName, a.Icon, a.Class };
                if (nn.Count() > 0)
                {
                    s.Append("<div style='width:100%;z-index:20;margin-bottom:1px;'>");
                    s.Append("<table  class='mytable' style='width:100%'>");
                    s.Append("<thead>");
                    s.Append("<tr style='Height:40px'>");
                    s.Append("<td  colspan='3' class='background_nn' style='background-color:rgba(0, 0, 0, .08)'>");
                    foreach (var item in nn)
                    {
                        s.Append("<label style='padding-right: 10px;'>");
                        s.Append(" <input name=\"form-field-radio\"  class='chkhidden' value='" + item.LID + "' type=\"radio\" id=\"chk" + item.LID + "\" onclick=\"ChangeLang('" + item.LID + "')\"" + (cookienn == item.LID ? " checked=checked" : "") + ">");
                        s.Append(" <span class=\"text\">" + item.LName + "</span> ");
                        s.Append("</label>");
                    }
                    s.Append("</td>");
                    s.Append("</tr>");
                    s.Append("<tr>");
                    s.Append("<td style='text-align:center; height:40px;width:5%'>#</td>");
                    s.Append("<td style='width:15%' class='heading'>Tiêu đề</td>");
                    s.Append("<td style='width:80%' class='heading'>Nội dung</td>");
                    s.Append("</tr>");
                    s.Append("<tr id='add'>");
                    s.Append("<td style='width:30px;font-weight:bold;color:#69B20E;font-size:25px;' align='center'>");
                    s.Append("<input type='button' class='add2' style='cursor:pointer;border:none' id='addGC' onclick=\"AddGhiChu()\">");
                    s.Append("</td>");
                    s.Append("<td style='width:15%'><input class='txtn' type='text' id='matd' onkeyup='search(0)' style='width:100%;'>");
                    s.Append("</td>");

                    s.Append("<td><span id='iadd'></span><input class='txtn' style='width:100%;' type='text' id='tentd'>");
                    s.Append("</td>");

                    s.Append("</tr>");

                    var gc = from a in db.GhiChus                                
                             select new { a.TID };
                    if (gc.Count() > 0)
                    {
                        s.Append("<tr><td colspan='4'>");
                        s.Append("<div class='scrollbar' style='overflow-y:scroll;overflow-x:hidden;max-height:" + (AS.sHeight() - 280) + "px' alig='left'>");

                        s.Append("<table class='mytable' style='width:100%' id='dsdata'>");

                        foreach (var item2 in gc)
                        {
                            s.Append("<tr id='tt-" + item2.TID + "' style='Height:40px'>");

                            s.Append("<td  style='text-align:center; width:5%'>");

                            s.Append("<input type='button' id='del" + item2.TID + "' class='imgdel'  onclick=\"DeleteTT('" + item2.TID + "')\">");
                            s.Append("</td>");
                            s.Append("<td style='width:15%'>" + item2.TID + "</td>");
                            s.Append("<td style='width:80%'>");
                            string tid = item2.TID;
                            var gcCT = (from a in db.GhiChuCTs
                                        where a.LID.Equals(cookienn) && a.TID.Equals(item2.TID)
                                        select new { a.LID, a.TName }).FirstOrDefault();
                            if (gcCT != null)
                                s.Append("<input type='text' id='txt" + item2.TID + cookienn + "' class='txtn'  onchange=\"ChangeND('" + item2.TID + "','" + cookienn + "')\"  style='width:100%' value='" + gcCT.TName + "'>");
                            else
                                s.Append("<input type='text' id='txt" + item2.TID + cookienn + "' class='txtn'  onchange=\"ChangeND('" + item2.TID + "','" + cookienn + "')\"  style='width:100%' value=''>");
                            s.Append("</td>");
                            s.Append("</tr>");
                        }
                        s.Append("</div></table></td></tr>");
                    }
                    s.Append("</thead>");
                    s.Append("</table></div>");
                }

                return "" + s;
            }
            catch
            {
                return "Lỗi hệ thống "+ cookienn + " kaka";
            }
        }

        [HttpPost]
        public ActionResult SearchGhiChu(string SLID, string SKEY)
        {
            StringBuilder s = new StringBuilder();          
            try
            {
                var gc = from a in db.GhiChus
                         where a.TID.Contains(SKEY)
                         select new { a.TID };
                if (gc.Count() > 0)
                {
                    foreach (var item2 in gc)
                    {
                        s.Append("<tr id='tt-" + item2.TID + "' style='Height:40px'>");

                        s.Append("<td  style='text-align:center; width:5%'>");

                        s.Append("<input type='button' id='del" + item2.TID + "' class='imgdel'  onclick=\"DeleteTT('" + item2.TID + "')\">");
                        s.Append("</td>");
                        s.Append("<td style='width:15%'>" + item2.TID + "</td>");
                        s.Append("<td style='width:80%'>");
                        string tid = item2.TID;
                        var gcCT = (from a in db.GhiChuCTs
                                    where a.LID.Equals(SLID) && a.TID.Equals(item2.TID)
                                    select new { a.LID, a.TName }).FirstOrDefault();
                        if (gcCT != null)
                            s.Append("<input type='text' id='txt" + item2.TID + SLID + "' class='txtn'  onchange=\"ChangeND('" + item2.TID + "','" + SLID + "')\"  style='width:100%' value='" + gcCT.TName + "'>");
                        else
                            s.Append("<input type='text' id='txt" + item2.TID + SLID + "' class='txtn'  onchange=\"ChangeND('" + item2.TID + "','" + SLID + "')\"  style='width:100%' value=''>");
                        s.Append("</td>");
                        s.Append("</tr>");
                    }

                }
                string data = "" + s;
                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateGhiChu(string STID, string SLID, string STNAME)
        {            
            GhiChuCT cn = db.GhiChuCTs.SingleOrDefault(r => r.TID.Equals(STID) && r.LID.Equals(SLID));
            if (cn != null)
            {
                cn.TName = STNAME;
            }
            else
            {
                cn = new GhiChuCT();
                cn.TID = STID;
                cn.LID = SLID;
                cn.TName = STNAME;               
                db.GhiChuCTs.Add(cn);
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertGhiChu(string SLID, string STID, string STNAME)
        {
            GhiChu gc = db.GhiChus.SingleOrDefault(a => a.TID.Equals(STID));
            if(gc ==null)
            {           
                gc = new GhiChu();
                gc.TID = STID;
                gc.HienThi = true;
                db.GhiChus.Add(gc);   

                GhiChuCT ct = new GhiChuCT();
                ct.TID = STID;
                ct.LID = SLID;
                ct.TName = STNAME;
                db.GhiChuCTs.Add(ct);

                db.SaveChanges();
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
           
        }

        [HttpPost]
        public ActionResult DeleteGhiChu(string STID)
        {
            var cn = db.GhiChuCTs.Where(r => r.TID.Equals(STID)).ToList();
            if (cn.Count() > 0)
            {
                db.GhiChuCTs.RemoveRange(db.GhiChuCTs.Where(r => r.TID.Equals(STID)));
            }
            db.GhiChus.RemoveRange(db.GhiChus.Where(r => r.TID.Equals(STID)));
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
    }
}