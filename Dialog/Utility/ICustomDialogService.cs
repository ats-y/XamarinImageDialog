using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dialog.Utility
{
    /// <summary>
    /// カスタムダイアログ。
    /// </summary>
    /// <remarks>
    /// https://qiita.com/P3PPP/items/bb8669a6bda52703f9bb
    /// </remarks>
    public interface ICustomDialogService
    {
        /// <summary>
        /// ダイアログを表示する。
        /// </summary>
        /// <param name="title">アラートのタイトル文字列</param>
        /// <param name="message">アラートのメッセージ文字列</param>
        /// <param name="imageKind">アラートに表示する画像種</param>
        /// <param name="accepts">受諾ボタンに表示する文字列リスト。このリスト分ボタン受諾ボタンを表示する。</param>
        /// <param name="destructives">破棄ボタンに表示する文字列。このリスト分ボタン破棄ボタンを表示する。</param>
        /// <param name="cancel">中止ボタンに表示する文字列。nullの場合は中止ボタンは表示しない。</param>
        /// <returns>アラートダイアログでボタンをタップしたら結果を返すTask。</returns>
        Task<CustomAlertResult> Show(string title, string message, EImageKind imageKind
            , List<string> accepts, List<string> destructives = null, string cancel = null);
    }

    /// <summary>
    /// ダイアログに表示する画像
    /// </summary>
    public enum EImageKind
    {
        /// <summary>
        /// なし。画像を表示しない。
        /// </summary>
        Nothing,

        /// <summary>
        /// ？マークの問い合わせ画像。
        /// </summary>
        Question,

        /// <summary>
        /// 三角にビックリマークの警告画像。
        /// </summary>
        Warning,

        /// <summary>
        /// 丸にXマークのエラー画像。
        /// </summary>
        Error,
    }

    /// <summary>
    /// ダイアログ選択結果。
    /// </summary>
    public class CustomAlertResult
    {
        /// <summary>
        /// キャンセルボタンで復帰したか
        /// </summary>
        public bool IsCancel { get; set; }

        /// <summary>
        /// ダイアログでタップしたボタンの表示文字列。
        /// </summary>
        public string PressedButtonTitle { get; set; }
    }
}
