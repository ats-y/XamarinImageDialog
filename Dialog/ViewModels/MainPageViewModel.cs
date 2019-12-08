using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Dialog.Utility;
using Prism.Services;
using Reactive.Bindings;
//using Xamarin.Forms;

namespace Dialog.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        /// <summary>
        /// 画像付きダイアログ表示コマンド
        /// </summary>
        public AsyncReactiveCommand ShowCostomDialogCommand { get; set; }

        /// <summary>
        /// 照合ダイアログ表示コマンド（全部）
        /// </summary>
        public AsyncReactiveCommand ShowCollationCommand { get; set; }

        /// <summary>
        /// 照合ダイアログ表示コマンド（エラーなし）
        /// </summary>
        public AsyncReactiveCommand ShowNonErrorCommand { get; set; }

        /// <summary>
        /// Prismのダイアログサービス。
        /// </summary>
        IPageDialogService _pageDialogService;

        ICustomDialogService _customDialogService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="pageDialogServece">Prismのダイアログサービス</param>
        public MainPageViewModel(IPageDialogService pageDialogServece, IDependencyService dependencyService)
        {
            _customDialogService = dependencyService.Get<ICustomDialogService>();
            _pageDialogService = pageDialogServece;

            // カスタムダイアログ表示コマンドの購読。
            ShowCostomDialogCommand = new AsyncReactiveCommand();
            ShowCostomDialogCommand.Subscribe(async _ => await OnShowCustomDialogCommandAsync());

            // 照合ダイアログ表示コマンド（全部）の購読。
            ShowCollationCommand = new AsyncReactiveCommand();
            ShowCollationCommand.Subscribe(async _ => await OnShowCollationCommandAsync());

            // 照合ダイアログ表示コマンド（エラーなし）の購読。
            ShowNonErrorCommand = new AsyncReactiveCommand();
            ShowNonErrorCommand.Subscribe(async _ => await OnShowCollationNonErrorCommandAsync());
        }

        /// <summary>
        /// カスタムダイアログを表示する。カスタムダイアログのテスト用。
        /// </summary>
        /// <returns>タスク</returns>
        private async Task OnShowCustomDialogCommandAsync()
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

            var result = await Xamarin.Forms.DependencyService.Get<ICustomDialogService>()
              .Show("照合エラー", msg, EImageKind.Error,
                      new List<string>() { "受諾1", "受諾2", "受諾3" },
                      new List<string>() { "破棄1", "破棄2", "破棄3" },
                      "きゃんせる");

            Debug.WriteLine($"ShowDialogCommand dialog result = {result.PressedButtonTitle}");
        }

        /// <summary>
        /// 照合ダイアログを表示する（警告、エラー、問い合わせ全部）
        /// </summary>
        /// <returns>タスク</returns>
        private async Task OnShowCollationCommandAsync()
        {
            // 照合ダイアログを表示する。
            CollationDialogUtility dialog = new CollationDialogUtility();
            await dialog.ShowCollationDialogAsync(
                new List<string>() { "警告メッセージ1", "警告メッセージ2", "警告メッセージ3" },
                new List<string>() { "エラーメッセージ1", "エラーメッセージ2", "エラーメッセージ3" },
                new List<string>() { "問い合わせメッセージ1", "問い合わせメッセージ2", "問い合わせメッセージ3" });
        }

        /// <summary>
        /// 照合ダイアログを表示する（警告、問い合わせのみ）
        /// </summary>
        /// <returns>タスク</returns>
        private async Task OnShowCollationNonErrorCommandAsync()
        {
            // 照合ダイアログを表示する。
            CollationDialogUtility dialog = new CollationDialogUtility();
            var result = await dialog.ShowCollationDialogAsync(
                new List<string>() { "警告メッセージ1", "警告メッセージ2", "警告メッセージ3" },
                null,
                new List<string>() { "問い合わせメッセージ1", "問い合わせメッセージ2", "問い合わせメッセージ3" });

            // 中断された場合はその旨をダイアログで表示する。
            if (!result)
            {
                //await _pageDialogService.DisplayAlertAsync(string.Empty, "中断されました", "ok");
                await _customDialogService.Show("たいとる", "中断されました", EImageKind.Nothing, new List<string> { "OK" });
            }
        }
    }
}
