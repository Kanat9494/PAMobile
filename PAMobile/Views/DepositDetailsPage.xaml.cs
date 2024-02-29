namespace PAMobile.Views;

public partial class DepositDetailsPage : ContentPage
{
	public DepositDetailsPage()
	{
		InitializeComponent();

        LocalNotificationCenter.Current.NotificationActionTapped +=
            Current_NotificationActionTapped;

        BindingContext = new DepositDetailsViewModel();
    }

    private async void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs args)
    {
        if (args.IsTapped)
        {
            string filePath = args.Request.ReturningData;

            if (File.Exists(filePath))
            {
                try
                {
                    await Launcher.OpenAsync(new OpenFileRequest
                    {
                        File = new ReadOnlyFile(filePath)
                    });
                }
                catch
                {

                }
            }
            else
                Debug.WriteLine("Файл не найден");
        }
    }
}