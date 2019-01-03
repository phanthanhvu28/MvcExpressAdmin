using MvcExpressAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcExpressAdmin.MyClass
{
    public class ClsData
    {
        public static int countRequest = 0;
        public static int countRequestSuccess = 0;
        public static int countRequestFail = 0;
        public static int countRequestDup = 0;
        public static string btnSync = "";
        public static List<string[]> listRequest = new List<string[]>();
        static NewsDataContext db = new NewsDataContext();

        public static bool CheckNewsSync(string Link, int NewspaperMenuId)
        {            

            var v = from a in db.rssNews
                    where a.Link.Equals(Link) && a.NewspaperMenuId == NewspaperMenuId
                    select a;
            return v.Count() == 0 ? true : false;
        }
        public static string RssIDSync()
        {
            //string kequa = Chuoi + "01";
            //var v = (from a in db.rssNews
            //         where a.DateInput.Value.Date.Equals(string.Format("{0:MM-dd-yyyy}", DateTime.Now))
            //         orderby a.DateInput descending
            //         select new { a.rssID }).First();
           
            //int number = int.Parse(v.ToString().Split('.')[1]);
            //if (v.rssID != "")
            //{

            //}
            return Conn.CreateCode(string.Format("{0:ddMMyyyy}", Conn.CVDate(string.Format("{0:MM-dd-yyyy}", DateTime.Now))) + ".", "rssNews", "rssID");
        }
        public static bool InsertNewsSync(string rssID, int NewspaperMenuId, string Title, string IconRss, string IconSave, string Link, string Description, string Summary, string rssDate)
        {
            string[] array = { NewspaperMenuId.ToString(), Link };
            if (!CheckforDuplicates(array))
            {
                listRequest.Add(new string[] { "(select dbo.[fn_CREATE_ID_AUTO]())", NewspaperMenuId.ToString(), "N'" + MyTag.CvtChar(Title) + "'", "'" + IconRss + "'", "'" + IconSave + "'", "'" + Link + "'", "N'" + MyTag.CvtChar(Description) + "'", "N'" + MyTag.CvtChar(Summary) + "'", "N'" + rssDate + "'", "getdate()", "1" });
                return true;
            }
            return false;

        }
        public static bool CheckforDuplicates(string[] array)
        {
            var duplicates = array
             .GroupBy(p => p)
             .Where(g => g.Count() > 1)
             .Select(g => g.Key);
            return (duplicates.Count() > 0);
        }
        public static void ClearArray()
        {
            listRequest.Clear();
            countRequest = 0;
            countRequestSuccess = 0;
            countRequestFail = 0;
            countRequestDup = 0;
            btnSync = "";
        }
    }
}