using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace XamarinTemplateApp.Helpers.interceptor
{
    public class HttpRequestManager
    {
        /// <summary>
        /// Le Client Http pour les requetes vers API
        /// </summary>
        public System.Net.Http.HttpClient client = null;

        public HttpRequestManager()
        {
            var pipeline = new RetryHandler()
            {
                InnerHandler = new AuthenticationHandler()
                {
                    InnerHandler = new HttpClientHandler()
                }
            };

            client = new HttpClient(pipeline);
        }
    }

}
