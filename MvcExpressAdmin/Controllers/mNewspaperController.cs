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
                s.Append("<div style='margin: 0 auto;overflow: auto;width: 100%;position: relative;height:" + (AS.sHeight() - 190) + "px'>");
                s.Append("<table width='100%' class='mytable' id='listMenu'>");
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
                        s.Append("<td class='heading' style='width:120px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' colspan='2' id='tname1_" + r.NewspaperId + "'><input class='txtn' type='text' id='txtMenu1" + r.NewspaperId + "' value='" + r.Title + "' onchange='Update_Name_Menu1(" + r.NewspaperId + ")' style='width:100%;'></td>");
                        s.Append("<td class='heading' style='width:160px;background-color:#b5ebf8;font-size:17px;font-weight:bold;text-align:left' id='tdlinkweb" + r.NewspaperId + "'><input class='txtn' type='text' id='txtLinkwebsite" + r.NewspaperId + "' value='" + r.Website + "' onchange='Update_Link_Website(" + r.NewspaperId + ")' style='width:100%;'></td>");
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
                                  select new {a.NewspaperMenuId,a.NewspaperId,a.Title,a.Display,a.Stt,a.rssLink,a.TopMenu,a.GroupApp,a.Video };
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


                                s.Append("<td class='heading'  style='width:80px; text-align:center'  id='n" + r2.NewspaperMenuId + "'><input class='txtn' type='text' id='txtNameCate" + r2.NewspaperMenuId + "' value='" + r2.Title + "' onchange='UpdateNameCate(" + r2.NewspaperMenuId + ")' style='width:100%'></td>");

                                s.Append("<td class='heading'  style='width:160px; text-align:center'  id='rss" + r2.NewspaperMenuId + "'><input class='txtn' type='text' id='txtRss" + r2.NewspaperMenuId + "' value='" + r2.rssLink + "' onchange='UpdateRssLink(" + r2.NewspaperMenuId + ")' style='width:100%'></td>");

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
    }
}