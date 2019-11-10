using System;
using System.Drawing;
using CoreGraphics;
using ObjCRuntime;
using UIKit;

namespace Dialog.iOS
{
    /// <summary>
    /// エラー画像を表示するビュー
    /// </summary>
    public class ErrorImageViewController : UIViewController
    {
        public ErrorImageViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // エラー画像を表示する。
            UIImage image = UIImage.FromFile("ErrorImage.png");
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
