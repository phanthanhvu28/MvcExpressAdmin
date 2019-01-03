using MvcExpressAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcExpressAdmin.Controllers
{
    public class PartialController : Controller
    {
        NewsDataContext db = new NewsDataContext();
        // GET: Partial
        public string Languge()
        {
            StringBuilder s = new StringBuilder();

            var lg = (from a in db.NgonNgus
                      where a.LShow == true
                      orderby a.ThuTu
                      select new { a.LID, a.LName });
            if (lg.Count() > 0)
            {
                foreach (var row in lg)
                {
                    s.Append("<option value='" + row.LID + "'>" + row.LName + "</option>");
                }
            }
            return "" + s;
        }
       
    }
}