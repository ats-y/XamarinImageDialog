using System;
using System.Drawing;
using CoreGraphics;
using ObjCRuntime;
using UIKit;

namespace Dialog.iOS.CustomDialog
{
    /// <summary>
    /// UIAlertControllerにエラー・警告・問い合わせ画像を表示するViewController。
    /// </summary>
    public class AlertImageViewController : UIViewController
    {
        /// <summary>
        /// 表示する画像種。
        /// </summary>
        public EImageKind ImageKind { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="imageKind">表示する画像種</param>
        public AlertImageViewController(EImageKind imageKind)
        {
            ImageKind = imageKind;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // 表示する画像を取得する。
            UIImage image;
            switch (ImageKind)
            {
                case EImageKind.Question:
                    image = UIImage.FromFile("IconQuestion.png");
                    break;
                case EImageKind.Warning:
                    image = UIImage.FromFile("IconWarning.png");
                    break;
                case EImageKind.Error:
                    image = UIImage.FromFile("IconError.png");
                    break;
                default:
                    image = null;
                    break;
            }

            // 表示する画像があればViewに追加する。
            if (image != null)
            {
                image.ImageWithRenderingMode(UIImageRenderingMode.Automatic);
                UIImageView imageView = new UIImageView
                {
                    Image = image,
                    Frame = new CGRect(90, 0, 90, 90),
                };
                View.AddSubview(imageView);
            }
        }
    }
}
