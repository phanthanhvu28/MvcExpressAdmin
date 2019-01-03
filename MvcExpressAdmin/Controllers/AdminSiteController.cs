
using MvcExpressAdmin.Models;
using MvcExpressAdmin.MyClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcExpressAdmin.Controllers
{
    public class AdminSiteController : Controller
    {
        // GET: AdminSite

        NewsDataContext db = new NewsDataContext();
        string lid = CookieCls.GetLanguge();
        string der = CookieCls.GetDerpartment();
        int manv = int.Parse(CookieCls.GetMaNV());
        public string Menu()
        {
            try
            {
                if (CookieCls.GetMaNV() != "")
                {                
                    StringBuilder s = new StringBuilder();
                    s.Append("<ul class='sidebar-menu scrollbarmenu' style='height:" + (sHeight() - 140) + "px'>");
                    var dmc = (from a in db.DanhMucChas
                               from b in db.DanhMucChaCTs
                               from c in db.PhanQuyens
                               from d in db.DanhMucs
                               where a.MaDMC == b.MaDMC && b.LID.Equals(lid) && a.HienThi == true && c.MaDM == d.MaDM && d.MaDMC == a.MaDMC && c.MaNV == manv
                               orderby a.ThuTu ascending
                               select new { a.MaDMC, b.TenDMC, a.CssClass }).Distinct();
                    int cout1 = dmc.Count();
                    if (dmc.Count() > 0)
                    {

                        foreach (var item in dmc)
                        {
                            s.Append("<li class='treeview active'><a ><i class='" + item.CssClass + "'></i><span>" + item.TenDMC + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-rightd'></i></span></a>");
                            s.Append("<ul class='treeview-menu' id='menu'>");

                            var dm = (from f1 in db.DanhMucs
                                      from f2 in db.DanhMucCTs
                                      from f3 in db.PhanQuyens
                                      where f1.MaDM == f2.MaDM && f2.LID.Equals(lid) && f1.HienThi == true && f1.MaDMC == item.MaDMC && f1.MaDM.Equals(f1.MaDM) && f3.MaNV==manv
                                      orderby f1.ThuTu ascending
                                      select new { f1.MaDM, f1.Site, f2.TenDM }
                                      ).Distinct();
                            int cout2 = dm.Count();
                            if (dm.Count() > 0)
                            {
                                foreach (var row in dm)
                                {
                                    s.Append("<li><a href='/" + row.Site + "' title='" + row.TenDM + "'><i class='fa fa-circle-o'></i>" + row.TenDM + "</a></li>");
                                }
                            }
                            s.Append("</ul>");
                        }
                        s.Append("</li></ul>");
                    }
                    return "" + s;
                }
            }
            catch { }
            return "";
        }        
        public string Derpartment()
        {         
            var derName = db.NhomNhanVienCTs.Where(a => a.NhomNVID.Equals(der) && a.LID.Equals(lid)).SingleOrDefault().TenNhom;          
            return derName;
        }
        public string ListDerp()
        {          
             StringBuilder s = new StringBuilder();        
            int manv =int.Parse(CookieCls.GetMaNV());
            var derp_nv = (from a in db.NhanViens
                          from b in db.NhomNhanVienCTs
                          where a.NhomNVID == b.NhomNVID && a.MaNV == manv && b.LID.Equals(lid)
                          select new { a.NhomNVID,b.TenNhom }).SingleOrDefault();
            s.Append("<table style=width:100%><tbody>");
            s.Append("<tr style=height:30px;cursor:pointer onclick=ChonFac(\"" + derp_nv.NhomNVID+ "\")>");           
            s.Append("<td style=color:#A9D0F5;font-weight:bold;font-size:14px>" + derp_nv.TenNhom + "</td>");
            s.Append("</tr>");

            var derp_list = (from a in db.PhanBoPBs
                             from b in db.NhomNhanVienCTs
                             where a.NhomNVID == b.NhomNVID && a.MaNV == manv && b.LID.Equals(lid)
                             select new {b.NhomNVID,b.TenNhom });
            if(derp_list.Count()>0)
            {
                foreach(var v in derp_list)
                {
                    s.Append("<tr style=height:30px;cursor:pointer onclick=ChonFac(\"" + v.NhomNVID + "\")>");
                    s.Append("<td style=color:#A9D0F5;font-weight:bold;font-size:14px>" + v.TenNhom + "</td>");
                    s.Append("</tr>");
                }
            }
            s.Append("</tbody></table>");
            return "" + s;
        }
        public string ListLanguge()
        {
            StringBuilder s = new StringBuilder();
            var nn = from a in db.NgonNgus
                     orderby a.ThuTu
                     select a;
            if(nn.Count()>0)
            {
                s.Append("<table style=width:100%>");
                s.Append("<tbody>");
                foreach(var v in nn)
                {
                    s.Append("<tr style=height:35px;cursor:pointer onclick=ChonLag(\"" + v.LID + "\")>");
                    s.Append("<td style=color:#A9D0F5;font-weight:bold;font-size:14px><img src=../" + v.Icon + "></td>");
                    s.Append("<td style=color:#A9D0F5;font-weight:bold;font-size:14px;>" + v.LName + "</td>");
                    s.Append("</tr>");
                }
                s.Append("</tbody>");
                s.Append("</table>");
            }
            return "" + s;
        }
        public string GetFullName()
        {
            if (CookieCls.GetMaNV() != "")
                return CookieCls.GetFullname();
            return "";
        }
        public string GetTT(string TID)
        {
            var r = (from a in db.GhiChuCTs
                     where a.TID.Equals(TID) && a.LID.Equals(lid)
                     select new { a.TName }).SingleOrDefault();
            //var r = db.GhiChuCTs.SqlQuery("select TName from GhiChuCT where TID=@TID and LID=@LID", new SqlParameter("@TID", TID), new SqlParameter("@LID", lid)).FirstOrDefault();
            return (r != null ? "" + r.TName : "");
        }
        public string LoadImageLang()
        {
            var v = from a in db.NgonNgus
                    where a.LID.Equals(lid)
                    orderby a.ThuTu
                    select new {a.Icon };
            return "" + v.SingleOrDefault().Icon;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public int sHeight()
        {
            if (System.Web.HttpContext.Current.Session["BrowserWidth"] != null)
            {
                return int.Parse(System.Web.HttpContext.Current.Session["BrowserHeight"].ToString());
            }
            return 700;
        }
        public bool Per(string MaCN)
        {
            string action = System.Web.HttpContext.Current.Request.Url.Segments[System.Web.HttpContext.Current.Request.Url.Segments.Length - 1].ToString();
            var v = from a in db.NhanVienChucNangs
                    from b in db.DanhMucs
                    where a.MaNV == manv && a.MaCN.Equals(MaCN) && a.MaDM == b.MaDM && b.Site.Equals(action)
                    select a;

            return v.Count() > 0 ? true : false;
        }

    }
}