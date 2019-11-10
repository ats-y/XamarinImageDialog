using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Dialog.iOS.CustomDialog))]

namespace Dialog.iOS
{
    public class CustomDialog : ICustomDialogService
    {
        public Task<CustomAlertResult> Show(string title, string message, string accepte, string cancel)
        {
            Debug.WriteLine("Show");

            var tcs = new TaskCompletionSource<CustomAlertResult>();

            UIAlertController alert = UIAlertController.Create("たいとる", "めっせーじ", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("でふぉ", UIAlertActionStyle.Default, x=>
            {
                tcs.SetResult(new CustomAlertResult { PressedButtonTitle = "でふぉ", Text = "" });
            }));

            UIImage image = UIImage.FromFile("ErrorImage.png");
            image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            UIAlertAction imageAction = UIAlertAction.Create("imangeAction", UIAlertActionStyle.Default, null);
            NSString key = new Foundation.NSString("image");
            imageAction.SetValueForKey(image, key);
            alert.AddAction(imageAction);

            alert.SetValueForKey(image, new NSString("image") );

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);

            Debug.WriteLine("Show End");

            return tcs.Task;
        }

        public Task<CustomAlertResult> Show3()
        {
            Debug.WriteLine("Show");

            var tcs = new TaskCompletionSource<CustomAlertResult>();

            UIAlertController alert = UIAlertController.Create("照合エラー", $"ああああああああああ" +
                Environment.NewLine +
                $"いいいいいいいいいいううううううううううええええええええええおおおおおおおおおおお", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("でふぉ", UIAlertActionStyle.Default, x =>
            {
                tcs.SetResult(new CustomAlertResult { PressedButtonTitle = "でふぉ", Text = "" });
            }));

            CustomAlertSubtitleViewController view = new CustomAlertSubtitleViewController();
            alert.SetValueForKey(view, new NSString("contentViewController"));

            //var navController = new UINavigationController(view);
            //UIApplication.SharedApplication.KeyWindow.RootViewController = navController;

            // アラートダイアログを表示する。
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);

            Debug.WriteLine("Show End");

            return tcs.Task;
        }

    }
}
