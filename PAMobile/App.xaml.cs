using Foundation;
using UIKit;

namespace PAMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(CustomEntry), (handler, view) =>
            {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif __IOS__
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
                var attributedString = new NSMutableAttributedString(handler.PlatformView.Text ?? "****");
                attributedString.AddAttribute(UIStringAttributeKey.KerningAdjustment, NSNumber.FromFloat(15), new NSRange(0, handler.PlatformView.Text.Length));
                handler.PlatformView.AttributedText = attributedString;
#endif
            });
        }
    }
}
