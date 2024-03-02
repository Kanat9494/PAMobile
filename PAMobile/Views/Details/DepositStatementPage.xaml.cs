namespace PAMobile.Views.Details;

public partial class DepositStatementPage : ContentPage
{
	public DepositStatementPage()
	{
		InitializeComponent();

        LocalNotificationCenter.Current.NotificationActionTapped +=
            Current_NotificationActionTapped;

        BindingContext = _viewModel = new DepositStatementViewModel();
    }

    DepositStatementViewModel _viewModel;

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