using System;
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
        public ReactiveCommand ShowDialogCommand { get; set; }

        public MainPageViewModel()
        {
            ShowDialogCommand = new ReactiveCommand();
            ShowDialogCommand.Subscribe(async _ =>
              {
                  Debug.WriteLine("ShowDialogCommand");
                  var result = await DependencyService.Get<ICustomDialogService>().ShowImageContent("照合エラー", "この行為は禁止です。", "はい", "やめる");
                  Debug.WriteLine($"ShowDialogCommand dialog result = {result.PressedButtonTitle}");
              });
        }
    }
}
