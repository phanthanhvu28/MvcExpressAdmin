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
    public class PQTrangBaoController : PerSiteController
    {
        // GET: PQTrangBao
        private NewsDataContext db = new NewsDataContext();
        AdminSiteController AS = new AdminSiteController();
        public string LoadNhanVien()
        {
            StringBuilder s = new StringBuilder();
            string cookienn = CookieCls.GetLanguge();
            try
            {
                s.Append("<table class='mytable' style='width:100%; z-index:15'>");
                s.Append("<thead>");
                s.Append("<tr style='height:50px'>");
                s.Append("<td rowspan='2' style='width:40px; text-align:center'>" + AS.GetTT("tt_stt") + "</td>");
                s.Append("<td rowspan='2' style='width:40px; text-align:center'></td>");

                s.Append("<td style='width:100px'>" + AS.GetTT("tt_bp") + "</td>");
                s.Append("<td style='width:100px'>Username</td>");
                s.Append("<td style='width:120px'>" + AS.GetTT("tt_ht") + "</td>");
                s.Append("<td style='width:80px; text-align:center'>Get Rss</td>");
                s.Append("</tr>");
                s.Append("</thead>");
                s.Append("</table>");
                var nv = (from a in db.NhanViens
                          from b in db.NhomNhanViens
                          from c in db.NhomNhanVienCTs
                          where a.NhomNVID == b.NhomNVID && b.NhomNVID == c.NhomNVID && c.LID.Equals(cookienn)
                          select new { a.MaNV, a.SoThe, a.HoTen, c.TenNhom }
                        );
                if (nv.Count() > 0)
                {
                    s.Append("<table style='width:100%;' class='mytable' id='dsnv'>");
                    int stt = 0;
                    foreach (var item in nv)
                    {

                        stt++;
                        s.Append("<tr id='tr" + item.MaNV + "'>");
                        s.Append("<td style='width:40px' align='center'>" + stt + "</td>");

                        s.Append("<td style='width:40px' align='center'><label class='lbcheck' style='z-index:1000'><input class='chkhidden' value='" + item.MaNV + "' type='checkbox' onclick=\"SelectUser('chknv" + stt + "','" + item.MaNV + "')\" id='chknv" + stt + "'> <span class='text'></span></td>");
                        s.Append("<td  style='width:100px'  id='m" + item.MaNV + "'>" + item.TenNhom + "</td>");
                        s.Append("<td  style='width:100px'  id='n" + item.MaNV + "'>" + item.SoThe + "</td>");
                        s.Append("<td  style='width:120px'  id='n" + item.MaNV + "'>" + item.HoTen + "</td>");
                        s.Append("<td style='width:80px' align='center'>  <img src='../Content/Layout/Images/permission.png' class='csspermission' onclick=\"Xem_Quyen_By_MaNV('" + stt + "','" + item.MaNV + "','" + item.HoTen + "')\"/></td>");

                        s.Append("</tr>");
                    }
                    s.Append("</table>");
                }

                return "" + s;
            }
            catch
            {
                return "";
            }
        }
        public string LoadTrangBao()
        {
            StringBuilder s = new StringBuilder();
            string cookienn = CookieCls.GetLanguge();
            try
            {
                s.Append("<table width='100%' class='mytable'>");
                s.Append("<thead>");
                s.Append("<tr style='height:50px'>");
                s.Append("<td style='width:60px;text-align:center'>" + AS.GetTT("tt_id") + "</td>");
                s.Append("<td style='width:160px'>" + AS.GetTT("tt_tt") + "</td>");
                s.Append("<td style='width:80px;text-align:center'>SL menu</td>");
                s.Append("<td style=''>Get Rss <label id='lbHoTen' class='lbcss'></td>");
                s.Append("</tr>");
                s.Append("</thead></table>");
                s.Append("<div class='scrollbar1' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 240) + "px;height:" + (AS.sHeight() - 245) + "px; border-bottom: 1px solid #CCCCCC;border-left: 1px solid #CCCCCC; clear:bold;' alig='left'>");
                s.Append("<table width='100%' class='mytable' id='dsquyen'>");
                var regions = from a in db.Regions
                              where a.HienThi == true
                              orderby a.ThuTu ascending
                              select new { a.MaNuoc, a.TenNuoc };
                if (regions.Count() > 0)
                {
                    int i = 0;
                    foreach (var region in regions)
                    {
                        i++;
                        s.Append("<tr id='tr" + region.MaNuoc + "'>");
                        s.Append("<td class='heading'  style='background-color:#b5ebf8;font-size:17px;font-weight:bold;' colspan='5' id='n" + region.MaNuoc + "'>" + i + ". " + region.TenNuoc + "(" + region.MaNuoc + ")</td>");
                        s.Append("</tr>");

                        var newspapers = from a in db.mNewspapers
                                         where a.Languge.Equals(region.MaNuoc) && a.Display == true
                                         select new { a.NewspaperId, a.Title };
                        if (newspapers.Count() > 0)
                        {
                            foreach (var newspaper in newspapers)
                            {
                                s.Append("<tr style='height:40px' id='tr" + newspaper.NewspaperId + "'>");

                                s.Append("<td  style='width:60px;'  class='heading'  align='center' id='m" + newspaper.NewspaperId + "'>" + newspaper.NewspaperId + "</td>");
                                s.Append("<td class='heading' style='width:160px;' id='n" + newspaper.NewspaperId + "'>" + newspaper.Title + "</td>");
                                s.Append("<td style='width:80px;text-align:center'>" + (from a in db.mNewspaperMenus where a.NewspaperId == newspaper.NewspaperId && a.Display == true select a.NewspaperMenuId).Count() + "</td>");
                                s.Append("<td>");
                                s.Append("<label style='margin: 3px;'  id='lb" + newspaper.NewspaperId + "'><input type='checkbox' class='checkbox-slider slider-icon colored-palegreen' id='chk" + newspaper.NewspaperId + "' value='" + newspaper.NewspaperId + "' onclick=\"SetFunc(0," + newspaper.NewspaperId + ")\"> <span class='text'></span>");
                                s.Append("</td>");
                                s.Append("</tr>");
                            }
                        }

                    }
                }
                s.Append("</table></div>");
                return "" + s;
            }
            catch
            {
                return "";
            }
        }
        [HttpPost]
        public ActionResult Xem_Quyen_By_MaNV(string sMaNV)
        {
            int manv = int.Parse(sMaNV);
            try
            {
                var newspapers = from a in db.PhanQuyenTBs
                                 where a.MaNV == manv
                                 select new { a.MaNV, a.NewspaperId };
                string data = "";
                if (newspapers.Count() > 0)
                {
                    foreach (var newspaper in newspapers)
                    {
                        data += "#$%chk" + newspaper.NewspaperId;
                    }
                }

                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SetFunc(string sNewspaperId, string sMaNV, string sCHK)
        {
            int newspaperid = int.Parse(sNewspaperId);
            int y = 1;
            sMaNV = sMaNV.ToLower();
            sMaNV = sMaNV.Replace(",undefined", "");
            sMaNV = sMaNV.Replace(",on", "");
            sMaNV = sMaNV.Replace(",false", "");
            sMaNV = sMaNV.Replace(",true", "");

            string[] nd = sMaNV.Split(',');
            int len = nd.Length;
            if (len > 0)
            {
                for (y = 1; y < len; y++)
                {
                    int manv = int.Parse(nd[y].ToString());
                    var newspapers = from a in db.PhanQuyenTBs
                                     where a.MaNV == manv && a.NewspaperId == newspaperid
                                     select a.NewspaperId;
                    if (newspapers.Count() > 0 && sCHK == "false")
                    {
                        db.PhanQuyenTBs.RemoveRange(db.PhanQuyenTBs.Where(r => r.MaNV == manv && r.NewspaperId == newspaperid));
                    }
                    else
                    {
                        PhanQuyenTB tb = new PhanQuyenTB();
                        tb.MaNV = manv;
                        tb.NewspaperId = newspaperid;
                        db.PhanQuyenTBs.Add(tb);

                    }
                    db.SaveChanges();
                }
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }
    }
}