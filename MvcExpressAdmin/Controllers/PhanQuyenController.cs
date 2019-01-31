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
    public class PhanQuyenController : PerSiteController
    {
        private NewsDataContext db = new NewsDataContext();
        AdminSiteController AS = new AdminSiteController();
        string languge = CookieCls.GetLanguge();
        // GET: PhanQuyen
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

                s.Append("<td  style='width:80px'>" + AS.GetTT("tt_bp") + "</td>");
                s.Append("<td  style='width:100px'>Username</td>");
                s.Append("<td  style='width:120px'>" + AS.GetTT("tt_ht") + "</td>");
                s.Append("<td rowspan='2' style='width:80px; text-align:center'>Quyền</td>");
                s.Append("</tr>");
                s.Append("<tr>");
                s.Append("<td></td>");
                s.Append("<td style='width:100px'><input type='text' class='cal' style='width:100%;height:30px' id='txtsothe' onkeyup='ShowDataByPage(1);'></td>");
                s.Append("<td style='width:120px'><input type='text' class='cal' style='width:100%;height:30px' id='txthoten' onkeyup='ShowDataByPage(1)'></td>");
                //s.Append("<td>");
                //s.Append("</td>");

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
                        s.Append("<td  style='width:80px'  id='m" + item.MaNV + "'>" + item.TenNhom + "</td>");
                        s.Append("<td  style='width:100px'  id='n" + item.MaNV + "'>" + item.SoThe + "</td>");
                        s.Append("<td  style='width:120px'  id='n" + item.MaNV + "'>" + item.HoTen + "</td>");
                        s.Append("<td style='width:80px' align='center'>  <img src='../Content/Layout/Images/permission.png' class='csspermission' onclick=\"Xem_Quyen_By_MaNV('" + stt + "','" + item.MaNV + "')\"/></td>");

                        s.Append("</tr>");
                    }
                    s.Append("</table>");



                    //s.Append("<div style='float:left;margin-top:2px;' id='pagenv'></div>");

                    //s.Append("<div style='float:right;margin-top:2px;'> <label class='icon120n'>" + AS.GetTT("tt_display") + " &nbsp;&nbsp;</label><div class='custom-select'>");
                    //s.Append("<select id='slrecored' style='width:70px;' onchange=\"ChangeRecord()\">");
                    //s.Append("<option value='20'>20</option>");
                    //s.Append("<option value='30'>30</option>");
                    //s.Append("<option value='50' selected>50</option>");

                    //s.Append("</select>");
                    //s.Append("</div></div>");


                    //s.Append("<script>\n");
                    //s.Append(" $(function () {\n");

                    //s.Append(" $('#pagenv').bootpag({\n");
                    //s.Append("   total: " + 1 + ",\n");
                    //s.Append("   page: 1,\n");
                    //s.Append(" firstLastUse:1,\n");
                    //s.Append("   maxVisible: 9\n");

                    //s.Append("  }).on(\"page\", function (event, num) {\n");
                    //s.Append("   \n");
                    //s.Append("   ShowDataByPage(num);\n");
                    //s.Append(" });\n");
                    //s.Append(" });\n");
                    //s.Append(" </script>  \n");
                }

                return "" + s;
            }
            catch
            {
                return "";
            }
        }

        public string LoadChucNang()
        {
            StringBuilder s = new StringBuilder();
            try
            {
                s.Append("<table width='100%' class='mytable'>");
                s.Append("<thead>");
                s.Append("<tr style='height:60px'>");
                s.Append("<td style='width:10%;text-align:center'>" + AS.GetTT("tt_id") + "</td>");
                s.Append("<td style='width:20%'>" + AS.GetTT("tt_tt") + "</td>");
                s.Append("<td style='width:70%'>" + AS.GetTT("tt_cn") + "<label id='lbHoTen' class='lbcss' /></td>");
                s.Append("</tr>");
                s.Append("</thead></table>");
                s.Append("<div class='scrollbar' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 210) + "px'>");
                s.Append("<table width='100%' class='mytable' id='dscn' style='' >");
                s.Append("<tbody id='dscn'>");
                var dmc = from a in db.DanhMucChaCTs
                          from b in db.DanhMucChas
                          where a.MaDMC == b.MaDMC && a.LID.Equals(languge)
                          orderby b.ThuTu
                          select new { b.MaDMC, a.TenDMC, b.HienThi };
                if (dmc.Count() > 0)
                {
                    foreach (var r in dmc)
                    {
                        s.Append("<tr id='tr" + r.MaDMC + "'>");
                        s.Append("<td  style='width:40px;background-color:#b5ebf8;font-weight:bold' class='heading'  align='center' id='m" + r.MaDMC + "'>" + r.MaDMC + "</td>");
                        s.Append("<td class='heading'  style='background-color:#b5ebf8;font-size:17px;font-weight:bold' colspan='4' id='n" + r.MaDMC + "'>" + r.TenDMC + "</td>");
                        s.Append("</tr>");
                        var dmcon = from a in db.DanhMucs
                                    from b in db.DanhMucCTs
                                    where a.MaDM == b.MaDM && b.LID.Equals(languge) && a.MaDMC == r.MaDMC
                                    orderby a.ThuTu
                                    select new { a.MaDM, a.MaDMC, a.Site, b.TenDM, a.ThuTu, a.HienThi };
                        if (dmcon.Count() > 0)
                        {
                            foreach (var r2 in dmcon)
                            {
                                s.Append("<tr id='tr" + r2.MaDM + "'>");
                                s.Append("<td  align='center'  style='width:5%;background-color:#e4e8f6'>&nbsp;</td>");
                                s.Append("<td  style='width:5%;' class='heading'  align='center' id='m" + r2.MaDM + "'>" + r2.MaDM + "</td>");
                                s.Append("<td class='heading'  style='width:20%;'  id='n" + r2.MaDM + "'>" + r2.TenDM + "</td>");
                                s.Append("<td style='width:70%;' id='cn" + r2.MaDM + "'>");
                                s.Append(GetCN(r2.MaDM, 0));
                                s.Append("</td></tr>");
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
        public StringBuilder GetCN(int MADM, int MANV)
        {
            StringBuilder s = new StringBuilder();
            var cn = from a in db.DanhMucs
                     from b in db.ChucNangs
                     where a.MaDM == b.MaDM && b.MaDM.Equals(MADM) && b.LID.Equals(languge)
                     select new { a.MaDM, b.TenCN, b.MaCN };
            if (cn.Count() > 0)
            {
                foreach (var r in cn)
                {
                    string macn = r.MaCN;
                    var nvcn = from a in db.DanhMucs
                               from b in db.NhanVienChucNangs
                               where a.MaDM == b.MaDM && b.MaNV == MANV && b.MaCN.Equals(macn)
                               select new { b.MaDM, b.MaCN };
                    s.Append("<div style='margin-left:3px;margin-right: 5px;float:left;width:auto;vertical-align:middle;' class='boxper' align='left' id='cn" + r.MaDM + "'>");
                    if (nvcn.Count() > 0)
                    {
                        s.Append("<label style='margin:3px'><input type='checkbox' checked='checkbox' class='checkbox-slider slider-icon colored-palegreen' id='chk" + r.MaDM + "-" + r.MaCN + "' value='" + r.MaCN + "' onclick=\"SetFunc('" + r.MaDM + "','" + r.MaCN + "')\"> <span class='text' id='lb" + r.MaDM + r.MaCN + "'></span>");
                        s.Append(" <span style='float:left; margin-right:5px;margin-top:3px;' title='" + r.MaCN + "'>" + r.TenCN + "</span></label></div>");
                    }
                    else
                    {
                        s.Append("<label style='margin:3px'><input type='checkbox' class='checkbox-slider slider-icon colored-palegreen' id='chk" + r.MaDM + "-" + r.MaCN + "' value='" + r.MaCN + "' onclick=\"SetFunc('" + r.MaDM + "','" + r.MaCN + "')\"> <span class='text' id='lb" + r.MaDM + r.MaCN + "'></span>");
                        s.Append(" <span style='float:left; margin-right:5px;margin-top:3px;' title='" + r.MaCN + "'>" + r.TenCN + "</span></label></div>");
                    }
                }
            }
            return s;
        }
        [HttpPost]
        public ActionResult ShowPerm(int sMaNV)
        {
            string data = "";
            try
            {
                var hoten = from a in db.NhanViens
                            where a.MaNV == sMaNV
                            select new { a.HoTen };
                data += hoten.FirstOrDefault().HoTen;

                var chucnang = from a in db.NhanVienChucNangs
                               where a.MaNV == sMaNV
                               select a;
                if (chucnang.Count() > 0)
                {
                    foreach (var cn in chucnang)
                    {
                        data += "#$%chk" + cn.MaDM + "-" + cn.MaCN;
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
        public ActionResult SetFunc(int sMaDM, string sMaCN, string sMaNV, string sCHK)
        {
            sMaNV = sMaNV.ToLower();
            sMaNV = sMaNV.Replace(",undefined", "");
            sMaNV = sMaNV.Replace(",on", "");
            sMaNV = sMaNV.Replace(",false", "");
            sMaNV = sMaNV.Replace(",true", "");

            string[] nd = sMaNV.Split(',');
            int len = nd.Length;
            if (len > 0)
            {
                for (int y = 1; y < len; y++)
                {
                    int manv = int.Parse(nd[y].ToString());
                    var check_nvcn = from a in db.NhanVienChucNangs
                                     where a.MaNV == manv && a.MaDM == sMaDM && a.MaCN.Equals(sMaCN)
                                     select a;
                    var check_nvcn2 = from a in db.NhanVienChucNangs
                                      where a.MaNV == manv && a.MaDM == sMaDM
                                      select a;

                    if (check_nvcn.Count() > 0)
                        db.NhanVienChucNangs.RemoveRange(db.NhanVienChucNangs.Where(d => d.MaNV == manv && d.MaDM == sMaDM && d.MaCN.Equals(sMaCN)));
                    if (check_nvcn2.Count() > 0)
                        db.PhanQuyens.RemoveRange(db.PhanQuyens.Where(d => d.MaNV == manv && d.MaDM == sMaDM));

                    var nvcn = from a in db.NhanVienChucNangs
                               where a.MaNV == manv && a.MaDM == sMaDM && a.MaCN.Equals(sMaCN)
                               select a;
                    if (nvcn.Count() == 0 && sCHK == "true")
                    {
                        var checkpq = from a in db.PhanQuyens
                                      where a.MaDM == sMaDM && a.MaNV == manv
                                      select a;
                        if (checkpq.Count() == 0)
                        {
                            PhanQuyen pq = new PhanQuyen();
                            pq.MaDM = sMaDM;
                            pq.MaNV = manv;
                            db.PhanQuyens.Add(pq);
                        }

                        NhanVienChucNang tb = new NhanVienChucNang();
                        tb.MaNV = manv;
                        tb.MaDM = sMaDM;
                        tb.MaCN = sMaCN;
                        db.NhanVienChucNangs.Add(tb);
                    }
                    db.SaveChanges();
                }
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetHoTen_BySoThe(string sSoThe)
        {
            string data = "";
            try
            {
                var hoten = from a in db.NhanViens
                            where a.SoThe.Equals(sSoThe)
                            select new { a.HoTen };
                data = hoten.FirstOrDefault().HoTen;

                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult ResetMK(string sSoThe, string sPass)
        {
            string passw = MyClss.Encode(sPass);
            try
            {
                var dangnhap = from a in db.DangNhaps
                               where a.Username.Equals(sSoThe)
                               select a;
                if (dangnhap.Count() == 0)
                    return Json(0, JsonRequestBehavior.AllowGet);
                else
                {
                    DangNhap dn = db.DangNhaps.SingleOrDefault(r => r.Username.Equals(sSoThe));
                    if (dn != null)
                    {
                        dn.Pass = passw;
                        db.SaveChanges();
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json("", JsonRequestBehavior.AllowGet);

                }
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LOAD_QUYEN_BY_MANV_COPY(string sSoThe)
        {
            StringBuilder s = new StringBuilder();
            string LID = CookieCls.GetLanguge();
            int manv = int.Parse(CookieCls.GetMaNV());
            string data = "";
            try
            {
                var varmanv = from a in db.NhanViens
                              where a.SoThe.Equals(sSoThe)
                              select new { a.MaNV };
                int mnv = varmanv.FirstOrDefault().MaNV;

                string[] clmenucha = { "bg-red", "bg-blue", "bg-purple", "bg-olive", "bg-yellow" };
                int cha = 0;
                var loaddmCha = (from a in db.DanhMucChaCTs
                                 from b in db.DanhMucChas
                                 from c in db.DanhMucs
                                 from d in db.NhanVienChucNangs
                                 where a.MaDMC == b.MaDMC && b.MaDMC == c.MaDMC && c.MaDM == d.MaDM && a.LID.Equals(LID) && d.MaNV == mnv && b.HienThi == true && c.HienThi == true
                                 select new { b.MaDMC, a.TenDMC }).Distinct();
                if (loaddmCha.Count() > 0)
                {
                    s.Append("<div class='scrollbar' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 300) + "px;' >");
                    s.Append("<ul class='timeline'>");
                    foreach (var r in loaddmCha)
                    {
                        s.Append("  <li class='time-label'> <span class='" + clmenucha[cha].ToString() + "'>" + r.TenDMC + "</span> </li>");
                        cha++;
                        if (cha >= clmenucha.Count())
                        {
                            cha = 0;
                        }
                        int madmc = r.MaDMC;
                        var loadmd = (from a in db.DanhMucs
                                      from b in db.DanhMucCTs
                                      from c in db.NhanVienChucNangs
                                      from d in db.NhanViens
                                      where a.MaDM == b.MaDM && a.MaDM == c.MaDM && c.MaNV == d.MaNV && b.LID.Equals(LID) && a.MaDMC == madmc && d.MaNV == manv && a.HienThi == true
                                      orderby a.ThuTu
                                      select new { a.MaDM, a.MaDMC, a.Site, b.TenDM, a.ThuTu }).Distinct();
                        if (loadmd.Count() > 0)
                        {
                            string[] color = { "bg-blue", "bg-yellow", "bg-red", "bg-purple", "bg-olive" };
                            int clo = 0;
                            foreach (var r2 in loadmd)
                            {
                                s.Append("<li> <i class='fa fa-check " + color[clo].ToString() + "'></i><div class='timeline-item'><h3 class='timeline-header'><a>" + r2.TenDM + "</a></h3>");
                                clo++;
                                if (clo >= color.Count())
                                {
                                    clo = 0;
                                }
                                s.Append(" <div class='timeline-body'>");
                                int mdm = r2.MaDM;
                                var loadcn = from a in db.DanhMucs
                                             from b in db.ChucNangs
                                             from c in db.NhanVienChucNangs
                                             where a.MaDM == b.MaDM && b.MaDM == c.MaDM && b.MaCN == c.MaCN && c.MaNV == manv && c.MaDM == mdm && b.LID.Equals(LID) && a.HienThi == true
                                             select new { a.MaDM, b.TenCN, c.MaCN };
                                if (loadcn.Count() > 0)
                                {
                                    foreach (var r3 in loadcn)
                                    {
                                        s.Append(" <label class='btn btn-border btn-lg outline'>" + r3.TenCN + "</label>");
                                    }

                                }
                                s.Append("</div>");
                            }
                            s.Append(" </div></li>");
                        }
                    }

                    s.Append("<li><i class='fa fa-clock-o bg-gray'></i></li>");
                    s.Append("</ul>");
                }


                data = "" + s;

                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Load_Auto(string stag)
        {
            string data = "";
            try
            {
                var varTag = from a in db.NhanViens
                             from b in db.DangNhaps
                             where a.MaNV == b.MaNV && a.HienThi == true && a.SoThe.Contains(stag)
                             select new { a.SoThe, a.HoTen };
                if (varTag.Count() > 0)
                {
                    foreach (var r in varTag)
                    {
                        data += r.SoThe + "-" + r.HoTen + "#$%";
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
        public ActionResult Copy_Quyen(string sSoThe1, string sSoThe2)
        {
            try
            {
                var varnguon = from a in db.NhanViens
                               where a.SoThe.Equals(sSoThe1)
                               select new { a.MaNV };
                int mnv1 = varnguon.FirstOrDefault().MaNV;
                var vardich = from a in db.NhanViens
                              where a.SoThe.Equals(sSoThe2)
                              select new { a.MaNV };
                int mnv2 = vardich.FirstOrDefault().MaNV;

                var pq = from a in db.PhanQuyens
                         where a.MaNV == mnv1
                         select new { a.MaDM };
                if (pq.Count() > 0)
                {
                    if (db.PhanQuyens.Where(r => r.MaNV == mnv2).Count() > 0)
                        db.PhanQuyens.RemoveRange(db.PhanQuyens.Where(r => r.MaNV == mnv2));
                    foreach (var r in pq)
                    {
                        PhanQuyen tbpq = new PhanQuyen();
                        int madm = r.MaDM;
                        tbpq.MaNV = mnv2;
                        tbpq.MaDM = madm;
                        db.PhanQuyens.Add(tbpq);
                    }

                    var varnvcn = from a in db.NhanVienChucNangs
                                  where a.MaNV == mnv1
                                  select new { a.MaDM, a.MaCN };
                    if (varnvcn.Count() > 0)
                    {
                        if (db.NhanVienChucNangs.Where(r => r.MaNV == mnv2).Count() > 0)
                            db.NhanVienChucNangs.RemoveRange(db.NhanVienChucNangs.Where(r => r.MaNV == mnv2));

                        foreach (var r2 in varnvcn)
                        {
                            NhanVienChucNang nvcn = new NhanVienChucNang();
                            int madm2 = r2.MaDM;
                            string macn = r2.MaCN;
                            nvcn.MaNV = mnv2;
                            nvcn.MaDM = madm2;
                            nvcn.MaCN = macn;
                            db.NhanVienChucNangs.Add(nvcn);
                        }
                    }
                    db.SaveChanges();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete_Quyen(string sSoThe2)
        {
            try
            {
                var vardich = from a in db.NhanViens
                              where a.SoThe.Equals(sSoThe2)
                              select new { a.MaNV };
                int mnv2 = vardich.FirstOrDefault().MaNV;

                var pq = from a in db.PhanQuyens
                         where a.MaNV == mnv2
                         select new { a.MaDM };
                if (pq.Count() > 0)
                {
                    var varnvcn = from a in db.NhanVienChucNangs
                                  where a.MaNV == mnv2
                                  select a;
                    if (varnvcn.Count() > 0)
                    {
                        if (db.NhanVienChucNangs.Where(r => r.MaNV == mnv2).Count() > 0)
                            db.NhanVienChucNangs.RemoveRange(db.NhanVienChucNangs.Where(r => r.MaNV == mnv2));
                    }

                    if (db.PhanQuyens.Where(r => r.MaNV == mnv2).Count() > 0)
                        db.PhanQuyens.RemoveRange(db.PhanQuyens.Where(r => r.MaNV == mnv2));

                    db.SaveChanges();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LOAD_NHANVIEN_MODAL()
        {
            StringBuilder s = new StringBuilder();
            string LID = CookieCls.GetLanguge();
            int manv = int.Parse(CookieCls.GetMaNV());
            string data = "";
            s.Append("<table class='mytable' style='width:100%;float:left'>");
            s.Append("<thead>");
            s.Append("<tr style='Height:40px'>");
            s.Append("<td style='width:40px;text-align:center'>" + AS.GetTT("tt_stt") + "</td>");
            s.Append("<td style='width:100px'>" + AS.GetTT("tt_bp") + "</td>");
            s.Append("<td style='width:100px'>" + AS.GetTT("tt_card") + "</td>");
            s.Append("<td style='width:150px'>" + AS.GetTT("tt_ht") + "</td>");
            s.Append("<td style='width:100px; text-align:center'>" + AS.GetTT("tt_sex") + "</td>");
            s.Append("<td style='width:70px; text-align:center'>" + AS.GetTT("tt_login") + "</td>");
            s.Append("<td style='text-align:center; width:50px'>#</td>");
            s.Append("</tr>");
            s.Append("<tr>");
            s.Append("<td></td>");
            s.Append("<td>");
            s.Append("<div style='width:100%' class=\"custom-select\"><select style='width:100%; height:30px;z-index:10' id='slnhom_modal' onchange='ShowDataByPage_Modal(1)'>");
            var derp = from a in db.NhomNhanVienCTs
                       from b in db.NhomNhanViens
                       where a.NhomNVID == b.NhomNVID && a.LID.Equals(LID)
                       orderby b.STT ascending
                       select new { a.NhomNVID, a.TenNhom };
            if (derp.Count() > 0)
            {
                s.Append("<option value='' class='option_empty'>Chọn bộ phận</option>");
                foreach (var r in derp)
                {
                    s.Append("<option value='" + r.NhomNVID + "'>" + r.TenNhom + "</option>");
                }
            }
            s.Append("</select></div>");
            s.Append("</td>");
            s.Append("<td><input type='text' class='cal' style='width:100%;height:30px' id='txtsothe_modal' onkeyup='ShowDataByPage_Modal(1);'></td>");
            s.Append("<td><input type='text' class='cal' style='width:100%;height:30px' id='txthoten_modal'></td>");
            s.Append("<td style='text-align:center'> <div class='radio'> <label><input name='form-field-radio' type='radio' class='colored-blue chkhidden' id='chkNam' value='1' checked='checked'>     <span class='text'>Nam</span>  &nbsp;</label><label><input name='form-field-radio' type='radio' value='0' class='colored-danger chkhidden' id='chkNu'> <span class='text'>Nữ</span>  </label></div></td>");
            s.Append("<td style='width:70px; text-align:center'><label class='lbcheck' style='z-index:1000'><input class='chkhidden' type='checkbox' checked='checked' id='chkLogin_modal'> <span class='text' style='z-index:1'></span></td>");

            s.Append("<td style='font-weight:bold;color:#69B20E;width:5%' align='center'>");
            s.Append("<input type='button' class='add2' style='cursor:pointer;border:none' id='btnInsertNV' onclick=\"InsertNV()\">");
            s.Append("</td>");
            s.Append("</tr>");
            s.Append("</thead>");

            s.Append("<tbody id='body_dsnv'>");
            var loadnv = from a in db.NhanViens
                         from b in db.NhomNhanViens
                         from c in db.NhomNhanVienCTs
                         where a.NhomNVID == b.NhomNVID && b.NhomNVID == c.NhomNVID && c.LID.Equals(LID)
                         select new { a.NhomNVID, a.MaNV, a.SoThe, a.HoTen, a.GioiTinh, a.HienThi, c.TenNhom };
            if (loadnv.Count() > 0)
            {
                int stt = 1;
                foreach (var r in loadnv)
                {
                    s.Append("<tr id='tr_modal" + r.MaNV + "'>");

                    s.Append("<td style='text-align:center'>" + stt + "</td>");
                    s.Append("<td  style=''  id='mnv" + r.MaNV + "'>");
                    s.Append("<div style='' class=\"custom-select\"><select style='width:100%; height:30px' onchange=\"UpdateNV('" + r.MaNV + "')\"  id='slnhom_modal_action" + r.MaNV + "'>");
                    var derp2 = from a in db.NhomNhanVienCTs
                                from b in db.NhomNhanViens
                                where a.NhomNVID == b.NhomNVID && a.LID.Equals(LID)
                                orderby b.STT ascending
                                select new { a.NhomNVID, a.TenNhom };
                    if (derp2.Count() > 0)
                    {
                        foreach (var r2 in derp2)
                        {
                            s.Append("<option value='" + r2.NhomNVID + "' " + (r.NhomNVID == r2.NhomNVID ? "selected" : "") + ">" + r2.TenNhom + "</option>");
                        }
                    }

                    s.Append("</select></div>");
                    s.Append("</td>");

                    s.Append("<td>");
                    s.Append("<input type='text' id='txtSoThe" + r.MaNV + "' class='txtn'  onchange=\"UpdateNV('" + r.MaNV + "')\"  style='width:100%' value='" + r.SoThe + "'>");
                    s.Append("</td>");
                    s.Append("<td>");
                    s.Append("<input type='text' id='txtHoTen" + r.MaNV + "' class='txtn'  onchange=\"UpdateNV('" + r.MaNV + "')\"  style='width:100%' value='" + r.HoTen + "'>");
                    s.Append("</td>");
                    string ht = r.HienThi == true ? " checked=checked" : "";
                    string gtnam = r.GioiTinh == 1 ? " checked=checked" : "";
                    string gtnu = r.GioiTinh == 0 ? " checked=checked" : "";

                    var dn = from a in db.DangNhaps
                             where a.MaNV == r.MaNV && a.Allow == true
                             select a;
                    string LG = dn.Count() > 0 ? " checked=checked" : "";

                    s.Append("<td style='text-align:center;'> <div class='radio'> <label><input name='form-field-radio" + r.MaNV + "' type='radio' value='1' onchange=\"UpdateNV('" + r.MaNV + "')\"  class='colored-blue chkhidden' " + gtnam + " id='chkNam" + r.MaNV + "'><span class='text'>Nam</span>  </label> <label><input name='form-field-radio" + r.MaNV + "' type='radio' value='0' onchange=\"UpdateNV('" + r.MaNV + "')\"  class='colored-danger chkhidden' " + gtnu + "  id='chkNu" + r.MaNV + "'> <span class='text'>Nữ</span>  </label></div></td>");                  
                    s.Append("<td style='text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + LG + " type='checkbox' onchange=\"UpdateNVDangNhap('" + r.MaNV + "')\"  id='chkLogin" + r.MaNV + "'> <span class='text' style='z-index:0'></span></td>");
                    s.Append("<td  style='text-align:center;'>");

                    s.Append("<input type='button' id='delModal" + r.MaNV + "' class='imgdel'  onclick=\"DeleteNV('" + r.MaNV + "')\">");
                    s.Append("</td>");
                    s.Append("</tr>");
                    stt++;
                }
            }
            s.Append("</tbody>");
            s.Append("</table>");

            data = "" + s;
            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LOAD_NHANVIEN_INSERT()
        {
            StringBuilder s = new StringBuilder();
            string LID = CookieCls.GetLanguge();
            int manv = int.Parse(CookieCls.GetMaNV());
            string data = "";     
            
            var loadnv = from a in db.NhanViens
                         from b in db.NhomNhanViens
                         from c in db.NhomNhanVienCTs
                         where a.NhomNVID == b.NhomNVID && b.NhomNVID == c.NhomNVID && c.LID.Equals(LID)
                         select new { a.NhomNVID, a.MaNV, a.SoThe, a.HoTen, a.GioiTinh, a.HienThi, c.TenNhom };
            if (loadnv.Count() > 0)
            {
                int stt = 1;
                foreach (var r in loadnv)
                {
                    s.Append("<tr id='tr_modal" + r.MaNV + "'>");

                    s.Append("<td style='text-align:center'>" + stt + "</td>");
                    s.Append("<td  style=''  id='mnv" + r.MaNV + "'>");
                    s.Append("<div style='' class=\"custom-select\"><select style='width:100%; height:30px' onchange=\"UpdateNV('" + r.MaNV + "')\"  id='slnhom_modal_action" + r.MaNV + "'>");
                    var derp2 = from a in db.NhomNhanVienCTs
                                from b in db.NhomNhanViens
                                where a.NhomNVID == b.NhomNVID && a.LID.Equals(LID)
                                orderby b.STT ascending
                                select new { a.NhomNVID, a.TenNhom };
                    if (derp2.Count() > 0)
                    {
                        foreach (var r2 in derp2)
                        {
                            s.Append("<option value='" + r2.NhomNVID + "' " + (r.NhomNVID == r2.NhomNVID ? "selected" : "") + ">" + r2.TenNhom + "</option>");
                        }
                    }

                    s.Append("</select></div>");
                    s.Append("</td>");

                    s.Append("<td>");
                    s.Append("<input type='text' id='txtSoThe" + r.MaNV + "' class='txtn'  onchange=\"UpdateNV('" + r.MaNV + "')\"  style='width:100%' value='" + r.SoThe + "'>");
                    s.Append("</td>");
                    s.Append("<td>");
                    s.Append("<input type='text' id='txtHoTen" + r.MaNV + "' class='txtn'  onchange=\"UpdateNV('" + r.MaNV + "')\"  style='width:100%' value='" + r.HoTen + "'>");
                    s.Append("</td>");
                    string ht = r.HienThi == true ? " checked=checked" : "";
                    string gtnam = r.GioiTinh == 1 ? " checked=checked" : "";
                    string gtnu = r.GioiTinh == 0 ? " checked=checked" : "";

                    var dn = from a in db.DangNhaps
                             where a.MaNV == r.MaNV && a.Allow == true
                             select a;
                    string LG = dn.Count() > 0 ? " checked=checked" : "";

                    s.Append("<td style='text-align:center;'> <div class='radio'> <label><input name='form-field-radio" + r.MaNV + "' type='radio' value='1' onchange=\"UpdateNV('" + r.MaNV + "')\"  class='colored-blue chkhidden' " + gtnam + " id='chkNam" + r.MaNV + "'><span class='text'>Nam</span>  </label> <label><input name='form-field-radio" + r.MaNV + "' type='radio' value='0' onchange=\"UpdateNV('" + r.MaNV + "')\"  class='colored-danger chkhidden' " + gtnu + "  id='chkNu" + r.MaNV + "'> <span class='text'>Nữ</span>  </label></div></td>");
                    s.Append("<td style='text-align:center'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + LG + " type='checkbox' onchange=\"UpdateNVDangNhap('" + r.MaNV + "')\"  id='chkLogin" + r.MaNV + "'> <span class='text' style='z-index:0'></span></td>");
                    s.Append("<td  style='text-align:center;'>");

                    s.Append("<input type='button' id='delModal" + r.MaNV + "' class='imgdel'  onclick=\"DeleteNV('" + r.MaNV + "')\">");
                    s.Append("</td>");
                    s.Append("</tr>");
                    stt++;
                }
            }
            
            data = "" + s;
            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateNV(string sMaNV, string sSoThe, string sHoTen, int sGioiTinh, string sNhomNDID)
        {
            try
            {
                int mnv = int.Parse(sMaNV);
                string Pass = MyClss.Encode(sSoThe);
                NhanVien nv = db.NhanViens.SingleOrDefault(r => r.MaNV == mnv);
                if (nv != null)
                {
                    nv.SoThe = sSoThe;
                    nv.HoTen = sHoTen;
                    nv.GioiTinh = sGioiTinh;
                    nv.NhomNVID = sNhomNDID;

                    DangNhap dn = db.DangNhaps.SingleOrDefault(r => r.MaNV == mnv);
                    if (dn != null)
                    {
                        dn.Allow = true;
                        dn.Pass = Pass;
                    }

                    db.SaveChanges();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json("", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateNV_DangNhap(string sMaNV, string sSoThe, string sAllow)
        {
            try
            {
                int mnv = int.Parse(sMaNV);
                string Pass = MyClss.Encode(sSoThe);
                DangNhap dn = db.DangNhaps.SingleOrDefault(r => r.MaNV == mnv);
                if (dn != null)
                {
                    dn.Allow = sAllow == "true" ? true : false;
                    dn.Username = sSoThe;
                    dn.Pass = Pass;

                }
                else
                {
                    dn = new DangNhap();
                    dn.MaNV = mnv;
                    dn.Username = sSoThe;
                    dn.Allow = sAllow == "true" ? true : false;
                    dn.Pass = Pass;
                    db.DangNhaps.Add(dn);

                }
                db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteNV(string sMaNV)
        {
            try
            {
                int mnv = int.Parse(sMaNV);
                if (db.DangNhaps.Where(d => d.MaNV == mnv).Count() > 0)
                {
                    db.DangNhaps.RemoveRange(db.DangNhaps.Where(d => d.MaNV == mnv));
                    if (db.NhanViens.Where(d => d.MaNV == mnv).Count() > 0)
                        db.NhanViens.RemoveRange(db.NhanViens.Where(d => d.MaNV == mnv));
                }
                db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult InsertNV(string sSoThe, string sHoTen, int sGioiTinh, string sBoPhan, string sAllow)
        {
            try
            {
                NhanVien nv = db.NhanViens.SingleOrDefault(r => r.SoThe.Equals(sSoThe));
                if(nv!=null)
                {

                }
                else
                {

                    int mnv = db.NhanViens.Select(x => x.MaNV).DefaultIfEmpty(0).Max() + 1;
                    string Pass = MyClss.Encode(sSoThe);
                    nv = new NhanVien();
                    nv.MaNV = mnv;
                    nv.SoThe = sSoThe;
                    nv.HoTen = sHoTen;
                    nv.GioiTinh = sGioiTinh;
                    nv.NhomNVID = sBoPhan;
                    nv.HienThi = true;
                    db.NhanViens.Add(nv);

                    if(sAllow=="true")
                    {
                        DangNhap dn = new DangNhap();
                        dn.MaNV = mnv;
                        dn.Username = sSoThe;
                        dn.Pass = Pass;
                        dn.DuyetTin = false;
                        dn.Allow = true;
                        db.DangNhaps.Add(dn);
                    }
                }
                db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}