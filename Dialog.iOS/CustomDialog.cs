using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Dialog.iOS.CustomDialog))]

namespace Dialog.iOS
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 参考サイト：
    /// https://mt312.com/1050
    /// </remarks>
    public class CustomDialog : ICustomDialogService
    {
        public Task<CustomAlertResult> Show(string title, string message, string accepte)
        {
            Debug.WriteLine("Show");

            var tcs = new TaskCompletionSource<CustomAlertResult>();

            UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            alert.AddAction(UIAlertAction.Create(accepte, UIAlertActionStyle.Default, x =>
            {
                tcs.SetResult(new CustomAlertResult { PressedButtonTitle = accepte, Text = "" });
            }));

            UIImage image = UIImage.FromFile("ErrorImage.png");
            image.ImageWithRenderingMode(UIImageRenderingMode.Automatic);
            alert.SetValueForKey(image, new NSString("image"));

            //UIAlertAction imageAction = UIAlertAction.Create(title, UIAlertActionStyle.Default, null);
            //NSString key = new Foundation.NSString("image");
            //imageAction.SetValueForKey(image, key);
            //alert.AddAction(imageAction);

            //alert.SetValueForKey(image, new NSString("image"));

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);

            Debug.WriteLine("Show End");

            return tcs.Task;
        }

        /// <summary>
        /// 画像付きアラートダイアログを表示する。
        /// </summary>
        /// <param name="title">アラートのタイトル文字列</param>
        /// <param name="message">アラートのメッセージ文字列</param>
        /// <param name="accept">受諾ボタンに表示する文字列</param>
        /// <param name="cancel">中止ボタンに表示する文字列</param>
        /// <returns></returns>
        public Task<CustomAlertResult> ShowImageContent(string title, string message, string accept, string cancel = null)
        {
            Debug.WriteLine("ShowImageContent");

            // ダイアログでどのボタンが選択されたか。
            var result = new TaskCompletionSource<CustomAlertResult>();

            // 引数で指定したタイトルとメッセージのアラートダイアログを生成する。
            UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            // アラートダイアログのメッセージとボタンの間に画像を表示する。
            ErrorImageViewController view = new ErrorImageViewController();
            alert.SetValueForKey(view, new NSString("contentViewController"));

            // 受諾ボタンを作成する。
            alert.AddAction(UIAlertAction.Create(accept, UIAlertActionStyle.Default, x =>
            {
                result.SetResult(new CustomAlertResult { PressedButtonTitle = accept, Text = "" });
            }));

            // 中止ボタンに表示するメッセージが指定されていれば中止ボタンを作成する。
            if (cancel != null)
            {
                alert.AddAction(UIAlertAction.Create(cancel, UIAlertActionStyle.Default, x =>
                {
                    result.SetResult(new CustomAlertResult { PressedButtonTitle = cancel, Text = "" });
                }));
            }

            // アラートダイアログを表示する。
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);

            Debug.WriteLine("ShowImageContent End");
            return result.Task;
        }
    }
}
