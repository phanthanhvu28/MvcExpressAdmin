using HtmlAgilityPack;
using MvcExpressAdmin.Models;
using MvcExpressAdmin.MyClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcExpressAdmin.Controllers
{
    public class AllRssController : PerSiteController
    {
        // GET: AllRss
        NewsDataContext db = new NewsDataContext();
        AdminSiteController ad = new AdminSiteController();
        int UserId = int.Parse(CookieCls.GetMaNV());
        public string PerFunction()
        {
            StringBuilder s = new StringBuilder();
            s.Append("<div style='float: left; clear: both;'>");
            s.Append("<label class='icon120m bg-red1' onclick='SyncData()'><i class='fa_excel fa-database bg-blue2'></i> </label>");
            s.Append("<label class='icon120blue bg-yellow cursor' id='lbSync' onclick='SyncData()'>");
            s.Append("<strong id='msgArray' style='color: white; font-weight: bold; font-size: 20px'>0 (R=0 S=0 D=0 F=0 <strong style='color:Red'>W=0</strong>)</strong>");
            s.Append("</label>");
            s.Append("</div>");

            if (ad.Per("resetArr"))
            {
                s.Append("<div style='float: left;'>");
                s.Append("<label class='icon120m bg-blue1' onclick='ResetArr()'><i class='fa_excel fa-refresh bg-red'></i> </label>");
                s.Append("<label class='icon120blue bg-red cursor' id='lbReset' onclick='ResetArr()'> Reset Array data </label>");
                s.Append("</div>");
            }
            if (ad.Per("updSelect"))
            {
                s.Append("<div style='float: left;'>");
                s.Append("<label class='icon120m bg-yellow' onclick='UpdSelect()'><i class='fa_excel fa-refresh bg-blue2'></i> </label>");
                s.Append("<label class='icon120r bg-blue2 cursor' id='lbSelect' onclick='UpdSelect()'>  Cập nhật Rss chọn </label>");
                s.Append("</div>");
            }
            if (ad.Per("updAll"))
            {
                s.Append("<div style='float: left;'>");
                s.Append("<label class='icon120m bg-green1' onclick='UpdAll()'><i class='fa_excel fa-refresh bg-red1'></i> </label>");
                s.Append("<label class='icon120r bg-red1 cursor' id='lbSelect' onclick='UpdAll()'>Cập nhật tất cả Rss</label>");
                s.Append("</div>");
            }

            return "" + s;
        }
        public string LoadNewsPaper()
        {
            StringBuilder s = new StringBuilder();
            s.Append("<div style='margin: 0 auto;overflow: auto;width: 100%;position: relative;height:" + (ad.sHeight() - 190) + "px'>");
            s.Append("<table id='listNewspaper' width='100%' class='mytable' style='font-size:16px;'>");
            s.Append("<thead>");
            s.Append("<tr style='height:60px'>");
            s.Append("<td style='width:50px;height:55px' align='center'><label class='lbcheck' style='z-index:1000'><input class='chkhidden' value='' type='checkbox' onclick=\"CheckAllNewsPaper()\" id='chkAll'> <span class='text'></span>");
            s.Append("</td>");
            s.Append("<th class='heading' style='width:50px; text-align:center'>STT</th>");
            s.Append("<th class='heading' style='width:50px;text-align:center'>ID</th>");
            s.Append("<th class='heading' style='width:150px;text-align:left'>Trang báo</th>");
            s.Append("<th class='heading' style='text-align:left;'>Webiste</th>");
            s.Append("<th class='heading' style='width:150px;text-align:center;'>S.L menu</th>");
            s.Append("<th class='heading' style='width:150px;text-align:center;'>S.L menu lỗi</th>");
            s.Append("<th class='heading' style='width:150px;text-align:center;'>Tin tức sau 2h</th>");
            s.Append("</tr>");
            s.Append("</thead>");
            s.Append("<tbody>");

            var regions = (from a in db.mNewspapers
                           from b in db.PhanQuyenTBs
                           from c in db.Regions
                           where a.NewspaperId == b.NewspaperId && c.MaNuoc.Equals(a.Languge) && b.MaNV == UserId
                           && a.Display == true && c.HienThi == true
                           orderby a.Stt
                           select new { c.MaNuoc, c.TenNuoc, c.GMT }).Distinct();

            if (regions.Count() > 0)
            {
                foreach (var region in regions)
                {
                    DateTimeOffset newTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(region.GMT));
                    s.Append("<tr>");
                    s.Append("<td colspan=3 class='heading' style='width:100px;text-align:center;height:50px;background-color:#EFFBFB;font-size:19px;font-weight:bold;color:#00b8e6'>" + region.TenNuoc + "</td>");
                    s.Append("<td colspan=5 class='heading' style='height:50px;background-color:#EFFBFB;font-size:19px;font-weight:bold;color:#00b8e6'><strong style='font-size:25px; color:#DF0101'>" + newTime.ToString("HH:mm") + "  </strong><strong style='font-size:16px'>" + newTime.ToString("dd-MM-yyyy") + "</strong></td>");
                    s.Append("</tr>");

                    string manuoc = region.MaNuoc;
                    var v = from a in db.mNewspapers
                            from b in db.PhanQuyenTBs
                            where a.NewspaperId == b.NewspaperId && a.Languge.Equals(manuoc) && b.MaNV == UserId
                            && a.Display == true
                            orderby a.Stt
                            select new { a.NewspaperId, a.Title, a.Website, a.Languge };
                    if (v.Count() > 0)
                    {
                        int stt = 1;
                        foreach (var r in v)
                        {
                            var countMenu = (from a in db.mNewspaperMenus
                                             where a.NewspaperId == r.NewspaperId && a.Display == true && !a.rssLink.Equals("")                                             
                                             select a).Count();
                            int countNews = (from a in db.rssNews
                                             from b in db.mNewspaperMenus
                                             where a.NewspaperMenuId == b.NewspaperMenuId && b.NewspaperId == r.NewspaperId
                                            // && a.DateInput.Value.Date.Equals(DateTime.Now.Date)                                             
                                             select a).Count();

                            var countMenuError = countMenu;

                            s.Append("<tr style='font-size:18px;'>");
                            s.Append("<td style='width:50px;height:55px' align='center'><label for='chkNewsPaper" + r.NewspaperId + "' class='lbcheck' style='z-index:1000'><input class='chkhidden' value='" + r.NewspaperId + "' type='checkbox' onclick=\"SelNewsPaper('chkNewsPaper" + r.NewspaperId + "'," + r.NewspaperId + ")\" id='chkNewsPaper" + r.NewspaperId + "' name='chkPaper'> <span class='text'></span>");
                            s.Append("</td>");
                            s.Append("<td style='width:50px; text-align:center'>" + stt + "</td>");
                            s.Append("<td style='width:50px;text-align:center'>" + r.NewspaperId + "</td>");
                            s.Append("<td style='width:150px;text-align:left'>" + r.Title + "</td>");
                            s.Append("<td style='text-align:left'>" + r.Website + "</td>");
                            s.Append("<td style='width:150px;text-align:center'>" + countMenu + "</td>");
                            s.Append("<td style='width:150px;text-align:center; color:red'>" + countMenuError + "</td>");
                            s.Append("<td style='width:150px;text-align:center'>" + string.Format("{0:#.###}", countNews) + "</td>");

                            s.Append("</tr>");
                            stt++;
                        }
                    }
                }
            }
            //s.Append("</tfooter style='position:fixed'>");
            //s.Append("<tr>");
            //s.Append("<td>Tổng</td>");
            //s.Append("</tr>");
           // s.Append("</tfooter>");
            s.Append("</tbody>");
            s.Append("</table>");
            s.Append("</div>");
            return "" + s;
        }

        public string LoadNewsPaperRun()
        {
            StringBuilder s = new StringBuilder();
            s.Append("<div style='margin: 0 auto;overflow: auto;width: 100%;position: relative;height:" + (ad.sHeight() - 190) + "px'>");
            s.Append("<table id='listRun' width='100%' class='mytable' style='font-size:16px;'>");
            s.Append("<thead>");
            s.Append("<tr style='height:60px'>");           
            s.Append("<th class='heading' style='width:50px;text-align:center'>ID</th>");
            s.Append("<th class='heading' style='width:150px;text-align:left'>Trang báo</th>");
            s.Append("<th class='heading' style='text-align:left;'>Time run</th>");
           
            s.Append("</tr>");
            s.Append("</thead>");
            s.Append("<tbody>");

            var users = (from a in db.mProcesses
                           from b in db.NhanViens
                           where a.MaNV == b.MaNV
                           orderby a.DateUpdate descending
                           select new { a.MaNV, b.HoTen }).Distinct();

            if (users.Count() > 0)
            {
                foreach (var user in users)
                {                   
                    s.Append("<tr>");
                    s.Append("<td colspan=3 class='heading' style='width:100px;text-align:left;height:50px;background-color:#EFFBFB;font-size:19px;font-weight:bold;color:#00b8e6'>" + user.HoTen + "</td>");                   
                    s.Append("</tr>");

                    int manv = user.MaNV;
                    var v = from a in db.mProcesses
                            from b in db.mNewspapers
                            where a.NewspaperId == b.NewspaperId && b.Display == true && a.MaNV == manv
                            orderby a.NewspaperId
                            select new { a.NewspaperId, b.Title, a.DateUpdate };
                    if (v.Count() > 0)
                    {                     
                        foreach (var r in v)
                        {                           
                            s.Append("<tr style='font-size:18px;'>");                          
                            s.Append("<td style='width:50px;text-align:center'>" + r.NewspaperId + "</td>");
                            s.Append("<td style='width:150px;text-align:left'>" + r.Title + "</td>");
                            s.Append("<td style='text-align:left'>" + String.Format("{0:dd-MM-yyyy  HH:mm:ss}", r.DateUpdate) + "</td>");
                            s.Append("</tr>");                         
                        }
                    }
                }
            }        
            s.Append("</table>");
            s.Append("</div>");
            return "" + s;
        }


        public async Task<ActionResult> GetAllRss(string AllNewsPaper)
        {
            string[] newspaper = AllNewsPaper.Split(',');           
            int manv = int.Parse(CookieCls.GetMaNV());
            for (int i = 1; i < newspaper.Length; i++)
            {
                Conn.UpdateRowData("insert into mProcess(MaNV,NewspaperId,DateUpdate) values(" + manv + "," + int.Parse(newspaper[i]) + ",getdate())");

                int newspaperMenuId = int.Parse(newspaper[i]);
                var region = from a in db.mNewspapers
                             where a.NewspaperId == newspaperMenuId
                             select new { a.Languge };
                var listNewspaperMenu = from a in db.mNewspaperMenus
                                        where a.NewspaperId == newspaperMenuId && a.Display == true
                                        select new { a.rssLink, a.NewspaperMenuId };
                if (region.Count() > 0)
                {
                    if (listNewspaperMenu.Count() > 0)
                    {
                        switch (region.SingleOrDefault().Languge)
                        {
                            case "Vie":
                                foreach (var item in listNewspaperMenu)
                                {                                    
                                    await Task.Run(() => rssVie.GetRSS(item.rssLink, item.NewspaperMenuId, manv));
                                }
                                break;
                            default: break;
                        }
                    }
                }
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetTimer()
        {
            StringBuilder s = new StringBuilder();           
            int async = ClsData.countRequest - (ClsData.countRequestSuccess + ClsData.countRequestFail + ClsData.countRequestDup);

            s.Append("<strong id='msgArray' style='color: white; font-weight: bold; font-size: 20px'>" + ClsData.countRequestSuccess + " (R=" + ClsData.countRequest + " S=" + ClsData.countRequestSuccess + " D=" + ClsData.countRequestDup + " F=" + ClsData.countRequestFail + " <strong style='color:Red'>W=" + async + "</strong>)</strong>");
            string shtml = "" + s;
            return Json(new { success = true, async, shtml }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SyncData()
        {
            StringBuilder s = new StringBuilder();
            if (ClsData.btnSync == "")
            {
                ClsData.btnSync = Conn.getInfo("select HoTen from NhanVien where MaNV=" + int.Parse(CookieCls.GetMaNV()) + "");
              
                if (ClsData.listRequest.Count > 0)
                {
                    string insertdata = "";
                    for (int i = 0; i < ClsData.listRequest.Count; i++)
                    {
                        insertdata += "insert into rssNews(rssID,NewspaperMenuId,Title,IconRss,IconSave,Link,[Description],Summary,rssDate,DateInput,Effect,MaNV) values(" + string.Join(",", ClsData.listRequest[i]) + "," + CookieCls.GetMaNV() + ")";
                    }
                    Conn.InsertBatch(insertdata);
                }
                ClsData.ClearArray();

                db.mProcesses.RemoveRange(db.mProcesses);
                db.SaveChanges();
                //Conn.UpdateRowData("delete from mProcess");
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ResetData()
        {
            StringBuilder s = new StringBuilder();
            ClsData.ClearArray();
            //Conn.UpdateRowData("delete from mProcess");            
            db.mProcesses.RemoveRange(db.mProcesses);
            db.SaveChanges();            
            int async = ClsData.countRequest - (ClsData.countRequestSuccess + ClsData.countRequestFail + ClsData.countRequestDup);
            s.Append("<strong id='msgArray' style='color: white; font-weight: bold; font-size: 20px'>" + ClsData.countRequestSuccess + " (R=" + ClsData.countRequest + " S=" + ClsData.countRequestSuccess + " D=" + ClsData.countRequestDup + " F=" + ClsData.countRequestFail + " <strong style='color:Red'>W=" + async + "</strong>)</strong>");
            string shtml = "" + s;
            return Json(new { success = true, async, shtml }, JsonRequestBehavior.AllowGet);
           
        }
    }
}