using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetUtils
{
    public static class Utils
    {
        public static bool IsNullOrEmpty(this string str) { return str == null || str.Length <= 0; }
        public static bool IsNullOrEmpty<T>(this List<T> list) { return list == null || list.Count <= 0; }
        public static bool IsNullOrEmpty<T,K>(this Dictionary<T,K> dic) { return dic == null || dic.Count <= 0; }
        public static bool IsNullOrEmpty(this object[] arr) { return arr == null || arr.Length <= 0; }
    }
}
