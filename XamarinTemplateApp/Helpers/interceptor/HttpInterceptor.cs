using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace XamarinTemplateApp.Helpers.interceptor
{
    public class HttpInterceptor
    {
        /// <summary>
        /// Envoi des Requetes HTTP
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requireAuth"></param>
        /// <returns></returns>
        public async Task<HttpResponse> SendHttpRequest(HttpRequestMessage _request, bool _requireAuth = true, string _contentType = "")
        {
            HttpResponseMessage response = null;

            if (_contentType != "")
                _request.Content.Headers.ContentType = new MediaTypeHeaderValue(_contentType);

            // actual initial request
            response = await AppSetting.restApiMgr.client.SendAsync(_request);
            var json = await response.Content.ReadAsStringAsync();
            HttpResponse parsedResonse = new HttpResponse(json);

            return parsedResonse;
        }
    }

}
