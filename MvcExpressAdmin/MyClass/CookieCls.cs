using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MvcExpressAdmin.MyClass
{
    public class CookieCls
    {
        public static string GetUser()
        {
            try
            {
                string ck = HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["CKUserLogin"]].Value;
                if (ck == null)
                {
                    ck = "";
                }
                return ck;
            }
            catch
            {
                return "";
            }
        }
        public static string GetMaNV()
        {
            try
            {

                if (HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["CKMaNV"]].Value != null)
                {
                    string ck = HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["CKMaNV"]].Value;
                    if (ck == null)
                    {
                        ck = "";
                    }
                    return ck;
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        public static string GetFullname()
        {
            try
            {
                string ck = HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["CKHoTen"]].Value;
                if (ck == null)
                {
                    ck = "";
                }
                return ck;
            }
            catch
            {
                return "";
            }
        }
        public static string GetLanguge()
        {
            try
            {
                string ck = HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["CKLang"]].Value;
                if (ck == null)
                {
                    ck = "";
                }
                return ck;
            }
            catch
            {
                return "";
            }
        }
        public static string GetDerpartment()
        {
            try
            {
                string ck = HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["CKPhongBan"]].Value;
                if (ck == null)
                {
                    ck = "";
                }
                return ck;
            }
            catch
            {
                return "";
            }
        }
    }
}