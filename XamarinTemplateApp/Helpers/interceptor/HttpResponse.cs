using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinTemplateApp.Helpers.interceptor
{
    public class HttpResponse
    {
        public string status { get; set; }

        public JObject data { get; set; }

        public JArray datas { get; set; }

        public HttpResponse(string _json)
        {
            //JObject obj = JObject.Parse(json);
            JObject obj = JsonConvert.DeserializeObject<JObject>(_json);
            status = obj.Value<string>("status");

            object dataObj = obj.Value<object>("data");

            if (dataObj.GetType() == typeof(JObject))
            {
                data = obj.Value<JObject>("data");
            }
            else if (dataObj.GetType() == typeof(JArray))
            {
                datas = obj.Value<JArray>("data");
            }
        }

        public bool IsOK()
        {
            return status == AppSetting.serverOkResponse;
        }

        public async Task GetErrorMessageAsync()
        {
            //await ConfirmDialog.instance.ShowConfirmDialogAsync(AppResource.Title_Error, getErrorText(), AppResource.Validate_OK, string.Empty, TypeConfirmBox.warning);
        }

        public int getErrorCode()
        {
            int errorCode = 0;

            if (!IsOK())
            {
                errorCode = (int)data["errNo"];
            }
            return errorCode;
        }

        public string getErrorText()
        {
            if (!IsOK())
            {
                return (string)data["errMsg"];
            }
            return "Unknown Error";
        }
    }

}
