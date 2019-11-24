using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dialog.Utility
{
    public class CollationDialogUtility
    {
        /// <summary>
        /// カスタムダイアログサービス
        /// </summary>
        ICustomDialogService _dialogService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CollationDialogUtility()
        {
            _dialogService = DependencyService.Get<ICustomDialogService>();
        }

        /// <summary>
        /// 照合大ログを表示する。
        /// </summary>
        /// <param name="warnings">照合警告メッセージリスト</param>
        /// <param name="errors">照合エラーメッセージリスト</param>
        /// <param name="questions">問い合わせメッセージリスト</param>
        /// <returns>true：続行、false：中断</returns>
        public async Task<bool> ShowCollationDialogAsync(List<string> warnings, List<string> errors, List<string> questions)
        {
            // 警告を表示する。
            foreach (string warning in warnings ?? new List<string>())
            {
                var result = await _dialogService.Show("警告", warning, EImageKind.Warning, new List<string>() { "OK" });
            }

            // エラーを表示する。
            if (errors != null && errors.Count > 0)
            {
                foreach (string error in errors)
                {
                    var result = await _dialogService.Show("エラー", error, EImageKind.Error, new List<string>() { "OK" });
                }

                // エラーメッセージを表示したら中断を返す。
                return false;
            }

            // 問い合わせを表示する。
            foreach (string question in questions ?? new List<string>())
            {
                var result = await _dialogService.Show("問い合わせ", question, EImageKind.Question,
                    new List<string>() { "はい" }, cancel: "いいえ");

                // いいえを選択されたら中断を返す。
                if (result.IsCancel)
                {
                    return false;
                }
            }

            // 続行を返す。
            return true;
        }
    }
}
