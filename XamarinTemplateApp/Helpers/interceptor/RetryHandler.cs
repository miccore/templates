using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamarinTemplateApp.Helpers.interceptor
{
    public class RetryHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    // Process the HttpRequestMessage object here.
                    Debug.WriteLine("Processing request in RetryHandler");

                    // Clear the header
                    AppSetting.restApiMgr.client.DefaultRequestHeaders.Accept.Clear();

                    // base.SendAsync calls the inner handler
                    var response = await base.SendAsync(request, cancellationToken);

                    if (response.IsSuccessStatusCode)
                        return response;

                    var repond = response.Content;

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        // Token is probably expired
                        var accessToken = Preferences.Get(AppSetting.AccessToken, string.Empty);
                        if (string.IsNullOrWhiteSpace(accessToken))
                            return response;

                        // Send the refresh Token request
                        var refresh = await RefreshToken(accessToken);

                        if (refresh.IsSuccessStatusCode)
                            continue;
                        else
                        {
                            await Task.Delay(1000, cancellationToken);
                            return refresh;   // retourne ca si on echoue le token refresh
                        }
                    }
                    else if (response.StatusCode == (HttpStatusCode)429)
                    {
                        // 429 Too many requests
                        // Wait a bit and try again later
                        await Task.Delay(1000, cancellationToken);
                        continue;
                    }
                    else
                        return response;
                }
                catch (Exception ex) when (IsNetworkError(ex))
                {
                    // Network error
                    // Wait a bit and try again later
                    await Task.Delay(2000, cancellationToken);
                    continue;
                }
            }
        }

        private static bool IsNetworkError(Exception ex)
        {
            // Check if it's a network error
            if (ex is SocketException)
                return true;
            if (ex.InnerException != null)
                return IsNetworkError(ex.InnerException);
            return false;
        }

        public async Task<HttpResponseMessage> RefreshToken(string _accessToken)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("refresh_token",_accessToken)
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, AppSetting.RefreshTokenUrl);
            request.Content = new FormUrlEncodedContent(keyValues);

            var response = await AppSetting.restApiMgr.client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JObject jwtDynamic = JsonConvert.DeserializeObject<JObject>(content);

                var bToken = jwtDynamic.Value<JObject>("data").Value<string>("bearerData");
                var aToken = jwtDynamic.Value<JObject>("data").Value<string>("refresh_token");
                var expireIn = jwtDynamic.Value<JObject>("data").Value<int>("expires_in");
                //var userData = jwtDynamic.Value<JObject>("data").Value<JObject>("user");

                DateTime expTime = DateTime.Now.AddSeconds(expireIn);

                Preferences.Set(AppSetting.ExpireDateToken, expTime.ToString());
                Preferences.Set(AppSetting.Token, bToken);
                Preferences.Set(AppSetting.AccessToken, aToken);
                AppSetting.IsValidToken = true;
            }

            return response;
        }
    }

}
