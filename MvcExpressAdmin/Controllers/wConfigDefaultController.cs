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
    public class wConfigDefaultController : PerSiteController
    {
        private NewsDataContext db = new NewsDataContext();
        AdminSiteController AS = new AdminSiteController();
        string languge = CookieCls.GetLanguge();

        public string LoadData()
        {
            StringBuilder s = new StringBuilder();
            try
            {
                s.Append("<table width='100%' class='mytable' id='reloadlistmenu'>");
                s.Append("<thead>");
                s.Append("<tr style='height:50px'>");
                s.Append("<td style='width:200px;text-align:left;font-size:18px'>Cấu hình trang chủ website</td>");
                s.Append("<td style='width:200px;text-align:left;font-size:18px'>List menu</td>");
                s.Append("<td style='width:70px;text-align:left;font-size:18px'>P.Right</td>");
                s.Append("<td style='width:70px;text-align:center;font-size:18px'>Upper video</td>");
                s.Append("<td style='width:50px;text-align:center;font-size:18px'>Column</td>");
                s.Append("<td style='width:50px;text-align:center;font-size:18px'>Slider</td>");
                s.Append("<td style='width:50px;text-align:center;font-size:18px'>Tab</td>");

                s.Append("</tr>");

                s.Append("</thead></table>");
                s.Append("<div class='scrollbar' style='overflow-y:scroll;max-height:" + (AS.sHeight() - 220) + "px'>");
                s.Append("<table width='100%' class='mytable' id='dsMenu' style='' >");
                s.Append("<tbody>");

                var dt1 = from a in db.wMenu1
                          select new { a.mID, a.Name, a.Effect, a.Folder, a.STT };
                if(dt1.Count()>0)
                {
                    foreach(var r in dt1)
                    {
                        int ID = r.mID;
                        string FRight = "";
                        string UpperVideo = "";
                        string column = "";
                        string slider = "";
                        string tab = "";
                        var dt2 = (from a in db.wFormats
                                   where a.mID == ID
                                   select new { a.TID, a.FRight, a.UpperVideo }).SingleOrDefault();
                        if (dt2 != null)
                        {

                            FRight = dt2.FRight == true ? " checked=checked" : "";
                            UpperVideo = dt2.UpperVideo == true ? " checked=checked" : "";

                            column = dt2.TID == 1 ? " checked=checked" : "";
                            slider = dt2.TID == 2 ? " checked=checked" : "";
                            tab = dt2.TID == 3 ? " checked=checked" : "";
                        }

                        s.Append("<tr id='tr1_" + r.mID + "'>");

                        s.Append("<td class='heading' style='width:400px;height:50px;background-color:#b5ebf8;font-size:19px;font-weight:bold;' colspan='3'>" + r.Name + "</td>");

                        s.Append("<td style='width:70px; text-align:left;background-color:#b5ebf8'> <label class='lbcheck' style='z - index:1000'><input class='chkhidden' value='" + r.mID + "' type='checkbox' " + FRight + " onclick=\"CheckRight(" + r.mID + ")\" id='chkRight" + r.mID + "'> <span class='text'></span></label></td>");
                        s.Append("<td style='width:70px; text-align:center;background-color:#b5ebf8'> <label class='lbcheck' style='z - index:1000'><input class='chkhidden' value='" + r.mID + "' type='checkbox' " + UpperVideo + " onclick=\"CheckUpperVideo(" + r.mID + ")\" id='chkUpperVideo" + r.mID + "'> <span class='text'></span></label></td>");

                        s.Append("<td style='width:50px; text-align:center;background-color:#b5ebf8'> <label class='lbcheck' style='z - index:1000'><input class='chkhidden' value='" + r.mID + "' type='checkbox' " + column + " onclick=\"SelectFormat(" + r.mID + ",1)\" id='chkColumn" + r.mID + "'> <span class='text'></span></label></td>");
                        s.Append("<td style='width:50px; text-align:center;background-color:#b5ebf8'> <label class='lbcheck' style='z - index:1000'><input class='chkhidden' value='" + r.mID + "' type='checkbox' " + slider + " onclick=\"SelectFormat(" + r.mID + ",2)\" id='chkSlider" + r.mID + "'> <span class='text'></span></label></td>");

                        s.Append("<td style='width:50px; text-align:center;background-color:#b5ebf8'> <label class='lbcheck' style='z - index:1000'><input class='chkhidden' value='" + r.mID + "' type='checkbox' " + tab + " onclick=\"SelectFormat(" + r.mID + ",3)\" id='chkTab" + r.mID + "'> <span class='text'></span></label></td>");

                        s.Append("</tr>");

                        var dt3 = from a in db.wMenu2
                                  where a.Effect == true && a.mID == r.mID
                                  orderby a.STT ascending
                                  select new {a.sID,a.mID,a.Name,a.Effect,a.STT };
                        if(dt3.Count()>0)
                        {
                            foreach(var r2 in dt3)
                            {
                                s.Append("<tr id='submenu" + r2.sID + "'>");

                                s.Append("<td  style='width:40px; Height:40px'  class='heading'  align='center'>" + r2.sID + "</td>");

                                s.Append("<td class='heading'  style='width:160px'>" + r2.Name + "</td>");

                                s.Append("<td style='width:200px; text-align:left;'>" + GetListMenu(r2.sID) + "</td>");

                                s.Append("<td class='heading' style='width:70px; text-align:center'> <input type='button' class='add2' id='sid" + r2.sID + "' onclick=\"LoadNewsPaperMenu('" + r2.sID + "')\"> <input type='button' class='list' id='list" + r2.sID + "' onclick=\"LoadListSelected('" + r2.sID + "')\"></td>");
                                s.Append("<td class='heading' colspan='4'> </td>");


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
        StringBuilder GetListMenu(int sID)
        {
            StringBuilder s = new StringBuilder();           
            var dt = from a in db.mNewspaperMenus
                     from b in db.wNewsMenuIds
                     from c in db.mNewspapers
                     where a.NewspaperMenuId == b.NewspaperMenuId && c.NewspaperId == a.NewspaperId && b.sID == sID
                     orderby a.Stt
                     select new {a.NewspaperMenuId,a.Title };
            if (dt.Count() > 0)
            {
                foreach(var r in dt)
                {
                    s.Append("<div style='margin-left:3px;margin-right: 5px;float:left;width:auto;vertical-align:middle;' class='boxper' align='left' id='cn" + r.NewspaperMenuId + "'>");                    
                    s.Append("<label style='margin: 3px;'><input type='checkbox' checked='checkbox' class='checkbox-slider slider-icon colored-palegreen' id='idSelected2" + r.NewspaperMenuId + "' value='" + r.NewspaperMenuId + "' onclick=\"CheckListSelected2('" + sID + "'," + r.NewspaperMenuId + ")\"> <span class='text' id='lb" + r.NewspaperMenuId + "'></span>");
                    s.Append(" <span style='float:left; margin-right:5px;margin-top:3px;' title='" + r.NewspaperMenuId + "'>" + r.Title + "</span></label></div>");

                }
            }
            return s;
        }

    }
}