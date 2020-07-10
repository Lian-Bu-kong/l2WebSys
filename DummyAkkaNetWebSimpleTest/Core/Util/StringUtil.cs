using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Util
{
    /**
 * Author : ICSC 余士鵬
 * Desc : MMS Snd Msg Model
 * **/
    public static class StringUtil
    {
        // 根據固定長度做分切
        public static IEnumerable<string> SplitBySpecificLength(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
        // 字串中刪除無效的字元
        public static string CleanInvalidChar(this string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return string.Empty;
            }
        }
        // 型態轉換
        public static T? ToNullable<T>(this string s) where T : struct
        {
            T? result = new T?();
            try
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    var conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);
                }
            }
            catch { }
            return result != null ? result : default(T);
        }
        public static T? ToNullable<T>(this char[] s) where T : struct
        {
            var str = new string(s);

            T? result = new T?();
            try
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    var conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(str);
                }
            }
            catch { }
            return result != null ? result : default(T);
        }
        public static char[] ToNChar(this string data, int totalWidth)
        {
            try
            {
                return data.PadLeft(totalWidth, '0').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.CleanInvalidChar());
                return "".PadLeft(totalWidth, '0').ToArray(); ;
            }
        }
        public static char[] ToNChar(this float data, int totalWidth)
        {
            try
            {
                return data.ToString().PadLeft(totalWidth, '0').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.CleanInvalidChar());
                return "".PadLeft(totalWidth, '0').ToArray(); ;
            }
        }
        public static char[] ToNChar(this int data, int totalWidth)
        {
            try
            {
                return data.ToString().PadLeft(totalWidth, '0').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.CleanInvalidChar());
                return "".PadLeft(totalWidth, '0').ToArray(); ;
            }
        }
        public static char[] ToCChar(this string data, int totalWidth)
        {
            try
            {
                return data.PadRight(totalWidth, ' ').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.CleanInvalidChar());
                return "".PadRight(totalWidth, ' ').ToArray(); ;
            }
        }
        public static string ToStr(this char[] data)
        {
            return new string(data);
        }
    }
}
