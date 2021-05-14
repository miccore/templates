using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamarinTemplateApp.Helpers.interceptor
{
        public class AuthenticationHandler : DelegatingHandler
        {
            // Constructors and other code here.
            protected async override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
            {
                // Process the HttpRequestMessage object here.
                Debug.WriteLine("Processing request in AuthenticationHandler");

                // Read the Bearer Token
                bool authNeeded = request.RequestUri.ToString().Contains(AppSetting.LoginSocialUrl)
                    || request.RequestUri.ToString().Contains(AppSetting.LoginUrl)
                    || request.RequestUri.ToString().Contains(AppSetting.RegisterPhoneUrl);

                if (!authNeeded)
                {
                    var bToken = Preferences.Get(AppSetting.Token, string.Empty);
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bToken);
                }

                // Once processing is done, call DelegatingHandler.SendAsync to pass it on the 
                // inner handler.
                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

                // Process the incoming HttpResponseMessage object here.
                Debug.WriteLine("Processing response in AuthenticationHandler");

                return response;
            }
        }
}
