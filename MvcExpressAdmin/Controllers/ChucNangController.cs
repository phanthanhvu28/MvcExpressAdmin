using MvcExpressAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Text;

using System.Data;
using MvcExpressAdmin.MyClass;

namespace MvcExpressAdmin.Controllers
{

    public class ChucNangController : PerSiteController
    {
        private NewsDataContext db = new NewsDataContext();
        AdminSiteController AS = new AdminSiteController();
        string languge = CookieCls.GetLanguge();
        // GET: ChucNang
        public string LoadData()
        {
            StringBuilder s = new StringBuilder();
            try
            {
                s.Append("<table width='100%' class='mytable'>");
                s.Append("<thead>");
                s.Append("<tr style='height:60px'>");
                s.Append("<td style='width:120px;height:55px' align='center'><input type='button' class='additem' id='adddanhmucm' value='&nbsp;' onclick=\"AddDanhmuc(0,'dmcha')\">");
                s.Append("</td>");
                s.Append("<td style='width:120px; text-align:center'>" + AS.GetTT("tt_id") + "</td>");
                s.Append("<td style='width:250px;text-align:center'>" + AS.GetTT("tt_tt") + "</td>");
                s.Append("<td style='width:90px;text-align:center'>" + AS.GetTT("tt_hieuluc") + "</td>");
                s.Append("<td style='text-align:left;'>" + AS.GetTT("tt_cn") + "</td>");
                s.Append("</tr>");
                s.Append("</thead></table>");
                s.Append("<div class='scrollbar' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 210) + "px'>");
                s.Append("<table width='100%' class='mytable' id='dsquyen' style='' >");
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
                        s.Append("<tr id='trdmcha" + r.MaDMC + "'>");
                        s.Append("<td  align='center' style='width:120px;background-color:#b5ebf8;' class='heading'>");

                        s.Append("<input type='button' class='add2' id='b" + r.MaDMC + "'onclick=\"AddDanhmuc(" + r.MaDMC + ",'dmcon')\">");

                        s.Append("<input type='button' class='imgedit' id='e" + r.MaDMC + "'onclick=\"EditDanhmuc(" + r.MaDMC + ",'dmcha')\"'>");

                        s.Append("<input type='button' class='imgdel' id='d" + r.MaDMC + "'  onclick=\"DeleteDanhMuc(" + r.MaDMC + ",'dmcha')\">");

                        s.Append("</td>");

                        s.Append("<td  style='width:50px;background-color:#b5ebf8' class='heading'  align='center' id='m" + r.MaDMC + "'>" + r.MaDMC + "</td>");
                        s.Append("<td class='heading'  style='width:200px;background-color:#b5ebf8;font-size:17px;font-weight:bold;' colspan='2' id='n" + r.MaDMC + "'>" + r.TenDMC + "</td>");
                        string HT = r.HienThi == true ? " checked=checked" : "";

                        s.Append("<td style='width:90px;text-align:left;background-color:#b5ebf8;'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT + " type='checkbox' onchange=\"Update_HieuLuc_Cha('" + r.MaDMC + "')\"  id='chkDMCha_" + r.MaDMC + "'> <span class='text' style='z-index:0'></span></td>");

                        s.Append("<td  align='center' style='background-color:#b5ebf8;' colspan='2' class='heading'></td>");
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
                                s.Append("<tr id='submenu" + r2.MaDM + "'>");
                                s.Append("<td  align='center'  style='width:40px;background-color:#b5ebf8'>&nbsp;</td>");
                                s.Append("<td  align='center' style='width:120px;' class='heading'>");

                                s.Append("<input type='button' class='imgedit' id='e" + r2.MaDM + "'  onclick=\"EditDanhmuc('" + r2.MaDM + "','dmcon')\">");

                                s.Append("<input type='button' class='imgdel' id='sub_del" + r2.MaDM + "'  onclick=\"DeleteDanhMuc('" + r2.MaDM + "','dmcon')\">");
                                s.Append("</td>");

                                s.Append("<td  style='margin-left:40px;width:50px;'  class='heading'  align='center' id='m" + r2.MaDM + "'>" + r2.MaDM + "</td>");
                                s.Append("<td class='heading' style='width:200px'  id='n" + r2.MaDM + "'>" + r2.TenDM + "</td>");
                                string HT2 = r2.HienThi == true ? " checked=checked" : "";

                                s.Append("<td style='width:90px;text-align:center;'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT2 + " type='checkbox' onchange=\"Update_HieuLuc_Con('" + r2.MaDM + "')\"  id='chkDMCon_" + r2.MaDM + "'> <span class='text' style='z-index:0'></span></td>");

                                s.Append("<td id='cn" + r2.MaDM + "'>" + GetCN(r2.MaDM));

                                s.Append("<input type='button' style='float:right' class='add2' id='fc" + r.MaDMC + "'  onclick=\"AddFunc('" + r2.MaDM + "')\"></td>");
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
        public string LOAD_JAVASCRIPT_DANHMUC()
        {
            StringBuilder s = new StringBuilder();
            try
            {
                s.Append("<tbody id='dscn'>");
                var dmc = from a in db.DanhMucChaCTs
                          from b in db.DanhMucChas
                          where a.MaDMC == b.MaDMC && a.LID.Equals(languge) && b.HienThi == true
                          orderby b.ThuTu
                          select new { b.MaDMC, a.TenDMC, b.HienThi };
                if (dmc.Count() > 0)
                {
                    foreach (var r in dmc)
                    {
                        s.Append("<tr id='trdmcha" + r.MaDMC + "'>");
                        s.Append("<td  align='center' style='width:120px;background-color:#b5ebf8;' class='heading'>");

                        s.Append("<input type='button' class='add2' id='b" + r.MaDMC + "'onclick=\"AddDanhmuc(" + r.MaDMC + ",'dmcon')\">");

                        s.Append("<input type='button' class='imgedit' id='e" + r.MaDMC + "'onclick=\"EditDanhmuc(" + r.MaDMC + ",'dmcha')\"'>");


                        s.Append("<input type='button' class='imgdel' id='d" + r.MaDMC + "'  onclick=\"DeleteDanhMuc(" + r.MaDMC + ",'dmcha')\">");
                        s.Append("</td>");

                        s.Append("<td  style='width:50px;background-color:#b5ebf8' class='heading'  align='center' id='m" + r.MaDMC + "'>" + r.MaDMC + "</td>");
                        s.Append("<td class='heading'  style='width:200px;background-color:#b5ebf8;font-size:17px;font-weight:bold;' colspan='2' id='n" + r.MaDMC + "'>" + r.TenDMC + "</td>");
                        string HT = r.HienThi == true ? " checked=checked" : "";

                        s.Append("<td style='width:90px;text-align:left;background-color:#b5ebf8;'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT + " type='checkbox' onchange=\"Update_HieuLuc_Cha('" + r.MaDMC + "')\"  id='chkDMCha_" + r.MaDMC + "'> <span class='text' style='z-index:0'></span></td>");

                        s.Append("<td  align='center' style='background-color:#b5ebf8;' colspan='2' class='heading'></td>");
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
                                s.Append("<tr id='submenu" + r2.MaDM + "'>");
                                s.Append("<td  align='center'  style='width:40px;background-color:#b5ebf8'>&nbsp;</td>");
                                s.Append("<td  align='center' style='width:120px;' class='heading'>");

                                s.Append("<input type='button' class='imgedit' id='e" + r2.MaDM + "'  onclick=\"EditDanhmuc('" + r2.MaDM + "','dmcon')\">");

                                s.Append("<input type='button' class='imgdel' id='sub_del" + r2.MaDM + "'  onclick=\"DeleteDanhMuc('" + r2.MaDM + "','dmcon')\">");
                                s.Append("</td>");

                                s.Append("<td  style='margin-left:40px;width:50px;'  class='heading'  align='center' id='m" + r2.MaDM + "'>" + r2.MaDM + "</td>");
                                s.Append("<td class='heading' style='width:200px'  id='n" + r2.MaDM + "'>" + r2.TenDM + "</td>");
                                string HT2 = r2.HienThi == true ? " checked=checked" : "";

                                s.Append("<td style='width:90px;text-align:center;'><label class='lbcheck' style='z-index:1000'><input style='z-index:1' class='chkhidden' " + HT2 + " type='checkbox' onchange=\"Update_HieuLuc_Con('" + r2.MaDM + "')\"  id='chkDMCon_" + r2.MaDM + "'> <span class='text' style='z-index:0'></span></td>");

                                s.Append("<td id='cn" + r2.MaDM + "'>" + GetCN(r2.MaDM));

                                s.Append("<input type='button' style='float:right' class='add2' id='fc" + r.MaDMC + "'  onclick=\"AddFunc('" + r2.MaDM + "')\"></td>");
                                s.Append("</tr>");
                            }
                        }
                    }
                }
                s.Append("</tbody>");
            }
            catch { }
            return "" + s;
        }
        public StringBuilder GetCN(int MADM)
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
                    s.Append("<div style='margin-left:3px;min-width:60px;text-align:center;padding:3px;float:left;width:auto;vertical-align:middle;' class='boxper' align='left' id='" + r.MaDM + "-" + r.MaCN + "'>");

                    s.Append("<label>");

                    s.Append("<input type='button' class='imgedit2' id='editcn" + MADM + "-" + r.MaCN + "'  onclick=\"UpdateCN('" + MADM + "','" + r.MaCN + "')\">");
                    s.Append(" <span style='float:left;margin-top:5px;' title='" + r.MaCN + "'>" + r.TenCN + "</span>");

                    s.Append("<input type='button' class='imgdel2' id='cn" + MADM + "-" + r.MaCN + "'  onclick=\"DeleteCN('" + r.MaDM + "-" + r.MaCN + "','" + MADM + "','" + r.MaCN + "')\">");
                    s.Append("</label></div>");

                }
            }
            return s;
        }
        public string RadioLanguge()
        {
            StringBuilder s = new StringBuilder();
            var nn = from a in db.NgonNgus
                     orderby a.ThuTu
                     select a;
            if (nn.Count() > 0)
            {

                foreach (var v in nn)
                {
                    string lg = v.LID == CookieCls.GetLanguge() ? " checked=checked" : "";

                    s.Append("<label style='padding-right: 10px;'>");
                    s.Append(" <input name='form-field-radio1' " + lg + " class='chkhidden " + v.Class + "' value='" + v.LID + "' type=\"radio\" id=\"chk" + v.LID + "\"   onclick=\"ChangeLang('" + v.LID + "')\"/> ");
                    s.Append(" <span class=\"text\">" + v.LName + "</span> ");
                    s.Append("</label>");
                }
            }
            return "" + s;
        }
        public string RadioLangugeFunc()
        {
            StringBuilder s = new StringBuilder();
            var nn = from a in db.NgonNgus
                     orderby a.ThuTu
                     select a;
            if (nn.Count() > 0)
            {
                foreach (var v in nn)
                {
                    string lg = v.LID == CookieCls.GetLanguge() ? " checked=checked" : "";
                    s.Append("<label style='padding-right: 10px;'>");
                    s.Append(" <input name='form-field-radio2' " + lg + " class='chkhidden " + v.Class + "' value='" + v.LID + "' type=\"radio\" id=\"chkcn" + v.LID + "\"   onclick=\"ChangeLang_CN('" + v.LID + "')\"/> ");
                    s.Append(" <span class=\"text\">" + v.LName + "</span> ");
                    s.Append("</label>");
                }
            }
            return "" + s;
        }
        [HttpPost]
        public ActionResult INSERT_UPDATE_DANHMUC(string sLOAI, string sMADMCHA, string sTENMDC, string sSITE, string sHIENTHI, string sSTT, string sLID, string sMaDMCon, string Bien)
        {

            int smdmc = int.Parse(sMADMCHA);
            if (sLOAI == "dmcha")
            {
                string cssClassCha = "menu-icon fa fa-th";
                DanhMucCha dmc = db.DanhMucChas.SingleOrDefault(r => r.MaDMC == smdmc);
                if (dmc != null)
                {
                    dmc.ThuTu = int.Parse(sSTT);
                    dmc.HienThi = (sHIENTHI == "true" ? true : false);
                    DanhMucChaCT dmcCT = db.DanhMucChaCTs.SingleOrDefault(r => r.MaDMC == smdmc && r.LID.Equals(sLID));
                    if (dmcCT != null)
                    {
                        dmcCT.TenDMC = sTENMDC;
                    }
                    else
                    {

                        dmcCT = new DanhMucChaCT();
                        dmcCT.MaDMC = smdmc;
                        dmcCT.LID = sLID;
                        dmcCT.TenDMC = sTENMDC;
                        db.DanhMucChaCTs.Add(dmcCT);
                    }
                }
                else
                {
                    int madmcha = 0, madmcha_old = 0;
                    if (Bien == "0")
                    {
                        madmcha = db.DanhMucChas.Select(x => x.MaDMC).DefaultIfEmpty(0).Max() + 1;
                        dmc = new DanhMucCha();
                        dmc.MaDMC = madmcha;
                        dmc.CssClass = cssClassCha;
                        dmc.ThuTu = int.Parse(sSTT);
                        dmc.HienThi = (sHIENTHI == "true" ? true : false);
                        db.DanhMucChas.Add(dmc);
                    }
                    else
                    {
                        madmcha_old = db.DanhMucChas.Select(x => x.MaDMC).DefaultIfEmpty(0).Max();
                    }
                    DanhMucChaCT dmcCT = db.DanhMucChaCTs.SingleOrDefault(r => r.MaDMC == (Bien == "0" ? madmcha : madmcha_old) && r.LID.Equals(sLID));
                    if (dmcCT != null)
                    {
                        dmcCT.TenDMC = sTENMDC;
                    }
                    else
                    {
                        dmcCT = new DanhMucChaCT();
                        dmcCT.MaDMC = (Bien == "0" ? madmcha : madmcha_old);
                        dmcCT.LID = sLID;
                        dmcCT.TenDMC = sTENMDC;
                        db.DanhMucChaCTs.Add(dmcCT);
                    }
                }
            }
            else if (sLOAI == "dmcon")
            {
                int smdmcon = int.Parse(sMaDMCon);
                string cssclassCon = "fm-user-secret";
                DanhMuc dm = db.DanhMucs.SingleOrDefault(a => a.MaDM == smdmcon);
                if (dm != null)
                {
                    dm.Site = sSITE;
                    dm.ThuTu = int.Parse(sSTT);
                    dm.HienThi = (sHIENTHI == "true" ? true : false);

                    DanhMucCT dmCT = db.DanhMucCTs.SingleOrDefault(a => a.MaDM == smdmcon && a.LID.Equals(sLID));
                    if (dmCT != null)
                    {
                        dmCT.TenDM = sTENMDC;
                    }
                    else
                    {
                        dmCT = new DanhMucCT();
                        dmCT.MaDM = smdmcon;
                        db.DanhMucCTs.Add(dmCT);
                    }
                }
                else
                {
                    int mdm = 0, madm_old = 0;
                    if (Bien == "0")
                    {
                        mdm = db.DanhMucs.Select(a => a.MaDM).DefaultIfEmpty(0).Max() + 1;
                        dm = new DanhMuc();
                        dm.MaDM = mdm;
                        dm.MaDMC = smdmc;
                        dm.Site = sSITE;
                        dm.ThuTu = int.Parse(sSTT);
                        dm.HienThi = (sHIENTHI == "true" ? true : false);
                        dm.Icon = cssclassCon;
                        db.DanhMucs.Add(dm);

                    }
                    else
                    {
                        madm_old = db.DanhMucs.Select(a => a.MaDM).DefaultIfEmpty(0).Max();
                    }
                    DanhMucCT dmCT = db.DanhMucCTs.SingleOrDefault(a => a.MaDM == (Bien == "0" ? mdm : madm_old) && a.LID.Equals(sLID));
                    if (dmCT != null)
                    {
                        dmCT.TenDM = sTENMDC;
                    }
                    else
                    {
                        dmCT = new DanhMucCT();
                        dmCT.MaDM = (Bien == "0" ? mdm : madm_old);
                        dmCT.TenDM = sTENMDC;
                        dmCT.LID = sLID;
                        db.DanhMucCTs.Add(dmCT);
                    }
                }
            }
            db.SaveChanges();
            return Json("ins", JsonRequestBehavior.AllowGet);//Insert DMC
        }
        public string LoadDMEdit(string sMaDM, string sNN, string sLOAIDM)
        {
            int madm = int.Parse(sMaDM);
            if (sLOAIDM == "dmcha")
            {
                var v = (from a in db.DanhMucChas
                         from b in db.DanhMucChaCTs
                         where a.MaDMC == b.MaDMC && b.LID.Equals(sNN) && a.MaDMC == madm
                         select new { a.MaDMC, a.HienThi, a.ThuTu, b.TenDMC }).SingleOrDefault();
                if (v != null)
                    return v.MaDMC + "#$%" + v.TenDMC + "#$%" + "" + "#$%" + v.HienThi + "#$%" + v.ThuTu;
                return "#$%";
            }
            else
            {
                var v = (from a in db.DanhMucs
                         from b in db.DanhMucCTs
                         where a.MaDM == b.MaDM && b.LID.Equals(sNN) && a.MaDM == madm
                         select new { a.MaDM, a.MaDMC, a.Site, a.HienThi, a.ThuTu, b.TenDM }).SingleOrDefault();

                return (v != null ? v.MaDMC + "#$%" + v.TenDM + "#$%" + v.Site + "#$%" + v.HienThi + "#$%" + v.ThuTu : "#$%");
            }
        }
        public string Show_ChucNang_Update(string sMaDM, string sMaCN, string sNN)
        {
            int madm = int.Parse(sMaDM);
            var v = from a in db.ChucNangs
                    where a.MaDM == madm && a.MaCN.Equals(sMaCN) && a.LID.Equals(sNN)
                    select new { a.TenCN };
            return v.Count() > 0 ? v.SingleOrDefault().TenCN : "";
        }

        [HttpPost]
        public ActionResult INSERT_UPDATE_CHUCNANG(string sMADM, string sMACN, string sLID, string sTENCN)
        {
            int madm = int.Parse(sMADM);
            ChucNang cn = db.ChucNangs.SingleOrDefault(r => r.MaDM == madm && r.LID.Equals(sLID) && r.MaCN.Equals(sMACN));
            if (cn != null)
            {
                cn.TenCN = sTENCN;
            }
            else
            {
                cn = new ChucNang();
                cn.MaDM = madm;
                cn.MaCN = sMACN;
                cn.LID = sLID;
                cn.TenCN = sTENCN;
                db.ChucNangs.Add(cn);
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);//Insert DMC
        }
        [HttpPost]
        public ActionResult Delete_Chucnang(string sMADM, string sMaCN)
        {
            int madm = int.Parse(sMADM);
            var cn = db.ChucNangs.Where(r => r.MaDM == madm && r.MaCN.Equals(sMaCN)).ToList();
            if (cn.Count() > 0)
            {
                db.ChucNangs.RemoveRange(db.ChucNangs.Where(r => r.MaDM == madm && r.MaCN.Equals(sMaCN)));
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete_DanhMuc(string sMADM, string sLOAIDM)
        {
            int madm = int.Parse(sMADM);
            if (sLOAIDM == "dmcha")
            {
                var vdm = db.DanhMucs.Where(a => a.MaDMC == madm);
                if (vdm.Count() > 0)
                {
                    foreach (DanhMuc item in vdm)
                    {
                        //db.DanhMucCTs.RemoveRange(db.DanhMucCTs.Where(a=>a.MaDM==));                  
                        if (db.DanhMucCTs.Where(a => a.MaDM == item.MaDM).Count() > 0)
                            db.DanhMucCTs.RemoveRange(db.DanhMucCTs.Where(a => a.MaDM == item.MaDM));

                        if (db.ChucNangs.Where(a => a.MaDM == item.MaDM).Count() > 0)
                            db.ChucNangs.RemoveRange(db.ChucNangs.Where(a => a.MaDM == item.MaDM));

                        if (db.PhanQuyens.Where(a => a.MaDM == item.MaDM).Count() > 0)
                            db.PhanQuyens.RemoveRange(db.PhanQuyens.Where(a => a.MaDM == item.MaDM));
                    }
                }
                if (db.DanhMucs.Where(r => r.MaDMC == madm).Count() > 0)
                    db.DanhMucs.RemoveRange(db.DanhMucs.Where(r => r.MaDMC == madm));
                if (db.DanhMucChaCTs.Where(r => r.MaDMC == madm).Count() > 0)
                    db.DanhMucChaCTs.RemoveRange(db.DanhMucChaCTs.Where(r => r.MaDMC == madm));
                if (db.DanhMucChas.Where(r => r.MaDMC == madm).Count() > 0)
                    db.DanhMucChas.RemoveRange(db.DanhMucChas.Where(r => r.MaDMC == madm));
            }
            else // dmcon
            {
                if (db.DanhMucChaCTs.Where(r => r.MaDMC == madm).Count() > 0)
                    db.DanhMucCTs.RemoveRange(db.DanhMucCTs.Where(r => r.MaDM == madm));
                if (db.ChucNangs.Where(r => r.MaDM == madm).Count() > 0)
                    db.ChucNangs.RemoveRange(db.ChucNangs.Where(r => r.MaDM == madm));
                if (db.PhanQuyens.Where(r => r.MaDM == madm).Count() > 0)
                    db.PhanQuyens.RemoveRange(db.PhanQuyens.Where(r => r.MaDM == madm));
                if (db.NhanVienChucNangs.Where(r => r.MaDM == madm).Count() > 0)
                    db.NhanVienChucNangs.RemoveRange(db.NhanVienChucNangs.Where(r => r.MaDM == madm));
                if (db.DanhMucs.Where(r => r.MaDM == madm).Count() > 0)
                    db.DanhMucs.RemoveRange(db.DanhMucs.Where(r => r.MaDM == madm));
            }
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update_HieuLuc_Cha(string sMaDMC, string sHienThi)
        {
            int madm = int.Parse(sMaDMC);
            bool hienthi = (sHienThi == "true" ? true : false);
            DanhMucCha dmc = db.DanhMucChas.SingleOrDefault(r => r.MaDMC == madm);
            if (dmc != null)
            {
                dmc.HienThi = hienthi;
            }
            db.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update_HieuLuc_Con(string sMaDM, string sHienThi)
        {
            int madm = int.Parse(sMaDM);
            bool hienthi = (sHienThi == "true" ? true : false);
            DanhMuc dm = db.DanhMucs.SingleOrDefault(r => r.MaDM == madm);
            if (dm != null)
            {
                dm.HienThi = hienthi;
            }
            db.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}