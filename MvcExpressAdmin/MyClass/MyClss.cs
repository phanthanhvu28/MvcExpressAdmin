using MvcExpressAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcExpressAdmin.MyClass
{
    public class MyClss
    {       
        public static string Encode(string input)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[input.Length];
            encode = Encoding.UTF8.GetBytes(input);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        public static string Decode(string inputEncode)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(inputEncode);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        public static DateTime CVDate(object value)
        {
            try
            {
                if (value != null)
                {
                    DateTime num = DateTime.Parse("" + value);
                    return num;
                }
            }
            catch
            {
            }
            return Convert.ToDateTime("01/01/1900");
        }
    }
}