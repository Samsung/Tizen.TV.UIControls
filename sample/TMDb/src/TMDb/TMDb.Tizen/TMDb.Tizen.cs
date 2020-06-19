using System;
using System.Threading.Tasks;
using Tizen.Security;
using Xamarin.Forms;

namespace TMDb
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Tizen.TV.UIControls.Forms.UIControls.MainWindowProvider = () => MainWindow;
            LoadApplication(new App());
            RequestPermission();
        }

        static Task RequestPermission()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            var response = PrivacyPrivilegeManager.GetResponseContext("http://tizen.org/privilege/mediastorage");
            PrivacyPrivilegeManager.ResponseContext target;
            response.TryGetTarget(out target);
            target.ResponseFetched += (s, e) =>
            {
                tcs.SetResult(true);
            };
            PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/mediastorage");

            return tcs.Task;
        }

        static void Main(string[] args)
        {
            var app = new Program();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(app);
            Tizen.TV.UIControls.Forms.UIControls.Init();
            Forms.Init(app);
            app.Run(args);
        }
    }
}
