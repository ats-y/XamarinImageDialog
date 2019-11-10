using System;
using System.Drawing;
using CoreGraphics;
using ObjCRuntime;
using UIKit;

namespace Dialog.iOS
{
    public class CustomAlertSubtitleViewController : UIViewController
    {
        
        public CustomAlertSubtitleViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIImage image = UIImage.FromFile("ErrorImage.png");
            image.ImageWithRenderingMode(UIImageRenderingMode.Automatic);

            UIImageView imageView = new UIImageView
            {
                Image = image,
                Frame = new CGRect(90, 0, 90, 90),
            };


            View.ContentMode = UIViewContentMode.ScaleToFill;

            //View.BackgroundColor = UIColor.Blue;
            View.AddSubview(imageView);
        }
    }
}
