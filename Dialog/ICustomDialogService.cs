using System;
using System.Threading.Tasks;

namespace Dialog
{
    /// <summary>
    /// https://qiita.com/P3PPP/items/bb8669a6bda52703f9bb
    /// </summary>
    public interface ICustomDialogService
    {
        Task<CustomAlertResult> Show(string title, string message, string accepte, string cancel);

        Task<CustomAlertResult> Show3();
    }

    public class CustomAlertResult
    {
        public string PressedButtonTitle { get; set; }
        public string Text { get; set; }
    }
}
