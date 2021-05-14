using System;
using System.Collections.Generic;
using System.Text;
using XamarinTemplateApp.Helpers.interceptor;

namespace XamarinTemplateApp.Helpers
{
    public static class AppSetting
    {
        /// <summary>
        /// L'Intercepteur de requete HTTP
        /// </summary>
        public static HttpInterceptor httpInterceptor = new HttpInterceptor();

        public static HttpRequestManager restApiMgr = new HttpRequestManager();
        internal static string serverOkResponse;

        public static string RefreshTokenUrl { get; internal set; }
        public static string ExpireDateToken { get; internal set; }
        public static string Token { get; internal set; }
        public static string AccessToken { get; internal set; }
        public static bool IsValidToken { get; internal set; }
        public static string LoginSocialUrl { get; internal set; }
        public static string LoginUrl { get; internal set; }
        public static string RegisterPhoneUrl { get; internal set; }
    }

}
