using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MvcExpressAdmin.MyClass
{
    public class MyTag
    {
        public static string RemoveIllegalCharacters(object input)
        {
            // cast the input to a string
            string data = input.ToString();

            // replace illegal characters in XML documents with their entity references
            data = data.Replace("&", "&amp;");
            data = data.Replace("\"", "&quot;");
            data = data.Replace("'", "&apos;");
            data = data.Replace("<", "&lt;");
            data = data.Replace(">", "&gt;");
            return data;
        }
        public static string CvtChar(object input)
        {
            // cast the input to a string
            string data = input.ToString();

            // replace illegal characters in XML documents with their entity references
            data = data.Replace("&amp;", "&");
            data = data.Replace("&quot;", "'");
            data = data.Replace("&apos;", "'");
            data = data.Replace("&lt;", "<");
            data = data.Replace("&gt;", ">");
            data = data.Replace("&#34;", "'");
            data = data.Replace("&#8217;", "’");
            data = data.Replace("&#160;", "");

            data = data.Replace("&agrave;", "à");
            data = data.Replace("&aacute;", "á");
            data = data.Replace("&acirc;", "â");
            data = data.Replace("&atilde;", "ã");
            data = data.Replace("&egrave;", "è");
            data = data.Replace("&eacute;", "é");
            data = data.Replace("&ecirc;", "ê");
            data = data.Replace("&igrave;", "ì");
            data = data.Replace("&iacute;", "í");
            data = data.Replace("&iuml;", "ĩ");
            data = data.Replace("&ograve;", "ò");
            data = data.Replace("&oacute;", "ó");
            data = data.Replace("&ocirc;", "ô");
            data = data.Replace("&otilde;", "õ");
            data = data.Replace("&ugrave;", "ù");
            data = data.Replace("&uacute;", "ú");
            data = data.Replace("&yacute;", "ý");


            data = data.Replace("&#224;", "à");
            data = data.Replace("&#225;", "á");
            data = data.Replace("&#226;", "â");
            data = data.Replace("&#227;", "ã");
            data = data.Replace("&#232;", "è");
            data = data.Replace("&#233;", "é");
            data = data.Replace("&#234;", "ê");
            data = data.Replace("&#236;", "ì");
            data = data.Replace("&#237;", "í");
            data = data.Replace("&#239;", "ĩ");
            data = data.Replace("&#242;", "ò");
            data = data.Replace("&#243;", "ó");
            data = data.Replace("&#244;", "ô");
            data = data.Replace("&#245;", "õ");
            data = data.Replace("&#249;", "ù");
            data = data.Replace("&#250;", "ú");
            data = data.Replace("&#253;", "ý");

            data = data.Replace("&#192;", "À");
            data = data.Replace("&#193;", "Á");
            data = data.Replace("&#194;", "Â");
            data = data.Replace("&#195;", "Ã");
            data = data.Replace("&#200;", "È");
            data = data.Replace("&#201;", "É");
            data = data.Replace("&#202;", "Ê");
            data = data.Replace("&#204;", "Ì");
            data = data.Replace("&#205;", "Í");
            data = data.Replace("&#207;", "Ĩ");
            data = data.Replace("&#208;", "Đ");
            data = data.Replace("&#210;", "Ò");
            data = data.Replace("&#211;", "Ó");
            data = data.Replace("&#212;", "Ô");
            data = data.Replace("&#213;", "Õ");
            data = data.Replace("&#217;", "Ù");
            data = data.Replace("&#218;", "Ú");
            data = data.Replace("&#221;", "Ý");

            data = data.Replace("&#39;", "'");
            data = data.Replace("&lsquo;", "‘");
            data = data.Replace("&rsquo;", "’");
            data = data.Replace("&sbquo;", "‚");
            data = data.Replace("&ldquo;", "“");

            data = data.Replace("&laquo;", "«");
            data = data.Replace("&raquo;", "»");

            data = data.Replace("&#039;", "'");
            data = data.Replace("&nbsp;", " ");

            data = data.Replace("&#44;", ",");
            data = data.Replace("&#45;", "-");
            data = data.Replace("&#46;", ".");
            data = data.Replace("&#47;", "/");
            data = data.Replace("&#58;", ":");
            data = data.Replace("&#59;", ";");

            data = data.Replace("&#40;", "(");
            data = data.Replace("&#41;", ")");

            data = data.Replace("'", "\"");
            data = data.Replace("\r", "");
            data = data.Replace("\n", "");
            data = data.Replace("\t", "");
            data = data.Replace("\"", "");
            data = data.Replace("'", "’");


            return data;
        }

        public static string RemoveHtml(string html)
        {
            return Regex.Replace(html, @"<[^>]*(>|$)|&nbsp;|&zwnj;|&raquo;|&laquo;", string.Empty).Trim();
        }
        public static string GetImg(string html)
        {
            string link = "";
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            if (doc.DocumentNode.SelectSingleNode("//img") != null)
            {
                link = doc.DocumentNode.SelectSingleNode("//img").Attributes["src"].Value;
            }

            return link;
        }
        public static string RemoveTagA(string html)
        {
            return Regex.Replace(html, @"</?(a|A).*?>", string.Empty).Trim();
        }
        public static string Between(string STR, string FirstString, string LastString)
        {
            try
            {
                string FinalString;
                int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
                int Pos2 = STR.IndexOf(LastString);
                FinalString = STR.Substring(Pos1, Pos2 - Pos1);
                return FinalString;
            }
            catch { return ""; }
        }
    }
}