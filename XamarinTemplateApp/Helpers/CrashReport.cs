using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace XamarinTemplateApp.Helpers
{
    public static class CrashReport
    {
        /// <summary>
        /// Envoiyer le Email avec le Rapport de Crash
        /// </summary>
        /// <param name="_path"></param>
        public static async Task SendCrashReportAsync(string _path)
        {
            //var confirmResult = await ConfirmDialog.instance.ShowConfirmDialogAsync(
            //    AppResource.Title_CrashReport,
            //    AppResource.Confirm_SendCrashReport,
            //    AppResource.Validate_Send,
            //    AppResource.Validate_Cancel,
            //    TypeConfirmBox.warning);

            //if (confirmResult)
            //{
            //    var message = new EmailMessage("O VAWI Crash Report",
            //        "An error occurred and the application closed by raising an unmanaged exception.",
            //        "bugsreports@tontine.plus");
            //    message.Attachments.Add(new EmailAttachment(_path));
            //    await Email.ComposeAsync(message);

            //    if (File.Exists(_path))
            //        File.Delete(_path);
            //}
        }

    }
}
