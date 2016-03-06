using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DotNetUtils
{
    public static class Utils
    {
        /*
         * list-related helpers
         */
        public static bool IsNullOrEmpty(this string str) { return str == null || str.Length <= 0; }
        public static bool IsNullOrEmpty<T>(this List<T> list) { return list == null || list.Count <= 0; }
        public static bool IsNullOrEmpty<T,K>(this Dictionary<T,K> dic) { return dic == null || dic.Count <= 0; }
        public static bool IsNullOrEmpty(this object[] arr) { return arr == null || arr.Length <= 0; }

        /*
         * web related helpers
         */

        /// <summary>
        /// Helper that returns true if the given request accepts json back,
        /// false otherwise.
        /// </summary>
        public static bool AcceptsJSON(this HttpRequestBase request)
        {
            if (request.AcceptTypes.IsNullOrEmpty()) return false;
            foreach(string type in request.AcceptTypes)
            {
                if (type == "application/json" || type == "APPLICATION/JSON")
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Helper that returns true if the given request is for the homepage,
        /// false otherwise (simply based on the request path.)
        /// </summary>
        public static bool IsForHompage(this HttpRequestBase request)
        {
            return request.Path != null && (request.Path == "" || request.Path == "/");
        }
    }
}
