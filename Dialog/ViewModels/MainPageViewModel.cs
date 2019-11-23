using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Reactive.Bindings;
using Xamarin.Forms;

namespace Dialog.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        /// <summary>
        /// 画像付きダイアログ表示コマンド
        /// </summary>
        public AsyncReactiveCommand ShowDialogCommand { get; set; }

        public MainPageViewModel()
        {
            ShowDialogCommand = new AsyncReactiveCommand();
            ShowDialogCommand.Subscribe(async _ =>
              {
                  Debug.WriteLine("ShowDialogCommand");

                  string msg = "あいうえおかきくけこさしすせそたちつてと" + Environment.NewLine
                  + "2" + Environment.NewLine
                  + "3" + Environment.NewLine
                  + "4" + Environment.NewLine
                  + "5" + Environment.NewLine
                  + "6" + Environment.NewLine
                  + "7" + Environment.NewLine
                  + "8" + Environment.NewLine
                  + "9" + Environment.NewLine
                  + "10" + Environment.NewLine;

                  var result = await DependencyService.Get<ICustomDialogService>()
                    .Show("照合エラー", msg, EImageKind.Error,
                            new List<string>() { "受諾1", "受諾2", "受諾3" },
                            new List<string>() { "破棄1", "破棄2", "破棄3" },
                            "きゃんせる");
                  Debug.WriteLine($"ShowDialogCommand dialog result = {result.PressedButtonTitle}");
              });
        }
    }
}
