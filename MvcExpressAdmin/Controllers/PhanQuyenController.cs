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
                        s.Append("<td style='width:80px' align='center'>  <img src='../Content/Layout/Images/permission.png' class='csspermission' onclick=\"Xem_Quyen_By_MaNV('" + stt + "','" + item.MaNV + "','" + item.HoTen + "')\"/></td>");

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
                s.Append("<td style='width:70%'>" + AS.GetTT("tt_cn") + "<label id='lbHoTen' class='lbcss'></td>");
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
                               select new {b.MaDM,b.MaCN };
                    s.Append("<div style='margin-left:3px;margin-right: 5px;float:left;width:auto;vertical-align:middle;' class='boxper' align='left' id='cn" + r.MaDM + "'>");
                    if (nvcn.Count()>0)
                    {
                        s.Append("<label style='margin:3px'><input type='checkbox' checked='checkbox' class='checkbox-slider slider-icon colored-palegreen' id='chk" + r.MaDM + "-" + r.MaCN+"' value='" + r.MaCN + "' onclick=\"SetFunc('" +r.MaDM + "','" + r.MaCN + "')\"> <span class='text' id='lb" + r.MaDM + r.MaCN + "'></span>");
                        s.Append(" <span style='float:left; margin-right:5px;margin-top:3px;' title='" + r.MaCN + "'>" + r.TenCN + "</span></label></div>");
                    }
                    else
                    {
                        s.Append("<label style='margin:3px'><input type='checkbox' class='checkbox-slider slider-icon colored-palegreen' id='chk" + r.MaDM + "-" + r.MaCN + "' value='" + r.MaCN + "' onclick=\"SetFunc('" + r.MaDM + "','" + r.MaCN + "')\"> <span class='text' id='lb" + r.MaDM+ r.MaCN + "'></span>");
                        s.Append(" <span style='float:left; margin-right:5px;margin-top:3px;' title='" + r.MaCN + "'>" + r.TenCN + "</span></label></div>");
                    }
                }
            }
            return s;
        }
    }
}