using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Dialog.iOS.CustomDialog.CustomDialog))]

namespace Dialog.iOS.CustomDialog
{
    /// <summary>
    /// カスタムダイアログ。
    /// </summary>
    /// <remarks>
    /// 参考サイト：
    /// https://mt312.com/1050
    /// </remarks>
    public class CustomDialog : ICustomDialogService
    {
        /// <summary>
        /// 画像付きアラートダイアログを表示する。
        /// </summary>
        /// <param name="title">アラートのタイトル文字列</param>
        /// <param name="message">アラートのメッセージ文字列</param>
        /// <param name="imageKind">アラートに表示する画像種</param>
        /// <param name="accepts">受諾ボタンに表示する文字列リスト。このリスト分ボタン受諾ボタンを表示する。</param>
        /// <param name="destructives">破棄ボタンに表示する文字列。このリスト分ボタン破棄ボタンを表示する。</param>
        /// <param name="cancel">中止ボタンに表示する文字列。nullの場合は中止ボタンは表示しない。</param>
        /// <returns>アラートダイアログでボタンをタップしたら結果を返すTask。</returns>
        public Task<CustomAlertResult> Show(string title, string message, EImageKind imageKind
            , List<string> accepts, List<string> destructives = null, string cancel = null)
        {
            Debug.WriteLine($"start {MethodBase.GetCurrentMethod().Name}()");

            // 引数で指定したタイトルとメッセージのアラートダイアログを生成する。
            UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            // アラートダイアログのメッセージとボタンの間に画像を表示する。
            AlertImageViewController view = new AlertImageViewController(imageKind);
            alert.SetValueForKey(view, new NSString("contentViewController"));

            // ボタンをタップしたら結果を返すタスクを生成する。
            var result = new TaskCompletionSource<CustomAlertResult>();

            // 受諾ボタンを作成する。
            foreach (string accept in accepts ?? new List<string>())
            {
                alert.AddAction(UIAlertAction.Create(accept, UIAlertActionStyle.Default, x =>
                {
                    // ボタンをタップしたらタスクを完了させ、結果を返す。
                    result.TrySetResult(
                        new CustomAlertResult
                        {
                            IsCancel = false,
                            PressedButtonTitle = accept,
                        });
                }));
            }

            // 破棄ボタンを作成する。
            foreach (string destructive in destructives ?? new List<string>())
            {
                alert.AddAction(UIAlertAction.Create(destructive, UIAlertActionStyle.Destructive, x =>
                {
                    // ボタンをタップしたらタスクを完了させ、結果を返す。
                    result.TrySetResult(
                        new CustomAlertResult
                        {
                            IsCancel = false,
                            PressedButtonTitle = destructive,
                        });
                }));
            }

            // 中止ボタンを作成する。
            if (cancel != null)
            {
                alert.AddAction(UIAlertAction.Create(cancel, UIAlertActionStyle.Cancel, x =>
                {
                    // ボタンをタップしたらタスクを完了させ、結果を返す。
                    result.TrySetResult(
                        new CustomAlertResult
                        {
                            IsCancel = true,
                            PressedButtonTitle = cancel
                        });
                }));
            }

            // アラートダイアログを表示する。
            // ※ダイアログを表示して処理は戻ってくる。
            //  ボタンタップで完了するタスクを返すので、ダイアログの結果はそのタスクで受け取る。
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);

            // ダイアログのボタンタップ結果を返すタスクを返す。
            Debug.WriteLine($"end {MethodBase.GetCurrentMethod().Name}()");
            return result.Task;
        }
    }
}
